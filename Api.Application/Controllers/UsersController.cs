using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var users = await _service.GetAll();
                return Ok(users);
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetWithId")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _service.Get(id);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }

        }
    }
}