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
            var user = await _membershipService.IsUserExistAsync(model.PhoneNumber);
            if (user != null)
                return BadRequest("a user with this phone number is already exist");

            var result = await _membershipService.SignUpAsync(model);
            if (result == null)
                //TODO make all messages with localization pattern
                return BadRequest("a user with this phone number is  already exist");

            return Ok(result);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel model)
        {
            var isUserExist = await _membershipService.IsUserExistAsync(model.phoneNumber);
            if (isUserExist != null)
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
            var user = await _membershipService.IsUserExistAsync(phoneNumber);
            if (user == null)
                //TODO make all messages with localization pattern
                return BadRequest("no user for this PhoneNumber!");

            var result = await _membershipService.ForgetPasswordAsync(user.PhoneNumber);
            //TODO please use ! not is false
            if (!result)
                return BadRequest("Somthing Went Wrong!");
            return Ok();
        }

        [HttpGet("setBundle")]
        public async Task<IActionResult> SetBundleAsync(string userId, string newBundle)
        {
            //TODO please use is user exits before set bundle
            var user = await _membershipService.GetUserAsync(userId);
            if (user == null)
                return BadRequest("no user for this phone number!");
            
            //TODO please use ! not is false
            var result = await _membershipService.SetBundleAsync(user.UserId, newBundle);
            if (!result)
                return BadRequest("Bundle not Updated!");
            return Ok();
        }
    }
}
