using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarGet
{
    public class Retorno_BadRequest
    {
        private UsersController _controller;
        [Fact(DisplayName = "É Possivel Invocar Get")]
        public async Task E_Possivel_Invocar_Controller_Get()
        {
            var idUsuario = Guid.NewGuid();

            var userDTO = new UserDTO
            {
                Id = idUsuario,
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreatedAt = DateTime.UtcNow
            };

            var _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(userDTO);

            _controller = new UsersController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Get(idUsuario);
            Assert.True(result is BadRequestObjectResult);


        }
    }
}