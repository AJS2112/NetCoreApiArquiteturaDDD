using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarUpdate
{
    public class Retorno_BadRequest
    {
        private UsersController _controller;

        [Fact(DisplayName = "É Possivel Invocar Controller Update")]
        public async Task E_Possivel_Invocar_Controller_Update()
        {
            var userUpdateDTO = new UserUpdateDTO
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            var _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Put(It.IsAny<UserUpdateDTO>())).ReturnsAsync(new UserUpdateResultDTO
            {
                Id = userUpdateDTO.Id,
                Name = userUpdateDTO.Name,
                Email = userUpdateDTO.Email,
                UpdatedAt = DateTime.UtcNow
            });

            _controller = new UsersController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Name", "É Obrigatorio");

            var result = await _controller.Put(userUpdateDTO);
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }
    }
}