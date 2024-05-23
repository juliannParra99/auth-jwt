using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drivers.Api.Configurations;
using Drivers.Api.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Drivers.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthManagementController : ControllerBase
    {
        private readonly ILogger<AuthManagementController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;


        public AuthManagementController(ILogger<AuthManagementController> logger, UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _logger = logger;
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        // user registration
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
        {
            //validate request
            if (ModelState.IsValid)
            {
                //check if email exist 
                var emailExist = await _userManager.FindByEmailAsync(requestDto.Email);

                if (emailExist != null)
                {
                    return BadRequest("Email already exist;");
                }

                // Create new user
                var newUser = new IdentityUser()
                {
                    Email = requestDto.Email
                };

                var isCreated = await _userManager.CreateAsync(newUser, requestDto.Password);

                ////return success respones if was succeded
                if (isCreated.Succeeded)
                {
                    return Ok(new RegistrationRequestResponse()
                    {
                        Result = true,
                        Token = ""

                    });
                }

                return BadRequest("Error creating the user, please try again later");

            }

            return BadRequest("Invalid request payload");
        }

    }
}