using ClosedXML.Excel;
using SGE.DTOs.Importacion;
using SGE.Helpers;
using SGE.Repositories;
using SGE.Services.Interfaces;
using SGE.Models.Entities;

namespace SGE.Services;

public class PadronImportService : IPadronImportService
{
    private const int TamanoLote = 2000;
    private readonly IPersonaRepository _personaRepository;

    public PadronImportService(IPersonaRepository personaRepository)
    {
        _personaRepository = personaRepository;
    }

    public async Task<ResultadoImportacionDto> ImportarAsync(
    Stream archivo,
    string nombreArchivo,
    CancellationToken cancellationToken = default)
    {
        ValidarArchivo(archivo, nombreArchivo);

        var resultado = new ResultadoImportacionDto
        {
            NombreArchivo = nombreArchivo,
            FechaInicio = DateTime.UtcNow
        };

        using var workbook = new XLWorkbook(archivo);

        IXLWorksheet? hoja = workbook.Worksheets.FirstOrDefault();

        if (hoja is null)
        {
            throw new InvalidOperationException(
                "El archivo Excel no contiene ninguna hoja.");
        }

        ValidarEncabezados(hoja);

        Dictionary<string, int> columnas =
            ObtenerMapaColumnas(hoja);

        ValidarFilas(
            hoja,
            columnas,
            resultado,
            cancellationToken);

        if (resultado.Errores.Count > 0)
        {
            resultado.FechaFin = DateTime.UtcNow;
            return resultado;
        }

        await ProcesarFilasAsync(
            hoja,
            columnas,
            resultado,
            cancellationToken);

        resultado.FechaFin = DateTime.UtcNow;

        return resultado;
    }

    private static void ValidarArchivo(
        Stream archivo,
        string nombreArchivo)
    {
        ArgumentNullException.ThrowIfNull(archivo);

        if (!archivo.CanRead)
        {
            throw new InvalidOperationException(
                "El archivo no puede ser leído.");
        }

        if (archivo.Length == 0)
        {
            throw new InvalidOperationException(
                "El archivo está vacío.");
        }

        string extension = Path.GetExtension(nombreArchivo);

        if (!string.Equals(
                extension,
                ".xlsx",
                StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "El archivo debe tener extensión .xlsx.");
        }
    }

    private static void ValidarEncabezados(
        IXLWorksheet hoja)
    {
        IXLRow? filaEncabezados = hoja.FirstRowUsed();

        if (filaEncabezados is null)
        {
            throw new InvalidOperationException(
                "La hoja de cálculo está vacía.");
        }

        HashSet<string> encabezadosEncontrados = filaEncabezados
            .CellsUsed()
            .Select(celda =>
                NormalizarEncabezado(celda.GetString()))
            .Where(encabezado =>
                !string.IsNullOrWhiteSpace(encabezado))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        string[] columnasFaltantes = PadronColumnas.Todas
            .Where(columna =>
                !encabezadosEncontrados.Contains(
                    NormalizarEncabezado(columna)))
            .ToArray();

        if (columnasFaltantes.Length == 0)
        {
            return;
        }

        string mensaje = string.Join(
            ", ",
            columnasFaltantes.Select(columna => $"'{columna}'"));

        throw new InvalidOperationException(
            $"Faltan las siguientes columnas obligatorias: {mensaje}.");
    }

    private static Dictionary<string, int> ObtenerMapaColumnas(
        IXLWorksheet hoja)
    {
        IXLRow filaEncabezados = hoja.FirstRowUsed()
            ?? throw new InvalidOperationException(
                "La hoja de cálculo está vacía.");

        return filaEncabezados
            .CellsUsed()
            .Where(celda =>
                !string.IsNullOrWhiteSpace(celda.GetString()))
            .ToDictionary(
                celda =>
                    NormalizarEncabezado(celda.GetString()),
                celda =>
                    celda.Address.ColumnNumber,
                StringComparer.OrdinalIgnoreCase);
    }

    private static void ValidarFilas(
        IXLWorksheet hoja,
        IReadOnlyDictionary<string, int> columnas,
        ResultadoImportacionDto resultado,
        CancellationToken cancellationToken)
    {
        IXLRow filaEncabezados = hoja.FirstRowUsed()
            ?? throw new InvalidOperationException(
                "La hoja de cálculo está vacía.");

        IXLRow ultimaFila = hoja.LastRowUsed()
            ?? throw new InvalidOperationException(
                "El archivo no contiene filas de datos.");

        int primeraFilaDatos =
            filaEncabezados.RowNumber() + 1;

        int numeroUltimaFila =
            ultimaFila.RowNumber();

        if (primeraFilaDatos > numeroUltimaFila)
        {
            throw new InvalidOperationException(
                "El archivo contiene encabezados, pero no contiene personas.");
        }

        HashSet<long> dnisLeidos = [];

        for (int numeroFila = primeraFilaDatos;
             numeroFila <= numeroUltimaFila;
             numeroFila++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            IXLRow fila = hoja.Row(numeroFila);

            if (fila.IsEmpty())
            {
                continue;
            }

            resultado.FilasLeidas++;

            PersonaImportDto? persona = LeerPersona(
                fila,
                columnas,
                resultado.Errores);

            if (persona is null)
            {
                resultado.FilasOmitidas++;
                continue;
            }

            if (dnisLeidos.Add(persona.Dni))
            {
                continue;
            }

            resultado.Errores.Add(
                new ErrorImportacionDto
                {
                    NumeroFila = numeroFila,
                    DniOriginal = persona.Dni.ToString(),
                    Mensaje =
                        "El DNI está repetido dentro del archivo."
                });

            resultado.FilasOmitidas++;
        }
    }

    private async Task ProcesarFilasAsync(
    IXLWorksheet hoja,
    IReadOnlyDictionary<string, int> columnas,
    ResultadoImportacionDto resultado,
    CancellationToken cancellationToken)
    {
        IXLRow filaEncabezados = hoja.FirstRowUsed()
            ?? throw new InvalidOperationException(
                "La hoja de cálculo está vacía.");

        IXLRow ultimaFila = hoja.LastRowUsed()
            ?? throw new InvalidOperationException(
                "El archivo no contiene filas de datos.");

        int primeraFilaDatos =
            filaEncabezados.RowNumber() + 1;

        int numeroUltimaFila =
            ultimaFila.RowNumber();

        var lote = new List<PersonaImportDto>(TamanoLote);

        for (int numeroFila = primeraFilaDatos;
            numeroFila <= numeroUltimaFila;
            numeroFila++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            IXLRow fila = hoja.Row(numeroFila);

            if (fila.IsEmpty())
            {
                continue;
            }

            PersonaImportDto? persona = LeerPersona(
                fila,
                columnas,
                resultado.Errores);

            if (persona is null)
            {
                continue;
            }

            lote.Add(persona);

            if (lote.Count < TamanoLote)
            {
                continue;
            }

            await ProcesarLoteAsync(
                lote,
                resultado,
                cancellationToken);

            lote.Clear();
        }

        if (lote.Count > 0)
        {
            await ProcesarLoteAsync(
                lote,
                resultado,
                cancellationToken);
        }
    }

    private async Task ProcesarLoteAsync(
    IReadOnlyCollection<PersonaImportDto> lote,
    ResultadoImportacionDto resultado,
    CancellationToken cancellationToken)
    {
        if (lote.Count == 0)
        {
            return;
        }

        Dictionary<long, Persona> personasExistentes =
            await _personaRepository.ObtenerPorDnisAsync(
                lote.Select(persona => persona.Dni),
                cancellationToken);

        var personasNuevas = new List<Persona>();
        var personasActualizadas = new List<Persona>();

        foreach (PersonaImportDto personaDto in lote)
        {
            if (personasExistentes.TryGetValue(
            personaDto.Dni,
            out Persona? personaExistente))
            {
                if (PersonaCambio(personaExistente, personaDto))
                {
                    ActualizarPersona(
                        personaExistente,
                        personaDto);

                    personasActualizadas.Add(personaExistente);
                    resultado.PersonasActualizadas++;
                }
                else
                {
                    resultado.PersonasSinCambios++;
                }
            }
            else
            {
                personasNuevas.Add(
                    CrearPersona(personaDto));

                resultado.PersonasCreadas++;
            }
        }

        if (personasNuevas.Count > 0)
        {
            await _personaRepository.AgregarRangoAsync(
                personasNuevas,
                cancellationToken);
        }

        if (personasActualizadas.Count > 0)
        {
            _personaRepository.ActualizarRango(
                personasActualizadas);
        }

        await _personaRepository.GuardarCambiosAsync(
            cancellationToken);

        _personaRepository.LimpiarSeguimiento();
    }
    private static Persona CrearPersona(
    PersonaImportDto dto)
    {
        return new Persona
        {
            Dni = dto.Dni,
            ApellidoNombre = dto.ApellidoNombre,
            Sexo = dto.Sexo,
            Domicilio = dto.Domicilio,
            Circuito = dto.Circuito,
            Localidad = dto.Localidad,
            Departamento = dto.Departamento,
            IdSeccion = dto.IdSeccion,
            Escuela = dto.Escuela,
            Mesa = dto.Mesa,
            DomicilioEscuela = dto.DomicilioEscuela,
            LocalidadEscuela = dto.LocalidadEscuela,
            Orden = dto.Orden,
            Cambio = dto.Cambio,
            Observaciones = dto.Observaciones
        };
    }

    private static void ActualizarPersona(
        Persona persona,
        PersonaImportDto dto)
    {
        persona.ApellidoNombre = dto.ApellidoNombre;
        persona.Sexo = dto.Sexo;
        persona.Domicilio = dto.Domicilio;
        persona.Circuito = dto.Circuito;
        persona.Localidad = dto.Localidad;
        persona.Departamento = dto.Departamento;
        persona.IdSeccion = dto.IdSeccion;
        persona.Escuela = dto.Escuela;
        persona.Mesa = dto.Mesa;
        persona.DomicilioEscuela = dto.DomicilioEscuela;
        persona.LocalidadEscuela = dto.LocalidadEscuela;
        persona.Orden = dto.Orden;
        persona.Cambio = dto.Cambio;
        persona.Observaciones = dto.Observaciones;
    }

    private static bool PersonaCambio(
    Persona persona,
    PersonaImportDto dto)
    {
        return
            persona.ApellidoNombre != dto.ApellidoNombre ||
            persona.Sexo != dto.Sexo ||
            persona.Domicilio != dto.Domicilio ||
            persona.Circuito != dto.Circuito ||
            persona.Localidad != dto.Localidad ||
            persona.Departamento != dto.Departamento ||
            persona.IdSeccion != dto.IdSeccion ||
            persona.Escuela != dto.Escuela ||
            persona.Mesa != dto.Mesa ||
            persona.DomicilioEscuela != dto.DomicilioEscuela ||
            persona.LocalidadEscuela != dto.LocalidadEscuela ||
            persona.Orden != dto.Orden ||
            persona.Cambio != dto.Cambio ||
            persona.Observaciones != dto.Observaciones;
    }

    private static PersonaImportDto? LeerPersona(
        IXLRow fila,
        IReadOnlyDictionary<string, int> columnas,
        List<ErrorImportacionDto> errores)
    {
        string dniOriginal = ObtenerTexto(
            fila,
            columnas,
            PadronColumnas.Dni) ?? string.Empty;

        if (!long.TryParse(dniOriginal, out long dni)
            || dni <= 0)
        {
            errores.Add(new ErrorImportacionDto
            {
                NumeroFila = fila.RowNumber(),
                DniOriginal = dniOriginal,
                Mensaje =
                    "El DNI está vacío o no tiene un formato válido."
            });

            return null;
        }

        string apellidoNombre = ObtenerTexto(
            fila,
            columnas,
            PadronColumnas.Persona) ?? string.Empty;

        if (string.IsNullOrWhiteSpace(apellidoNombre))
        {
            errores.Add(new ErrorImportacionDto
            {
                NumeroFila = fila.RowNumber(),
                DniOriginal = dniOriginal,
                Mensaje =
                    "El apellido y nombre son obligatorios."
            });

            return null;
        }

        return new PersonaImportDto
        {
            Dni = dni,
            ApellidoNombre = apellidoNombre,
            Sexo = ObtenerTexto(
                fila,
                columnas,
                PadronColumnas.Sexo),
            Domicilio = ObtenerTexto(
                fila,
                columnas,
                PadronColumnas.Domicilio),
            Circuito = ObtenerTexto(
                fila,
                columnas,
                PadronColumnas.Circuito),
            Localidad = ObtenerTexto(
                fila,
                columnas,
                PadronColumnas.Localidad),
            Departamento = ObtenerTexto(
                fila,
                columnas,
                PadronColumnas.Departamento),
            IdSeccion = ObtenerEnteroNullable(
                fila,
                columnas,
                PadronColumnas.IdSeccion),
            Escuela = ObtenerTexto(
                fila,
                columnas,
                PadronColumnas.Escuela),
            Mesa = ObtenerEnteroNullable(
                fila,
                columnas,
                PadronColumnas.Mesa),
            DomicilioEscuela = ObtenerTexto(
                fila,
                columnas,
                PadronColumnas.DomicilioEsc),
            LocalidadEscuela = ObtenerTexto(
                fila,
                columnas,
                PadronColumnas.Loca),
            Orden = ObtenerEnteroNullable(
                fila,
                columnas,
                PadronColumnas.Orden),
            Cambio = ObtenerTexto(
                fila,
                columnas,
                PadronColumnas.Cambio),
            Observaciones = ObtenerTexto(
                fila,
                columnas,
                PadronColumnas.Observaciones)
        };
    }

    private static string? ObtenerTexto(
        IXLRow fila,
        IReadOnlyDictionary<string, int> columnas,
        string nombreColumna)
    {
        string nombreNormalizado =
            NormalizarEncabezado(nombreColumna);

        if (!columnas.TryGetValue(
                nombreNormalizado,
                out int numeroColumna))
        {
            return null;
        }

        string valor = fila
            .Cell(numeroColumna)
            .GetFormattedString()
            .Trim();

        return string.IsNullOrWhiteSpace(valor)
            ? null
            : valor;
    }

    private static int? ObtenerEnteroNullable(
        IXLRow fila,
        IReadOnlyDictionary<string, int> columnas,
        string nombreColumna)
    {
        string? valor = ObtenerTexto(
            fila,
            columnas,
            nombreColumna);

        if (string.IsNullOrWhiteSpace(valor))
        {
            return null;
        }

        return int.TryParse(valor, out int numero)
            ? numero
            : null;
    }

    private static string NormalizarEncabezado(
        string encabezado)
    {
        return encabezado
            .Trim()
            .ToUpperInvariant();
    }
}