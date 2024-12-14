﻿using Commons.Domain.Communication;
using Contato = Contatos.Consulta.Api.Domain.Entities.Contato;

namespace Contatos.Consulta.Api.Domain.Repositories;

public interface IContatoRepository
{
    Task Inserir(Contato contato);
    Task<Contato?> ObterContatoPorId(Guid id);
    Task<PagedResult<Contato>> ObterContatosPaginados(int pageSize, int pageIndex, string? query = null);
}