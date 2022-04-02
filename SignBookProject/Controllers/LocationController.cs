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

        public LocationController(ILocationService locationservice)
        {
            _locationservice = locationservice;
        }

        [HttpPost("isEligilble")]
        public BundleModel IsEligible([FromBody]PointModel point, string userId)
        {
            var result = _locationservice.isEligible(point, userId);
            return result;
        }
    }
}
