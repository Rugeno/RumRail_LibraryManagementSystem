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
        private readonly IBookDetail _bookDetail;

        public UserController(IUserAuth userAuth,IBookDetail bookDetail)
        {
            _userAuth = userAuth;
            _bookDetail = bookDetail;

        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Dashboard()
        {
            int userid = Int32.Parse(User.FindFirst("UserId").Value);
            IEnumerable<BookingDetailList> obj = _bookDetail.GetBookingDetailsOfUser(userid);
            return View(obj);
        }

        public IActionResult Books()
        {
            IEnumerable<BookDetails> obj = _bookDetail.GetActiveBookList();
            return View(obj);
        }

        [HttpGet]
        public IActionResult Signup()
        {
            ViewBag.Message = null;
            Signup_VM obj = new Signup_VM();
            return View(obj);
        }


        [HttpPost]
        public IActionResult Signup(Signup_VM obj)
        {
            if (ModelState.IsValid)
            {
                string msg = _userAuth.CheckEmailExist(obj.Email);
                if (msg == "SUCCESS")
                {
                    ViewBag.Message = "Email already Exist !!!";
                    return View();
                }

                if (obj.Password != obj.ConfirmPassword)
                {
                    ViewBag.Message = "Password Doesnot Match !!!";
                    return View();
                }

                Save_PortalUser data = new Save_PortalUser();
                data.FullName = obj.FullName;
                data.Email = obj.Email;
                data.PhoneNo = obj.PhoneNo;
                data.Password = obj.Password;
                data.Role = 2; //User Role

                string output = _userAuth.SaveUserData(data);
                if (output == "SUCCESS")
                {
                    return RedirectToAction("Index");
                }
            }
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
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Index");

        }

        public IActionResult BookDetails(int id)
        {
           
            BookDetails obj = _bookDetail.GetBookDetailsById(id);
            return View(obj);
        }

       

        [HttpPost]
        public IActionResult BookBooking(BookingDetail obj)
        {
            string output = _bookDetail.SaveBooking(obj);
            if (output == "SUCCESS")
            {
                return Json(new { data = "Success" });
            }
            return Json(new { data = "Something Went Wrong !!!" });
        }

        public IActionResult ReturnBooking(int id)
        {
            string obj = _bookDetail.ReturnBookingById(id);
            return RedirectToAction("Dashboard");
        }
    }
   
}
