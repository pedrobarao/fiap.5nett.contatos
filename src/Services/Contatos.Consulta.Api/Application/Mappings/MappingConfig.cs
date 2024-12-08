using Contatos.Consulta.Api.Application.DTOs;
using Contatos.Consulta.Api.Domain.Entities;
using Contatos.Consulta.Api.Domain.ValueObjects;
using Mapster;

namespace Contatos.Consulta.Api.Application.Mappings;

public static class MappingConfig
{
    public static void Register()
    {
        TypeAdapterConfig<Contato, ObterContatoOutput>
            .NewConfig()
            .Map(dest => dest.Nome, src => src.Nome.PrimeiroNome)
            .Map(dest => dest.Sobrenome, src => src.Nome.Sobrenome)
            .Map(dest => dest.Email, src => src.Email != null ? src.Email.Endereco : null);

        TypeAdapterConfig<Telefone, ObterContatoOutput.TelefoneOutput>
            .NewConfig()
            .Map(dest => dest.Ddd, src => src.Ddd)
            .Map(dest => dest.Numero, src => src.Numero)
            .Map(dest => dest.Tipo, src => src.Tipo);
    }
}