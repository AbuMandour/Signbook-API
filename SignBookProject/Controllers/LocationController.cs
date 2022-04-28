using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationservice;
        private readonly IMembershipService _membershipService;

        public LocationController(ILocationService locationservice, IMembershipService membershipService)
        {
            _locationservice = locationservice;
            _membershipService = membershipService;
        }

        [HttpGet("isEligible")]
        public async Task<IActionResult> IsEligibleAsync(string latitude , string longitude, string userId)
        {
            var user = await _membershipService.GetUserByIdAsync(userId);
            if (user == null)
                return BadRequest("no user associated with this id!");
            var result = await _locationservice.IsEligibleAsync(latitude, longitude, user);
            return Ok(result);
        }
    }
}
