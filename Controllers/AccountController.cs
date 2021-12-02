using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventAttendance.Enum;
using EventAttendance.Models;
namespace EventAttendance.Controllers
{
    public class AccountController : Controller
    {
        EventAttendaceDbContext db = new EventAttendaceDbContext();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            User currentUser = db.Users.Where(s => s.Username == user.Username && s.Password == user.Password && s.Active == 1 && s.Type == (int)UserType.Staff).FirstOrDefault();
            if (currentUser != null)
            {
                Session["user_name"] = currentUser.Username;
                Session["id"] = currentUser.Id;
                Session["user"] = currentUser;
                Session["type"] = currentUser.Type;
                return RedirectToAction("Index", "Dashboard");
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}