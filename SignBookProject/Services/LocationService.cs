using Microsoft.EntityFrameworkCore;
using SignBookProject.Data;
using SignBookProject.Models;
using SignBookProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignBookProject.Services
{
    public class LocationService : ILocationService
    {
        private readonly AppDbContext _context;
        private readonly IMembershipService _membershipService; 

        public LocationService(AppDbContext context, IMembershipService membershipService)
        {
            _context = context;
            _membershipService = membershipService;
        }

        public async Task<BundleModel> IsEligibleAsync(string latitude, string longitude, UserModel user)
        {           
            var doubleLatitude = Convert.ToDouble(latitude);
            var doubleLongitude = Convert.ToDouble(longitude);

            List<PointModel> locations = await _context.Locations.ToListAsync();
            var userIsWithinRange = false;
            foreach (PointModel location in locations)
            {
                userIsWithinRange = WithinRange(doubleLatitude, doubleLongitude, location);
                if (userIsWithinRange)
                    break;
            }

            return new BundleModel { isEligible = userIsWithinRange, credit = user.BundleOfMinutes, userId = user.UserId };
        }
        public bool WithinRange(double userAPoint, double userLPoint, PointModel refereancePoint)
        {
            if (refereancePoint.Latitude + 0.180 < userAPoint || refereancePoint.Latitude - 0.180 > userAPoint)
                return false;
            if (refereancePoint.Longitude + 0.180 < userLPoint || refereancePoint.Longitude - 0.180 > userLPoint)
                return false;
            return true;
        }
    }
}
