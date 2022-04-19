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
        //TODO make all messages with localization pattern
        //TODO search where is best place to do try and catch controller or service and for logging
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpModel model)
        {
            var user = await _membershipService.GetUserByPhoneNumberAsync(model.PhoneNumber);
            if (user != null)
                return BadRequest("a user with this phone number is already exist");

            var result = await _membershipService.SignUpAsync(model);
            if (result == null)
                
                return BadRequest("a user with this phone number is  already exist");

            return Ok(result);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel model)
        {
            var isUserExist = await _membershipService.GetUserByPhoneNumberAsync(model.phoneNumber);
            if (isUserExist == null)
                return BadRequest("no user associate with this phone number!");

            var user = await _membershipService.SignInAsync(model);
            if (user is null)
                return BadRequest("Invalid Email or Password!");
            return Ok(user);

        }

        [HttpGet("forgetPassword")]
        public async Task<IActionResult> ForgetPasswordAsyc(string phoneNumber)
        {
            var user = await _membershipService.GetUserByPhoneNumberAsync(phoneNumber);
            if (user == null)
                return BadRequest("no user for this PhoneNumber!");

            var result = await _membershipService.ForgetPasswordAsync(user);
            if (!result)
                return BadRequest("Somthing Went Wrong!");
            return Ok();
        }

        [HttpGet("setBundle")]
        public async Task<IActionResult> SetBundleAsync(string userId, string newBundle)
        {
            var user = await _membershipService.GetUserByIdAsync(userId);
            if (user == null)
                return BadRequest("no user for this Id!");
            
            var result = await _membershipService.SetBundleAsync(user.UserId, newBundle);
            if (!result)
                return BadRequest("Bundle didn't Updated!");
            return Ok();
        }

        [HttpGet("listofusers")]
        public async Task<IActionResult> GetListOfUsersAsync()
        {
            var result =await _membershipService.GetListOfUsersAsync();
            if (result == null)
                return BadRequest("No Users Founded");
            return Ok(result);
        }
        [HttpGet("getuser")]
        public async Task<IActionResult> GetUserWithoutPasswordAsync(string id)
        {
            var result = await _membershipService.GetUserWithoutPasswordAsync(id);
            if (result == null)
                return BadRequest("No user for this Id!");
            return Ok(result);
        }

        [HttpGet("modifyrole")]
        public async Task<IActionResult> ModifyUserRole(string userId, string role)
        {
            role = role.ToLower();
            if (role != "user" && role != "admin")
                return BadRequest($"the {role} role does not exist");
            var result = await _membershipService.ModifyUserRole(userId, role);
            if (result == null)
                return BadRequest("no user for this Id!");
            return Ok();
        }
    }
}
