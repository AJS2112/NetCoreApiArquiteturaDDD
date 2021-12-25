using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UsuarioMapper : BaseTestService
    {
        [Fact(DisplayName = "É Possivel Mappear os Modelos")]
        public void E_Possivel_Mappear_Os_Modelos()
        {
            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var listEntity = new List<UserEntity>();

            for (int i = 0; i < 5; i++)
            {
                var item = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                listEntity.Add(item);
            }


            //Model => Entity
            var dtoToEntity = Mapper.Map<UserEntity>(model);
            Assert.Equal(dtoToEntity.Id, model.Id);
            Assert.Equal(dtoToEntity.Name, model.Name);
            Assert.Equal(dtoToEntity.Email, model.Email);
            Assert.Equal(dtoToEntity.CreatedAt, model.CreatedAt);
            Assert.Equal(dtoToEntity.UpdatedAt, model.UpdatedAt);

            //Entity => DTO
            var userDTO = Mapper.Map<UserDTO>(dtoToEntity);
            Assert.Equal(userDTO.Id, dtoToEntity.Id);
            Assert.Equal(userDTO.Name, dtoToEntity.Name);
            Assert.Equal(userDTO.Email, dtoToEntity.Email);
            Assert.Equal(userDTO.CreatedAt, dtoToEntity.CreatedAt);

            var listDTO = Mapper.Map<List<UserEntity>>(listEntity);
            Assert.True(listDTO.Count() == listEntity.Count());
            for (int i = 0; i < listDTO.Count(); i++)
            {
                Assert.Equal(listDTO[i].Id, listEntity[i].Id);
                Assert.Equal(listDTO[i].Name, listEntity[i].Name);
                Assert.Equal(listDTO[i].Email, listEntity[i].Email);
                Assert.Equal(listDTO[i].CreatedAt, listEntity[i].CreatedAt);

            }

            var userDtoCreateResult = Mapper.Map<UserCreateResultDTO>(dtoToEntity);
            Assert.Equal(userDtoCreateResult.Id, dtoToEntity.Id);
            Assert.Equal(userDtoCreateResult.Name, dtoToEntity.Name);
            Assert.Equal(userDtoCreateResult.Email, dtoToEntity.Email);
            Assert.Equal(userDtoCreateResult.CreatedAt, dtoToEntity.CreatedAt);

            var userDtoUpdateResult = Mapper.Map<UserUpdateResultDTO>(dtoToEntity);
            Assert.Equal(userDtoUpdateResult.Id, dtoToEntity.Id);
            Assert.Equal(userDtoUpdateResult.Name, dtoToEntity.Name);
            Assert.Equal(userDtoUpdateResult.Email, dtoToEntity.Email);
            Assert.Equal(userDtoUpdateResult.UpdatedAt, dtoToEntity.UpdatedAt);


            //Dto => Model
            var userModel = Mapper.Map<UserModel>(userDTO);
            Assert.Equal(userModel.Id, userDTO.Id);
            Assert.Equal(userModel.Name, userDTO.Name);
            Assert.Equal(userModel.Email, userDTO.Email);
            Assert.Equal(userModel.CreatedAt, userDTO.CreatedAt);

            var userCreateModel = Mapper.Map<UserCreateDTO>(userModel);
            Assert.Equal(userCreateModel.Name, userModel.Name);
            Assert.Equal(userCreateModel.Email, userModel.Email);

            var userUpdateModel = Mapper.Map<UserUpdateDTO>(userModel);
            Assert.Equal(userUpdateModel.Id, userModel.Id);
            Assert.Equal(userUpdateModel.Name, userModel.Name);
            Assert.Equal(userUpdateModel.Email, userModel.Email);
        }
    }
}