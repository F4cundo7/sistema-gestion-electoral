using  SGE.DTOs.Compartidos;
using SGE.DTOs.Referentes;
using SGE.Models.Entities;
using SGE.Repositories;
using SGE.Services.Interfaces;

namespace SGE.Services;

public class ReferenteService : IReferenteService
{
    private readonly IPersonaRepository _personaRepository;
    private readonly IReferenteRepository _referenteRepository;

    public ReferenteService(IPersonaRepository personaRepository, IReferenteRepository referenteRepository)
    {
        _personaRepository = personaRepository;
        _referenteRepository = referenteRepository;
    }
    public async Task<List<ReferenteListadoDto>> ObtenerTodosAsync(
    CancellationToken cancellationToken = default)
    {
        var referentes = await _referenteRepository.ObtenerTodosAsync(
            cancellationToken);

        return referentes
            .Select(referente => new ReferenteListadoDto
            {
                Dni = referente.Persona.Dni,
                ApellidoNombre = referente.Persona.ApellidoNombre,
                Telefono = referente.Telefono,
                Activo = referente.Activo,
                FechaAlta = referente.FechaAlta
            })
            .ToList();
    }
    public async Task<ReferenteDetalleDto?> ObtenerDetallePorDniAsync(
    long dni,
    CancellationToken cancellationToken = default)
{
    if (dni <= 0)
    {
        return null;
    }

    var referente = await _referenteRepository.ObtenerPorDniAsync(
        dni,
        cancellationToken);

    if (referente is null)
    {
        return null;
    }

    return new ReferenteDetalleDto
    {
        Dni = referente.Persona.Dni,
        ApellidoNombre = referente.Persona.ApellidoNombre,
        Sexo = referente.Persona.Sexo,
        Domicilio = referente.Persona.Domicilio,
        Localidad = referente.Persona.Localidad,
        Departamento = referente.Persona.Departamento,
        Circuito = referente.Persona.Circuito,
        Escuela = referente.Persona.Escuela,
        Mesa = referente.Persona.Mesa,
        Telefono = referente.Telefono,
        Observaciones = referente.Observaciones,
        FechaAlta = referente.FechaAlta,
        Activo = referente.Activo,
        CantidadMovilizadores = referente.Movilizadores.Count
    };
}
    public async Task<EditarReferenteDto?> ObtenerParaEditarPorDniAsync(
    long dni,
    CancellationToken cancellationToken = default)
{
    if (dni <= 0)
    {
        return null;
    }

    var referente =
        await _referenteRepository.ObtenerParaEditarPorDniAsync(
            dni,
            cancellationToken);

    if (referente is null)
    {
        return null;
    }

    return new EditarReferenteDto
    {
        Dni = referente.Persona.Dni,
        ApellidoNombre = referente.Persona.ApellidoNombre,
        Telefono = referente.Telefono,
        Observaciones = referente.Observaciones,
        Activo = referente.Activo
    };
}

public async Task<ResultadoOperacionDto> EditarAsync(
    EditarReferenteDto dto,
    CancellationToken cancellationToken = default)
{
    if (dto.Dni <= 0)
    {
        return new ResultadoOperacionDto
        {
            Exito = false,
            Mensaje = "El DNI ingresado no es válido."
        };
    }

    var referente =
        await _referenteRepository.ObtenerParaEditarPorDniAsync(
            dto.Dni,
            cancellationToken);

    if (referente is null)
    {
        return new ResultadoOperacionDto
        {
            Exito = false,
            Mensaje = "No se encontró el referente indicado."
        };
    }

    referente.Telefono = NormalizarTexto(dto.Telefono);
    referente.Observaciones = NormalizarTexto(dto.Observaciones);
    referente.Activo = dto.Activo;

    _referenteRepository.Actualizar(referente);

    await _referenteRepository.GuardarCambiosAsync(
        cancellationToken);

    return new ResultadoOperacionDto
    {
        Exito = true,
        Mensaje = "Los datos del referente fueron actualizados correctamente."
    };
}
    public async Task<ResultadoOperacionDto> CrearAsync(
        CrearReferenteDto dto,
        CancellationToken cancellationToken = default)
    {
        if(dto.Dni <= 0)
        {
            return new ResultadoOperacionDto
            {
                Exito= false,
                Mensaje = "El DNI ingresado no es válido."
            };
        }
        Persona? persona = await _personaRepository.ObtenerPorDniAsync(
        dto.Dni,
        cancellationToken);
        if(persona is null)
        {
            return new ResultadoOperacionDto
            {
                Exito = false,
                Mensaje = "La persona no se encuentra en el padrón electoral."
            };
        }
        bool yaEsReferente = await _referenteRepository.ExistePorPersonaAsync(
            persona.Id,
            cancellationToken);
        if(yaEsReferente)
        {
            return new ResultadoOperacionDto
            {
                Exito = false,
                Mensaje = "La persona ya está registrada como referente."
            };
        }
        var referente = new Referente
        {
            PersonaId = persona.Id,
            Telefono = NormalizarTexto(dto.Telefono),
            Observaciones = NormalizarTexto(dto.Observaciones),
            FechaAlta = DateTime.UtcNow,
            Activo = true
        };
        await _referenteRepository.AgregarAsync(referente, cancellationToken);
        await _referenteRepository.GuardarCambiosAsync(cancellationToken);

        return new ResultadoOperacionDto
        {
            Exito = true,
            Mensaje = "Referente registrado exitosamente."
        };
    }
    private static string? NormalizarTexto(string? valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            return null;
        }
        return valor.Trim();
    }
}