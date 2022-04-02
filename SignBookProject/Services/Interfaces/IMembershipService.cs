using SignBookProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignBookProject.Services.Interfaces
{
    public interface IMembershipService
    {
        Task<UserModel> SignUp(SignUpModel model);
        UserModel SignIn(SignInModel model);
        bool ForgetPassword(string phoneNumber);
        bool SetBundle(string userId, double newBundle);

    }
}
