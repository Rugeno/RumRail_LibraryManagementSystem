using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RR_LibraryManagementSystem.DataAccess.Domain;
using RR_LibraryManagementSystem.DataAccess.Interface;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RR_LibrarymanagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAuth _userAuth;

        public UserController(IUserAuth userAuth)
        {
            _userAuth = userAuth;
           
        }
        public IActionResult Index()
        {
        
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            Login_VM obj = new Login_VM();
            return View(obj);
        }

        [HttpPost]
        public async Task<ActionResult> Login(Login_VM obj)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Invalid Form Input !!!";
                return View();
            }
            else
            {
                string msg = _userAuth.CheckLogin(obj);
                string depatment = "";
                if (msg == "SUCCESS")
                {
                    Portal_User usr = _userAuth.GetUserData(obj);

                    if (usr.RoleName == "Admin" || usr.RoleName == "Staff")
                    {
                        depatment = "Admin";
                    }
                    else
                    {
                        depatment = "User";
                    }

                    //Creating Security Context
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, usr.Email),
                        new Claim(ClaimTypes.Name, usr.FullName),
                        new Claim("UserId", usr.Id.ToString()),
                        new Claim("Role", usr.RoleName),
                        new Claim("Deparment", depatment)
                    };
                    var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                    return RedirectToAction("Index");
                }
                ViewBag.Message = "Username and Password doesnot match or donot exist !!!";
            }
            return View();

        }

    }
}

