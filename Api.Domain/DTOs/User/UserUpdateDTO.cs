using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs.User
{
    public class UserUpdateDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nome é obrigatorio")]
        [StringLength(60, ErrorMessage = "Maximo{1} caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email obrigatorio")]
        [EmailAddress(ErrorMessage = "Formato de email invalido")]
        public string Email { get; set; }
    }
}