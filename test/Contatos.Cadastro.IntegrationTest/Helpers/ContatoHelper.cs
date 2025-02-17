using Contatos.Cadastro.Api.Domain.Entities;
using Contatos.Test.Commons.Cadastros.Builders;

namespace Contatos.Cadastro.IntegrationTest.Helpers;

public static class ContatoHelper
{
    public static Contato ContatoValido()
    {
        return new ContatoBuilder().Build();
    }
}