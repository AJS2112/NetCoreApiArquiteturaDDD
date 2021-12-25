using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarGetAll
{
    public class Retorno_BadRequest
    {
        private UsersController _controller;
        [Fact(DisplayName = "É Possivel Invocar GetAll")]
        public async Task E_Possivel_Invocar_Controller_GetAll()
        {
            var _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(new List<UserDTO>{
                new UserDTO{
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreatedAt=DateTime.UtcNow
                },
                new UserDTO{
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreatedAt=DateTime.UtcNow
                }
            });

            _controller = new UsersController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Invalid");

            var result = await _controller.GetAll();
            Assert.True(result is BadRequestObjectResult);
        }

    }
}