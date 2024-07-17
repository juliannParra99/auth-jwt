using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Drivers.Api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Drivers.Api.Controllers.RoleManagement
{
    //within the API. It provides endpoints to retrieve all claims for a specific user // based on their email and to add new claims to a user.
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimsSetupController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<SetupController> _logger;

        public ClaimsSetupController(
            ApiDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            ILogger<SetupController> logger)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClaims(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(new { error = "Unable to find user" });
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            return Ok(userClaims);

        }

        [HttpPost]
        public async Task<IActionResult> AddClaimsToUser(string email, string claimName, string claimValue)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(new { error = "Unable to find user" });
            }

            var userClaim = new Claim(claimName, claimValue);

            var result = await _userManager.AddClaimAsync(user, userClaim);

            if (result.Succeeded)
            {
                return Ok(new { result = $"user {user.Email} has a claim {claimName} added " });
            }
            else
            {
                return BadRequest(new { error = $"Unable toa add claim {claimName} to the user {user.Email}" });
            }

        }
    }
}