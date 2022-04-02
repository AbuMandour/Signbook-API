using SignBookProject.Data;
using SignBookProject.Models;
using SignBookProject.Services.Interfaces;
using SignBookProject.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignBookProject.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly ICallService _callService;
        private readonly GenerateRandomPassword _generateRandomPassword;
        private readonly AppDbContext _Context;

        public MembershipService(ICallService callService,GenerateRandomPassword generateRandomPassword, AppDbContext context)
        {
            _generateRandomPassword = generateRandomPassword;
            _callService = callService;
            _Context = context;
        }

        public async Task<UserModel> SignUp(SignUpModel model)
        {
            var userid = Guid.NewGuid().ToString();
            // create request Model
            var requestModel = new CallsRequestModel
            {
                UserId = userid,
                Nickname = model.UserName,
            };
            var callUser = await _callService.CreateUserInSendBird(requestModel);

            var User = new UserModel
            {
                PhoneNumber = model.PhoneNumber,
                UserName = callUser.Nickname,
                UserId = callUser.UserId,
                BundleOfMinutes = 100,
                Password = model.Password,
                AccessToken = callUser.AccessToken
            };
            _Context.Users.Add(User);
            _Context.SaveChanges();
            return User;
        }

        public UserModel SignIn(SignInModel model)
        {
            var user = _Context.Users.FirstOrDefault(p => p.PhoneNumber == model.phoneNumber);           
            return user;
        }

        public bool ForgetPassword(string phoneNumber)
        {
            var user = _Context.Users.FirstOrDefault(p => p.PhoneNumber == phoneNumber);
            if (user is null)
                return false;
            var newPassword = _generateRandomPassword.RandomPassword();
            user.Password = newPassword;
            _Context.Users.Update(user);
            _Context.SaveChanges();
            return true;
        }

        public bool SetBundle(string userId, double newBundle)
        {
            var user = _Context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user is null)
                return false;
            var set = user.BundleOfMinutes - newBundle;
            var newset = user.BundleOfMinutes = set;
            var result = _Context.Users.Update(user);
            _Context.SaveChanges();
            return true;
        }
    }
}
