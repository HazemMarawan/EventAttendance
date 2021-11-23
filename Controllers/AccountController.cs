using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            User currentUser = db.Users.Where(s => s.user_name == user.user_name && s.password == user.password && s.active == 1).FirstOrDefault();
            if (currentUser != null)
            {
                Session["user_name"] = currentUser.user_name;
                Session["id"] = currentUser.id;
                Session["user"] = currentUser;

                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}