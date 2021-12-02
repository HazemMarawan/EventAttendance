using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventAttendance.Models;
using EventAttendance.ViewModel;
using EventAttendance.Enum;

namespace EventAttendance.Controllers
{
    public class ExternalAccountController : Controller
    {
        EventAttendaceDbContext db = new EventAttendaceDbContext();
        // GET: Guest
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel user)
        {
            User currentUser = db.Users.Where(s => s.Username == user.Username && s.Password == user.Password && s.Active == 1 && s.Type == (int)UserType.Member).FirstOrDefault();
            if (currentUser != null)
            {
                Session["user_name"] = currentUser.Username;
                Session["id"] = currentUser.Id;
                Session["user"] = currentUser;
                Session["type"] = currentUser.Type;
                Session["code"] = db.Members.Find(currentUser.Id).Code;
                return RedirectToAction("Profile", "Guest");
            }
            return RedirectToAction("Index");
        }
    }
}