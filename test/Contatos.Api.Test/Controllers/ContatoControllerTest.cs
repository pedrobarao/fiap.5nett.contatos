// using Commons.Domain.Communication;
// using Contatos.Api.Controllers;
// using Contatos.Application.DTOs.Inputs;
// using Contatos.Application.DTOs.Outputs;
// using Contatos.Application.UseCases.Interfaces;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using Moq.AutoMock;
//
// namespace Contatos.Api.Test.Controllers;
//
// public class ContatoControllerTests
// {
//     private readonly AutoMocker _mocker;
//
//     public ContatoControllerTests()
//     {
//         _mocker = new AutoMocker();
//     }
//
//     [Fact(DisplayName = "Listar contatos cadastrados deve retornar contatos com sucesso")]
//     [Trait("Category", "ContatoController")]
//     public async Task Listar_DeveRetornarContatosComSucesso()
//     {
//         // Arrange
//         var pagedResult = new PagedResult<ObterContatoOutput>(new List<ObterContatoOutput>(), 1, 1, 1, "");
//         _mocker.GetMock<IListarContatoUseCase>()
//             .Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
//             .ReturnsAsync(pagedResult);
//         var controller = _mocker.CreateInstance<ContatoController>();
//
//         // Act
//         var result = await controller.Listar();
//
//         // Assert
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
//         Assert.Equal(pagedResult, okResult.Value);
//     }
//
//     [Fact(DisplayName = "Obter contato cadastrado deve retornar contato com sucesso")]
//     [Trait("Category", "ContatoController")]
//     public async Task Obter_ContatoEncontrado_DeveRetornarContatoComSucesso()
//     {
//         // Arrange
//         var contatoOutput = new ObterContatoOutput();
//         _mocker.GetMock<IObterContatoUseCase>().Setup(u => u.ExecuteAsync(It.IsAny<Guid>()))
//             .ReturnsAsync(contatoOutput);
//         var controller = _mocker.CreateInstance<ContatoController>();
//         // Act
//         var result = await controller.Obter(Guid.NewGuid());
//
//         // Assert
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
//         Assert.Equal(contatoOutput, okResult.Value);
//     }
//
//     [Fact(DisplayName = "Atualizar contato com valores válidos deve atualizar contato com sucesso")]
//     [Trait("Category", "ContatoController")]
//     public async Task Obter_ContatoNaoExiste_DeveRetornarNotFound()
//     {
//         // Arrange
//         _mocker.GetMock<IObterContatoUseCase>().Setup(u => u.ExecuteAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);
//         var controller = _mocker.CreateInstance<ContatoController>();
//         // Act
//         var result = await controller.Obter(Guid.NewGuid());
//
//         // Assert
//         Assert.IsType<NotFoundResult>(result);
//     }
//
//     [Fact(DisplayName = "Criar contato com valores válidos deve criar contato com sucesso")]
//     [Trait("Category", "ContatoController")]
//     public async Task Criar_ContatoValido_DeveCriarContatoComSucesso()
//     {
//         // Arrange
//         var contatoCriadoOutput = new ContatoCriadoOutput { Id = Guid.NewGuid() };
//         var resultMock = new Result<ContatoCriadoOutput>();
//         resultMock.SetData(contatoCriadoOutput);
//         _mocker.GetMock<ICadastrarContatoUseCase>().Setup(u => u.ExecuteAsync(It.IsAny<NovoContatoInput>()))
//             .ReturnsAsync(resultMock);
//         var controller = _mocker.CreateInstance<ContatoController>();
//
//         // Act
//         var result = await controller.Criar(new NovoContatoInput());
//
//         // Assert
//         var createdResult = Assert.IsType<CreatedResult>(result);
//         Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
//         Assert.Equal(contatoCriadoOutput, createdResult.Value);
//     }
//
//     [Fact(DisplayName = "Criar contato com valores inválidos deve retornar erro")]
//     [Trait("Category", "ContatoController")]
//     public async Task Criar_ContatoInvalido_DeveRetornarErro()
//     {
//         // Arrange
//         var resultMock = new Result<ContatoCriadoOutput>();
//         resultMock.AddError("Erro");
//         _mocker.GetMock<ICadastrarContatoUseCase>().Setup(u => u.ExecuteAsync(It.IsAny<NovoContatoInput>()))
//             .ReturnsAsync(resultMock);
//         var controller = _mocker.CreateInstance<ContatoController>();
//         // Act
//         var result = await controller.Criar(new NovoContatoInput());
//
//         // Assert
//         var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
//         Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
//     }
//
//     [Fact(DisplayName = "Atualizar contato com valores válidos deve atualizar contato com sucesso")]
//     [Trait("Category", "ContatoController")]
//     public async Task Atualizar_ContatoValido_DeveAtualizarContatoComSucesso()
//     {
//         // Arrange
//         var resultMock = new Result();
//         _mocker.GetMock<IAtualizarContatoUseCase>().Setup(u => u.ExecuteAsync(It.IsAny<AtualizarContatoInput>()))
//             .ReturnsAsync(resultMock);
//         var id = Guid.NewGuid();
//         var controller = _mocker.CreateInstance<ContatoController>();
//         // Act
//         var result = await controller.Atualizar(id, new AtualizarContatoInput { Id = id });
//
//         // Assert
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
//     }
//
//     [Fact(DisplayName = "Atualizar contato com id de rota diferente do id de input deve retornar erro")]
//     [Trait("Category", "ContatoController")]
//     public async Task Atualizar_IdRotaDiferenteIdInput_DeveRetornarErro()
//     {
//         // Arrange
//         var controller = _mocker.CreateInstance<ContatoController>();
//
//         // Act
//         var result = await controller.Atualizar(Guid.NewGuid(), new AtualizarContatoInput { Id = Guid.NewGuid() });
//
//         // Assert
//         var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
//         Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
//     }
//
//     [Fact(DisplayName = "Atualizar contato com valores inválidos deve retornar erro")]
//     [Trait("Category", "ContatoController")]
//     public async Task Atualizar_ContatoInvalido_DeveRetornarErro()
//     {
//         // Arrange
//         var resultMock = new Result();
//         resultMock.AddError("Erro");
//         _mocker.GetMock<IAtualizarContatoUseCase>().Setup(u => u.ExecuteAsync(It.IsAny<AtualizarContatoInput>()))
//             .ReturnsAsync(resultMock);
//         var controller = _mocker.CreateInstance<ContatoController>();
//         // Act
//         var result = await controller.Atualizar(Guid.NewGuid(), new AtualizarContatoInput { Id = Guid.NewGuid() });
//
//         // Assert
//         var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
//         Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
//     }
//
//     [Fact(DisplayName = "Excluir contato deve excluir com sucesso")]
//     [Trait("Category", "ContatoController")]
//     public async Task Excluir_ContatoValido_DeveExcluirContatoComSucesso()
//     {
//         // Arrange
//         var controller = _mocker.CreateInstance<ContatoController>();
//
//         // Act
//         var result = await controller.Excluir(It.IsAny<Guid>());
//
//         // Assert
//         var noContentResult = Assert.IsType<NoContentResult>(result);
//         Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
//     }
// }