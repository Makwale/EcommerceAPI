using ecommerce.Data;
using ecommerce.Domain;
using ecommerce.DTO;
using ecommerce.Entities;
using ecommerce.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controllers
{
    public class AccountController: BaseController
    {
        private IAccountService _accountService;
        public AccountController(IAccountService accountService) {
            _accountService = accountService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> RegisterUser(UserRequest userRequest)
        {
            try
            {
                var user = await _accountService.CreateUser(userRequest);

                return Created("", user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest loginRequest)
        {
            try
            {
                var user = await _accountService.Login(loginRequest);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
