using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarCreate
{
    public class Retorno_Created
    {
        private UsersController _controller;
        [Fact(DisplayName = "É Possivel Realizar o Created")]
        public async Task E_Possivel_Invocar_Controller_Create()
        {
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            var _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Post(It.IsAny<UserCreateDTO>())).ReturnsAsync(new UserCreateResultDTO
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                CreatedAt = DateTime.UtcNow
            });

            _controller = new UsersController(_serviceMock.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");

            _controller.Url = url.Object;

            var userCreateDTO = new UserCreateDTO
            {
                Name = name,
                Email = email
            };

            var result = await _controller.Post(userCreateDTO);
            Assert.True(result is CreatedResult);

            var resultValue = ((CreatedResult)result).Value as UserCreateResultDTO;
            Assert.NotNull(resultValue);
            Assert.Equal(userCreateDTO.Name, resultValue.Name);
            Assert.Equal(userCreateDTO.Email, resultValue.Email);

        }
    }
}