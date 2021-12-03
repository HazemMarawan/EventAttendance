using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventAttendance.Models;
using EventAttendance.ViewModel;
using EventAttendance.Enum;
using System.IO;

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

        [HttpPost]
        public ActionResult Register(MemberViewModel memberVM)
        {
           
            User user = new User();
            user.Username = memberVM.Username;
            user.Password = memberVM.Password;
            user.Type = (int)UserType.Member;
            user.CreatedAt = DateTime.Now;
            user.Active = 1;

            //if (memberVM.Image != null)
            //{
            //    Guid guid = Guid.NewGuid();
            //    var InputFileName = Path.GetFileName(memberVM.Image.FileName);
            //    var ServerSavePath = Path.Combine(Server.MapPath("~/Members/Profile/") + guid.ToString() + "_Profile" + Path.GetExtension(memberVM.Image.FileName));
            //    memberVM.Image.SaveAs(ServerSavePath);
            //    user.Image = "/Members/Profile/" + guid.ToString() + "_Profile" + Path.GetExtension(memberVM.Image.FileName);
            //}


            db.Users.Add(user);
            db.SaveChanges();

            user.CreatedBy = user.Id;
            db.SaveChanges();

            Member member = AutoMapper.Mapper.Map<MemberViewModel, Member>(memberVM);
            member.Id = user.Id;
            member.CreatedAt = DateTime.Now;
            member.CreatedBy = user.Id;
            member.Active = 1;
            db.Members.Add(member);
            db.SaveChanges();

            Session["user_name"] = user.Username;
            Session["id"] = user.Id;
            Session["user"] = user;
            Session["type"] = user.Type;
            //Session["code"] = db.Members.Find(user.Id).Code;
            return RedirectToAction("Profile", "Guest");
            
        }
        public JsonResult checkUsername(string Username)
        {
            if(db.Users.Any(s=>s.Username == Username) || String.IsNullOrEmpty(Username))
                return Json(new { Valid = false }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Valid = true }, JsonRequestBehavior.AllowGet);
        }
    }
}