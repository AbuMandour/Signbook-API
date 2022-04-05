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

        public LocationService(AppDbContext context)
        {
            _context = context;
        }

        //TODO please use user rather than userId
        public async Task<BundleModel> isEligibleAsync(string APoint, string LPoint, string userId)
        {
            //TODO first or default to be removed
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            var doubleAPoint = Convert.ToDouble(APoint);
            var doubleLPoint = Convert.ToDouble(LPoint);

            //TODO to be async
            List<PointModel> locations = _context.Locations.ToList();
            var userIsWithinRange = false;
            foreach (PointModel location in locations)
            {
                userIsWithinRange = WithinRange(doubleAPoint, doubleLPoint, location);
                //TODO please use break
                if (userIsWithinRange is true)
                    return new BundleModel { isEligible = userIsWithinRange, credit = user.BundleOfMinutes, userId = userId };
            }

            return new BundleModel { isEligible = userIsWithinRange, credit = user.BundleOfMinutes, userId = userId };
        }
        //TODO to be moved to membership service
        public async Task<bool> isUserExistAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            return user is not null ? true : false;
        }

        private bool WithinRange(double userAPoint, double userLPoint, PointModel refereancePoint)
        {
            if (refereancePoint.APoint + 20 < userAPoint || refereancePoint.APoint - 20 > userAPoint)
                return false;
            if (refereancePoint.LPoint + 20 < userLPoint || refereancePoint.LPoint - 20 > userLPoint)
                return false;
            return true;
        }
    }
}
