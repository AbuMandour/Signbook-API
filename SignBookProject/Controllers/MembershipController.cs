using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignBookProject.Data;
using SignBookProject.Models;
using SignBookProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignBookProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody]SignUpModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.PhoneNumber == model.PhoneNumber);
            if (user is not null)
                return BadRequest("a user with the same PhoneNumber is already exist!");

            var result = await _membershipService.SignUp(model);

            return Ok(result);
        }

        [HttpPost("signin")]
        public IActionResult SignIn(SignInModel model)
        {
            var user = _membershipService.SignIn(model);
            if (user.PhoneNumber is null)
                return BadRequest("Wrong PhoneNumber or Password!");
            return Ok(user);

        }

        [HttpGet("forgetPassword")]
        public IActionResult ForgetPassword(string phoneNumber)
        {
            var result = _membershipService.ForgetPassword(phoneNumber);
            if (result == false)
                return BadRequest("no user for this PhoneNumber!");
            return Ok();
        }

        [HttpGet("setBundle")]
        public IActionResult SetBundle(string userId, double newBundle)
        {
            var result = _membershipService.SetBundle(userId, newBundle);
            if (result is false)
                return BadRequest("user bundle of minutes did not updated!");
            return Ok();
        }
    }
}
