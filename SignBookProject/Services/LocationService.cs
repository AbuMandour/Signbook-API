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

        //TODO please use user rather than userId
        public async Task<BundleModel> IsEligibleAsync(string APoint, string LPoint, UserModel user)
        {
            //TODO first or default to be removed
            

            var doubleAPoint = Convert.ToDouble(APoint);
            var doubleLPoint = Convert.ToDouble(LPoint);

            //TODO to be async
            List<PointModel> locations = await _context.Locations.ToListAsync();
            var userIsWithinRange = false;
            foreach (PointModel location in locations)
            {
                userIsWithinRange = WithinRange(doubleAPoint, doubleLPoint, location);
                //TODO please use break
                if (userIsWithinRange)
                    break;
            }

            return new BundleModel { isEligible = userIsWithinRange, credit = user.BundleOfMinutes, userId = user.UserId };
        }
        //TODO to be moved to membership service


        public bool WithinRange(double userAPoint, double userLPoint, PointModel refereancePoint)
        {
            if (refereancePoint.APoint + 20 < userAPoint || refereancePoint.APoint - 20 > userAPoint)
                return false;
            if (refereancePoint.LPoint + 20 < userLPoint || refereancePoint.LPoint - 20 > userLPoint)
                return false;
            return true;
        }
    }
}
