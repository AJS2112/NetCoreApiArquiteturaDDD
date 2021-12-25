using System;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Login
{
    public class QuandoForExecutadoFindByLogin
    {
        private ILoginService _service;
        private Mock<ILoginService> _serviceMock;

        [Fact(DisplayName = "E Possivel Executar o Metodo FindByLogin")]
        public async Task E_Possivel_Executar_Metodo_FindByLogin()
        {
            var email = Faker.Internet.Email();
            var objetoRetorno = new
            {
                authenticated = true,
                created = DateTime.UtcNow,
                expirationDate = DateTime.UtcNow.AddHours(8),
                accessToken = Guid.NewGuid(),
                userName = email,
                message = "Usuario logado com sucesso"
            };

            var loginDTO = new LoginDTO
            {
                Email = email
            };

            _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(m => m.FindByLogin(loginDTO)).ReturnsAsync(objetoRetorno);
            _service = _serviceMock.Object;

            var result = await _service.FindByLogin(loginDTO);
            Assert.NotNull(result);

        }

    }
}