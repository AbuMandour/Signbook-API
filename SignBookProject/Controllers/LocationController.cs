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
        [HttpPost("addPoint")]
        public async Task<IActionResult> AddPointAsync([FromBody] PointModel point)
        {
            if (point.Name == null || point.Latitude == 0 || point.Longitude == 0) return BadRequest("please set all the point values");

            var result = await _locationservice.AddPointAsync(point);

            if (result == null) return BadRequest("something went wrong!");

            return Ok(result);
        }




        [HttpPut("updatePoint")]
        public async Task<IActionResult> UpdatePointAsync([FromBody] PointModel point, string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("please assign the id of this point");
            if (string.IsNullOrEmpty(point.Name) || point.Latitude == 0 || point.Longitude == 0) return BadRequest("please set all the point values");

            var result = await _locationservice.UpdatePointAsync(point, id);

            if (result == null) return NotFound("no point for this id!");
            
            return Ok(result);
        }

        [HttpDelete("deletePoint")]
        public async Task<IActionResult> DeletePointAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("please assign the id of this point");

            var result = await _locationservice.DeletePointAsync(id);

            if (result == null) return NotFound("no point for this id!");

            return Ok(result);
        }
    }
}
