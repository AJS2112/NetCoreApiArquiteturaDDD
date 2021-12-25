using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarDelete
{
    public class Retorno_BadRequest
    {
        private UsersController _controller;
        [Fact(DisplayName = "E Possivel Invocar Delete")]
        public async Task E_Possivel_Invocar_Controller_Delete()
        {
            var idUsuario = Guid.NewGuid();
            var _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            _controller = new UsersController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Required");

            var result = await _controller.Delete(idUsuario);
            Assert.True(result is BadRequestObjectResult);
        }

    }
}