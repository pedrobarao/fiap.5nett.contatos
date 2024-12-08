using Contatos.Consulta.Api.Application.DTOs;
using Contatos.Consulta.Api.Domain.Repositories;
using MapsterMapper;

namespace Contatos.Consulta.Api.Application.Queries;

public class ObterContatoQuery(IContatoRepository contatoRepository, IMapper mapper) : IObterContatoQuery
{
    public async Task<ObterContatoOutput?> ExecuteAsync(Guid id)
    {
        var contato = await contatoRepository.ObterContatoPorIdAsync(id);

        if (contato is null) return null;

        return mapper.Map<ObterContatoOutput>(contato);
    }
}