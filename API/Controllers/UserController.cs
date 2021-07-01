
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Filters;
using DTOS.Dto;
using DTOS.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Service.UserService;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
  
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;
        public UserController(IUserService UserService)
        {
            _UserService = UserService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OutputUserDto>>> GetAllUsers()
        {
            return Ok(await _UserService.GetAllUsers());
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddUser(CreateUserDto createUserDto)
        {

            return Ok(await _UserService.CreateUser(createUserDto));
        }


        [HttpPost]
        public async Task<ActionResult<bool>> UpdateUser(UpdateUserDto updateUserDto)
        {
            return Ok(await _UserService.UpdateUser(updateUserDto));

        }
        [HttpPost]
        public async Task<ActionResult<bool>> DeleteUser(int Id)
        {
            return Ok(await _UserService.DeleteUser(Id));
        }

    }
}
