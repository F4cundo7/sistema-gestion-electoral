using SGE.DTOs.Movilizadores;
using SGE.Models.Entities;
using SGE.Repositories;
using SGE.Services.Interfaces;

namespace SGE.Services;

public class MovilizadorService : IMovilizadorService
{
    private readonly IMovilizadorRepository _movilizadorRepository;
    private readonly IPersonaRepository _personaRepository;
    private readonly IReferenteRepository _referenteRepository;

    public MovilizadorService(
        IMovilizadorRepository movilizadorRepository,
        IPersonaRepository personaRepository,
        IReferenteRepository referenteRepository)
    {
        _movilizadorRepository = movilizadorRepository;
        _personaRepository = personaRepository;
        _referenteRepository = referenteRepository;
    }

    public async Task<List<MovilizadorListadoDto>> ObtenerTodosAsync(
        CancellationToken cancellationToken = default)
    {
        var movilizadores =
            await _movilizadorRepository.ObtenerTodosAsync(
                cancellationToken);

        return movilizadores
            .Select(MapearListado)
            .ToList();
    }

    public async Task<List<MovilizadorListadoDto>> ObtenerPorReferenteAsync(
        long dniReferente,
        CancellationToken cancellationToken = default)
    {
        var referente =
            await _referenteRepository.ObtenerPorDniAsync(
                dniReferente,
                cancellationToken);

        if (referente is null)
        {
            throw new InvalidOperationException(
                "No se encontró el referente indicado.");
        }

        var movilizadores =
            await _movilizadorRepository.ObtenerPorReferenteAsync(
                referente.Id,
                cancellationToken);

        return movilizadores
            .Select(MapearListado)
            .ToList();
    }

    public async Task<MovilizadorDetalleDto?> ObtenerDetallePorDniAsync(
        long dni,
        CancellationToken cancellationToken = default)
    {
        var movilizador =
            await _movilizadorRepository.ObtenerPorDniAsync(
                dni,
                cancellationToken);

        if (movilizador is null)
        {
            return null;
        }

        return new MovilizadorDetalleDto
        {
            Dni = movilizador.Persona.Dni,
            ApellidoNombre = movilizador.Persona.ApellidoNombre,
            Sexo = movilizador.Persona.Sexo,
            Domicilio = movilizador.Persona.Domicilio,
            Localidad = movilizador.Persona.Localidad,
            Departamento = movilizador.Persona.Departamento,
            Circuito = movilizador.Persona.Circuito,
            Escuela = movilizador.Persona.Escuela,
            Mesa = movilizador.Persona.Mesa,

            DniReferente = movilizador.Referente.Persona.Dni,
            ReferenteApellidoNombre =
                movilizador.Referente.Persona.ApellidoNombre,

            Vehiculo = movilizador.Vehiculo,
            Patente = movilizador.Patente,

            CantidadVotantes =
                movilizador.Votantes.Count(votante => votante.Activo),

            FechaAlta = movilizador.FechaAlta,
            Activo = movilizador.Activo
        };
    }

    public async Task<EditarMovilizadorDto?>
        ObtenerParaEditarPorDniAsync(
            long dni,
            CancellationToken cancellationToken = default)
    {
        var movilizador =
            await _movilizadorRepository
                .ObtenerParaEditarPorDniAsync(
                    dni,
                    cancellationToken);

        if (movilizador is null)
        {
            return null;
        }

        return new EditarMovilizadorDto
        {
            Dni = movilizador.Persona.Dni,
            ApellidoNombre = movilizador.Persona.ApellidoNombre,

            DniReferente = movilizador.Referente.Persona.Dni,
            ReferenteApellidoNombre =
                movilizador.Referente.Persona.ApellidoNombre,

            Vehiculo = movilizador.Vehiculo,
            Patente = movilizador.Patente,
            Activo = movilizador.Activo
        };
    }

    public async Task CrearAsync(
        CrearMovilizadorDto dto,
        CancellationToken cancellationToken = default)
    {
        ValidarDtoCreacion(dto);

        var persona =
            await _personaRepository.ObtenerPorDniAsync(
                dto.Dni,
                cancellationToken);

        if (persona is null)
        {
            throw new InvalidOperationException(
                "La persona no se encuentra registrada en el padrón.");
        }

        var referente =
            await _referenteRepository.ObtenerPorDniAsync(
                dto.DniReferente,
                cancellationToken);

        if (referente is null)
        {
            throw new InvalidOperationException(
                "No se encontró el referente seleccionado.");
        }

        if (!referente.Activo)
        {
            throw new InvalidOperationException(
                "No se puede asignar un movilizador a un referente inactivo.");
        }

        var yaEsMovilizador =
            await _movilizadorRepository.ExistePorPersonaAsync(
                persona.Id,
                cancellationToken);

        if (yaEsMovilizador)
        {
            throw new InvalidOperationException(
                "La persona ya está registrada como movilizador.");
        }

        /*
         * Un referente puede actuar como movilizador.
         * Por eso no impedimos que la persona también tenga
         * un registro activo como referente.
         */

        var movilizador = new Movilizador
        {
            PersonaId = persona.Id,
            ReferenteId = referente.Id,
            Vehiculo = NormalizarTexto(dto.Vehiculo),
            Patente = NormalizarPatente(dto.Patente),
            FechaAlta = DateTime.UtcNow,
            Activo = true
        };

        await _movilizadorRepository.AgregarAsync(
            movilizador,
            cancellationToken);

        await _movilizadorRepository.GuardarCambiosAsync(
            cancellationToken);
    }

    public async Task EditarAsync(
        EditarMovilizadorDto dto,
        CancellationToken cancellationToken = default)
    {
        ValidarDtoEdicion(dto);

        var movilizador =
            await _movilizadorRepository
                .ObtenerParaEditarPorDniAsync(
                    dto.Dni,
                    cancellationToken);

        if (movilizador is null)
        {
            throw new InvalidOperationException(
                "No se encontró el movilizador.");
        }

        movilizador.Vehiculo =
            NormalizarTexto(dto.Vehiculo);

        movilizador.Patente =
            NormalizarPatente(dto.Patente);

        movilizador.Activo = dto.Activo;

        await _movilizadorRepository.GuardarCambiosAsync(
            cancellationToken);
    }

    private static MovilizadorListadoDto MapearListado(
        Movilizador movilizador)
    {
        return new MovilizadorListadoDto
        {
            Dni = movilizador.Persona.Dni,
            ApellidoNombre =
                movilizador.Persona.ApellidoNombre,

            DniReferente =
                movilizador.Referente.Persona.Dni,

            ReferenteApellidoNombre =
                movilizador.Referente.Persona.ApellidoNombre,

            Vehiculo = movilizador.Vehiculo,
            Patente = movilizador.Patente,

            CantidadVotantes =
                movilizador.Votantes.Count(votante => votante.Activo),

            FechaAlta = movilizador.FechaAlta,
            Activo = movilizador.Activo
        };
    }

    private static void ValidarDtoCreacion(
        CrearMovilizadorDto dto)
    {
        if (dto.Dni <= 0)
        {
            throw new InvalidOperationException(
                "El DNI del movilizador no es válido.");
        }

        if (dto.DniReferente <= 0)
        {
            throw new InvalidOperationException(
                "Debe seleccionar un referente válido.");
        }

        ValidarVehiculoYPatente(
            dto.Vehiculo,
            dto.Patente);
    }

    private static void ValidarDtoEdicion(
        EditarMovilizadorDto dto)
    {
        if (dto.Dni <= 0)
        {
            throw new InvalidOperationException(
                "El DNI del movilizador no es válido.");
        }

        ValidarVehiculoYPatente(
            dto.Vehiculo,
            dto.Patente);
    }

    private static void ValidarVehiculoYPatente(
        string vehiculo,
        string patente)
    {
        if (string.IsNullOrWhiteSpace(vehiculo))
        {
            throw new InvalidOperationException(
                "Debe ingresar el vehículo.");
        }

        if (vehiculo.Trim().Length > 100)
        {
            throw new InvalidOperationException(
                "El vehículo no puede superar los 100 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(patente))
        {
            throw new InvalidOperationException(
                "Debe ingresar la patente.");
        }

        var patenteNormalizada =
            NormalizarPatente(patente);

        if (patenteNormalizada.Length > 15)
        {
            throw new InvalidOperationException(
                "La patente no puede superar los 15 caracteres.");
        }
    }

    private static string NormalizarTexto(string texto)
    {
        return texto.Trim();
    }

    private static string NormalizarPatente(string patente)
    {
        return patente
            .Trim()
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty)
            .ToUpperInvariant();
    }
}