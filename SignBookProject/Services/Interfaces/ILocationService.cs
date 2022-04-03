using SignBookProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignBookProject.Services.Interfaces
{
    public interface ILocationService
    {
        Task<BundleModel> isEligibleAsync(string Apoint,string LPoint, string userId);
        Task<bool> isUserExistAsync(string userId);
    }
}
