
using DTOS;
using DTOS.Dto;
using DTOS.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Service.UserService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IUserService _UserService;
        public AccountController(IUserService UserService)
        {
            _UserService = UserService;
        }
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    throw new Exception();
        //}

        [HttpPost]
        public async Task<ActionResult<OutputSignInUserDto>> SignIn(InputSignInUserDto inputSignInUserDto)  
        {

            return Ok(await _UserService.SignInAsync(inputSignInUserDto));
        }

        [HttpPost]
        public async Task<ActionResult<OutputRefreshTokenDto>> Token(InputRefreshTokenDto inputRefreshTokenDto)
        {

            OutputRefreshTokenDto outputRefreshTokenDto = await _UserService.RefreshToken(inputRefreshTokenDto);
            if (outputRefreshTokenDto == null)
            {
                return Unauthorized();
            }
            return Ok(outputRefreshTokenDto);
        }

    }
}
