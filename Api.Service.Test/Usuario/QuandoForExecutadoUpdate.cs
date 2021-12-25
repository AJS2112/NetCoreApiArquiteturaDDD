using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class QuandoForExecutadoUpdate : UsuarioTestes
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;
        [Fact(DisplayName = "É Possivel Executar o Metodo Update")]
        public async Task E_Possivel_Executar_Metodo_Update()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Put(userUpdateDTO)).ReturnsAsync(userUpdateResultDTO);
            _service = _serviceMock.Object;

            var result = await _service.Put(userUpdateDTO);
            Assert.NotNull(result);
            Assert.Equal(result.Name, NomeUsuarioAlterado);
            Assert.Equal(result.Email, EmailUsuarioAlterado);
            Assert.NotNull(result.Id);
        }
    }
}