using Dapper;
using Data.Interfaces;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public readonly ConnectionStrings _connectionString;
        public UserRepository(ConnectionStrings connectionString, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _connectionString = connectionString;
        }

        public async Task<IdentityResult> Register(UserVM userVM)
        {
            User user = new User(userVM);
            var result = await _userManager.CreateAsync(user, userVM.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            return result; 
        }

        public async void Logout()
        {
           await _signInManager.SignOutAsync();
        }
    }
}
