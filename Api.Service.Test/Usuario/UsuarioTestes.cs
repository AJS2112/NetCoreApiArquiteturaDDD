using System;
using System.Collections.Generic;
using Api.Domain.DTOs.User;

namespace Api.Service.Test.Usuario
{
    public class UsuarioTestes
    {
        public static string NomeUsuario { get; set; }
        public static string EmailUsuario { get; set; }
        public static string NomeUsuarioAlterado { get; set; }
        public static string EmailUsuarioAlterado { get; set; }
        public static Guid IdUsuario { get; set; }


        public List<UserDTO> listaUserDTO = new List<UserDTO>();
        public UserDTO userDTO;
        public UserCreateDTO userCreateDTO;
        public UserCreateResultDTO userCreateResultDTO;
        public UserUpdateDTO userUpdateDTO;
        public UserUpdateResultDTO userUpdateResultDTO;

        public UsuarioTestes()
        {
            IdUsuario = Guid.NewGuid();
            NomeUsuario = Faker.Name.FullName();
            EmailUsuario = Faker.Internet.Email();
            NomeUsuarioAlterado = Faker.Name.FullName();
            EmailUsuarioAlterado = Faker.Internet.Email();

            for (int i = 0; i < 10; i++)
            {
                var dto = new UserDTO()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };

                listaUserDTO.Add(dto);
            }

            userDTO = new UserDTO
            {
                Id = IdUsuario,
                Name = NomeUsuario,
                Email = EmailUsuario
            };

            userCreateDTO = new UserCreateDTO
            {
                Name = NomeUsuario,
                Email = EmailUsuario
            };

            userCreateResultDTO = new UserCreateResultDTO
            {
                Id = IdUsuario,
                Name = NomeUsuario,
                Email = EmailUsuario,
                CreateAt = DateTime.UtcNow
            };

            userUpdateDTO = new UserUpdateDTO
            {
                Id = IdUsuario,
                Name = NomeUsuario,
                Email = EmailUsuario
            };

            userUpdateResultDTO = new UserUpdateResultDTO
            {
                Id = IdUsuario,
                Name = NomeUsuario,
                Email = EmailUsuario,
                UpdateAt = DateTime.UtcNow
            };


        }
    }
}