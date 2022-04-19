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
        public async Task<UserModel> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            return user;
        }
        public async Task<UserModel> GetUserByIdAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            return user;
        }
        public async Task<UserModel> SignUpAsync(SignUpModel model)
        {
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
                AccessToken = callUser.AccessToken,
                UserRole = "user"  
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
        public async Task<bool> ForgetPasswordAsync(UserModel user)
        {            
            var newPassword = _generateRandomPassword.RandomPassword();
            user.Password = newPassword;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> SetBundleAsync(string userId, string newBundle)
        {
            var user = await GetUserByIdAsync(userId);
            var doubleBundle = Convert.ToDouble(newBundle);
            var set = user.BundleOfMinutes - doubleBundle;
            user.BundleOfMinutes = set;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<ICollection<UserModel>> GetListOfUsersAsync()
        {
            ICollection<UserModel> list = await _context.Users.Select(u => new UserModel
            {
                PhoneNumber = u.PhoneNumber,
                UserId = u.UserId,
                UserName = u.UserName,
                UserRole = u.UserRole,
            }).ToListAsync();
            return list;
        }

        public async Task<UserModel> GetUserWithoutPasswordAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if(user == null)
                return null;
            return new UserModel { PhoneNumber = user.PhoneNumber, UserName = user.UserName, UserId = user.UserId, UserRole = user.UserRole };
        }

        public async Task<UserModel> ModifyUserRole(string userId, string role)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
                return null;

            user.UserRole = role;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
