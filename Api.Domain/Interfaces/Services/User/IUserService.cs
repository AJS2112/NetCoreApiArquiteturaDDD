using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<UserDTO> Get(Guid id);
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserCreateResultDTO> Post(UserCreateDTO user);
        Task<UserUpdateResultDTO> Put(UserUpdateDTO user);
        Task<bool> Delete(Guid id);
    }
}