using SignBookProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignBookProject.Services.Interfaces
{
    public interface ILocationService
    {
        Task<BundleModel> IsEligibleAsync(string latitude, string longitude, UserModel user);
        bool WithinRange(double userAPoint, double userLPoint, PointModel refereancePoint);
    }
}
