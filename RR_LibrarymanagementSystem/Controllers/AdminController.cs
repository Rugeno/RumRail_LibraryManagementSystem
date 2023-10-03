using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RR_LibraryManagementSystem.DataAccess.Domain;
using RR_LibraryManagementSystem.DataAccess.Interface;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RR_LibrarymanagementSystem.Controllers
{
    public class AdminController : Controller
    { 
        private readonly IBookDetail _bookDetail;

        public AdminController(IBookDetail bookDetail)
        {
            _bookDetail = bookDetail;
        }


        public IActionResult Index()
        {
            return View();
        }

        //CAR ACTIONS
        public IActionResult BookIndex()
        {
            IEnumerable<BookDetails> obj = _bookDetail.GetBookList();
            return View(obj);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View(new BookDetails_VM());
        }

        public IActionResult UpdateBook(int id)
        {
            BookDetails obj = _bookDetail.GetBookDetailsById(id);
            return View(obj);
        }

        



        [HttpPost]
        public IActionResult AddBook(BookDetails_VM obj)
        {
           if (ModelState.IsValid)
            {
            if (obj.UploadImage != null)
            {
                //Upload File
                string folder = "wwwroot/uploadfiles/";
                string fileurl = "/uploadfiles/";
                string guid = Guid.NewGuid().ToString();
                fileurl += guid + obj.UploadImage.FileName;
                folder += guid + obj.UploadImage.FileName;
                string serverFolder = Path.Combine(Directory.GetCurrentDirectory(), folder);

                obj.UploadImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                BookDetails data = new BookDetails();
                data.BookName = obj.BookName;
                data.Author = obj.Author;
                data.Description = obj.Description;
                data.UploadImage = fileurl;
                data.CreatedBy = User.Identity.Name; //User Role
                data.Stock = obj.Stock;
                string output = _bookDetail.SaveBookDetail(data);
                if (output == "SUCCESS")
                {
                    return RedirectToAction("BookIndex");
                }

            }
                return View();
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateBook(BookDetails obj)
        {
            string output = _bookDetail.UpdateBookDetail(obj);
            if (output == "SUCCESS")
            {
                return RedirectToAction("BookIndex");
            }
            return View();
        }

        public IActionResult DeleteBook(int id)
        {
            string output = _bookDetail.DeleteBookDetail(id);
            if (output == "SUCCESS")
            {
                return RedirectToAction("BookIndex");
            }
            return RedirectToAction("BookIndex");
        }


        public IActionResult Booking()
        {
            IEnumerable<BookingDetailList> obj = _bookDetail.GetBookingDetails();
            return View(obj);
        }
        public IActionResult BookBookingDetail(int id)
        {
            BookingDetailList obj = _bookDetail.GetBookingDetailById(id);
            return Json(obj);
        }

        [HttpPost]
        public IActionResult VerifyBookingDetail(int id)
        {
            string obj = _bookDetail.VerifyBookingDetailById(id);
            return Json(obj);
        }












        //    [HttpGet]
        //    public IActionResult Login()
        //    {
        //        if (User.FindFirstValue("Role") == "Admin")
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        Login_VM obj = new Login_VM();
        //        return View(obj);
        //    }

        //    [HttpPost]
        //    public async Task<ActionResult> Login(Login_VM obj)
        //    {

        //        if (!ModelState.IsValid)
        //        {
        //            return View();
        //        }
        //        else
        //        {
        //            string msg = _userAuth.CheckLogin(obj);
        //            string depatment = "";
        //            if (msg == "SUCCESS")
        //            {
        //                Portal_User usr = _userAuth.GetUserData(obj);

        //                if (usr.RoleName == "Admin" || usr.RoleName == "Staff")
        //                {
        //                    depatment = "Admin";
        //                }
        //                else
        //                {
        //                    depatment = "User";
        //                }

        //                //Creating Security Context
        //                var claims = new List<Claim>
        //                {
        //                    new Claim(ClaimTypes.Email, usr.Email),
        //                    new Claim(ClaimTypes.Name, usr.FullName),
        //                    new Claim("UserId", usr.Id.ToString()),
        //                    new Claim("Role", usr.RoleName),
        //                    new Claim("Deparment", depatment)
        //                };
        //                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
        //                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

        //                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

        //                return RedirectToAction("Index");
        //            }
        //        }
        //        return View();

        //    }
        //}
    }
}

