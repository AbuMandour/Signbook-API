using SignBookProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignBookProject.Services.Interfaces
{
    public interface IMembershipService
    {
        Task<UserModel> SignUpAsync(SignUpModel model);
        Task<UserModel> SignInAsync(SignInModel model);
        Task<bool> ForgetPasswordAsync(string phoneNumber);
        Task<bool> SetBundleAsync(string userId, string newBundle);
        Task<UserModel> IsUserExistAsync(string phoneNumber);
        Task<UserModel> GetUserAsync(string userId);

    }
}
