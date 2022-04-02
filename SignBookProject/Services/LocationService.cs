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

        public bool WithinRange(PointModel model)
        {
            List<PointModel> locations = _context.Locations.ToList();
            foreach (var Name in locations)
            {
                if (Name.APoint + 20 < model.APoint || Name.APoint - 20 > model.APoint)
                    return false;
                if (Name.LPoint + 20 < model.LPoint || Name.LPoint - 20 > model.LPoint)
                    return false;
            }
            return true;
        }

        public BundleModel isEligible(PointModel point, string userId)
        {
            var checkLocation = WithinRange(point);
            if (checkLocation is false)
                return new BundleModel { isEligible = false };

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user.BundleOfMinutes == 0)
                return new BundleModel { isEligible = false, credit = user.BundleOfMinutes, userId=userId };

            return new BundleModel { isEligible = true, credit = user.BundleOfMinutes, userId = userId };
        }
    }
}
