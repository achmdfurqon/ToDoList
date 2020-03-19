using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Contexts;
using Data.Interfaces;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _repository;
        private readonly DataContext _context;
        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, IUserRepository repository, IConfiguration config, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                var user = _repository.Register(userVM);
                var result = user.Result;

                if (result.Succeeded)
                {
                    return Ok();
                }
                AddErrors(result);
            }
            return BadRequest(ModelState);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            _repository.Logout();
            return Ok("Logged out");
        }
    }
}