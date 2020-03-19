using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        //[HttpGet]
        //public ActionResult Logout()
        //{
        //    Client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("Token"));
        //    HttpContext.Session.Remove("Id");
        //    return RedirectToAction(nameof(Index));
        //}
    }
}