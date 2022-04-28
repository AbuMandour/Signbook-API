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
        Task<bool> ForgetPasswordAsync(UserModel user);
        Task<bool> SetBundleAsync(string userId, string newBundle);
        Task<UserModel> GetUserByPhoneNumberAsync(string phoneNumber);
        Task<UserModel> GetUserByIdAsync(string userId);
        Task<ICollection<UserModel>> GetListOfAdminsAsync();
        Task<UserModel> GetUserWithoutPasswordAsync(string userId);
        Task<UserModel> ModifyUserRole(string userId, string role);

    }
}
