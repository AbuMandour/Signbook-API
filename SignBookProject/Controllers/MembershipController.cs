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
        //TODO search where is best place to do try and catch controller or service and for logging
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpModel model)
        {
            //TODO please please use is exist user before sign up
            var result = await _membershipService.SignUpAsync(model);

            if (result is null)
                //TODO make all messages with localization pattern
                return BadRequest("a user with this phone number is  already exist");

            return Ok(result);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel model)
        {
            var isUserExist = await _membershipService.isUserExistAsync(model.phoneNumber);
            if (isUserExist == false)
                //TODO make all messages with localization pattern
                return BadRequest("no user associate with this phone number!");

            var user = await _membershipService.SignInAsync(model);
            if (user is null)
                //TODO make all messages with localization pattern
                return BadRequest("Invalid Email or Password!");
            return Ok(user);

        }

        [HttpGet("forgetPassword")]
        public async Task<IActionResult> ForgetPasswordAsyc(string phoneNumber)
        {
            //TODO please use is user exits before forget password
            var result = await _membershipService.ForgetPasswordAsync(phoneNumber);
            //TODO please use ! not is false
            if (result is false)
                //TODO make all messages with localization pattern
                return BadRequest("no user for this PhoneNumber!");
            return Ok();
        }

        [HttpGet("setBundle")]
        public async Task<IActionResult> SetBundleAsync(string userId, string newBundle)
        {
            //TODO please use is user exits before set bundle
            var result = await _membershipService.SetBundleAsync(userId, newBundle);
            //TODO please use ! not is false
            if (result is false)
                return BadRequest("no user for this phone number!");
            return Ok();
        }
    }
}
