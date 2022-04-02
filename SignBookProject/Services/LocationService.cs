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

        public LocationService(AppDbContext context)
        {
            _context = context;
        }

        //TODO get location and user with to async
        public BundleModel isEligible(PointModel point, string userId)
        {
            List<PointModel> locations = _context.Locations.ToList();
            var userIsWithinRange = false; 
            foreach (PointModel location in locations)
            {
                userIsWithinRange = WithinRange(point, location);
            }

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            return new BundleModel { isEligible = userIsWithinRange, credit = user.BundleOfMinutes, userId = userId };
        }

        private bool WithinRange(PointModel userPoint, PointModel refereancePoint)
        {
            if (refereancePoint.APoint + 20 < userPoint.APoint || refereancePoint.APoint - 20 > userPoint.APoint)
                return false;
            if (refereancePoint.LPoint + 20 < userPoint.LPoint || refereancePoint.LPoint - 20 > userPoint.LPoint)
                return false;
            return true;
        }
    }
}
