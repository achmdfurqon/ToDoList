using Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public User() { }
        public User(UserVM userVM) {
            Id = userVM.Id;
            Name = userVM.Name;
            Email = userVM.Email;
            UserName = userVM.Username;
            PasswordHash = userVM.Password;
        }            
    }
}
