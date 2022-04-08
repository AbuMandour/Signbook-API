using Microsoft.EntityFrameworkCore;
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
        private readonly AppDbContext _context;

        public MembershipService(ICallService callService, GenerateRandomPassword generateRandomPassword, AppDbContext context)
        {
            _generateRandomPassword = generateRandomPassword;
            _callService = callService;
            _context = context;
        }
        //TODO please make i Capital to be I
        //TODO make method return user
        public async Task<UserModel> IsUserExistAsync(string phoneNumber)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            return user;
        }
        //TODO add overloading for is user exit with user id
        public async Task<UserModel> GetUserAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            return user;
        }

        public async Task<UserModel> SignUpAsync(SignUpModel model)
        {
            //TODO is user exist to be removed

            var userid = Guid.NewGuid().ToString();

            var requestModel = new CallsRequestModel
            {
                UserId = userid,
                Nickname = model.UserName,
            };

            var callUser = await _callService.CreateUserInSendBirdAsync(requestModel);

            var User = new UserModel
            {
                PhoneNumber = model.PhoneNumber,
                UserName = callUser.Nickname,
                UserId = callUser.UserId,
                BundleOfMinutes = 100,
                Password = model.Password,
                AccessToken = callUser.AccessToken
            };
            await _context.Users.AddAsync(User);
            await _context.SaveChangesAsync();
            return User;
        }

        public async Task<UserModel> SignInAsync(SignInModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.phoneNumber);

            if (user.Password != model.password)
                return null;

            return user;
        }

        public async Task<bool> ForgetPasswordAsync(string phoneNumber)
        {
            //TODO first or defualt to be removed
            var user = await _context.Users.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
            
            var newPassword = _generateRandomPassword.RandomPassword();
            user.Password = newPassword;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetBundleAsync(string userId, string newBundle)
        {
            //TODO first or defualt to be removed
            var user = await GetUserAsync(userId);
            var doubleBundle = Convert.ToDouble(newBundle);
            var set = user.BundleOfMinutes - doubleBundle;
            user.BundleOfMinutes = set;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
