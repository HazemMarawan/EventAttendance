using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventAttendance.Models;
using EventAttendance.ViewModel;
using EventAttendance.Enum;
using EventAttendance.Auth;
using System.IO;

namespace EventAttendance.Controllers
{
    [GuestAuthenticationFilter]
    public class GuestController : Controller
    {
        EventAttendaceDbContext db = new EventAttendaceDbContext();
        // GET: Guest
        public ActionResult Profile()
        {
            int UserId = Convert.ToInt32(Session["id"].ToString());
            MemberViewModel selectedMember = (from member in db.Members
                                              join user in db.Users on member.Id equals user.Id
                                              select new MemberViewModel
                                              {
                                                  Id = member.Id,
                                                  Code = member.Code,
                                                  Username = user.Username,
                                                  Password = user.Password,
                                                  FirstName = member.FirstName,
                                                  LastName = member.LastName,
                                                  Organization = member.Organization,
                                                  JobTitle = member.JobTitle,
                                                  StreetAddress = member.StreetAddress,
                                                  Zip = member.Zip,
                                                  City = member.City,
                                                  CountryName = member.CountryName,
                                                  Phone = member.Phone,
                                                  Mobile = member.Mobile,
                                                  Email = member.Email,
                                                  HomePage = member.HomePage,
                                                  ImagePath = user.Image,
                                                  Active = member.Active,
                                                  Facebook = member.Facebook,
                                                  Insta = member.Insta,
                                                  Twitter = member.Twitter,
                                                  Linkedin = member.Linkedin,
                                                  Whatsapp = member.Whatsapp


                                              }).Where(s => s.Id == UserId).FirstOrDefault();
            return View(selectedMember);
        }

        [HttpPost]
        public JsonResult Edit (MemberViewModel memberVM)
        {
                User user = db.Users.Find(memberVM.Id);
                user.Username = memberVM.Username;
                user.Password = memberVM.Password;

                if (memberVM.Image != null)
                {
                    Guid guid = Guid.NewGuid();
                    var InputFileName = Path.GetFileName(memberVM.Image.FileName);
                    var ServerSavePath = Path.Combine(Server.MapPath("~/Members/Profile/") + guid.ToString() + "_Profile" + Path.GetExtension(memberVM.Image.FileName));
                    memberVM.Image.SaveAs(ServerSavePath);
                    user.Image = "/Members/Profile/" + guid.ToString() + "_Profile" + Path.GetExtension(memberVM.Image.FileName);
                }

                db.SaveChanges();

                Member oldMember = db.Members.Find(memberVM.Id);
                oldMember.FirstName = memberVM.FirstName;
                oldMember.LastName = memberVM.LastName;
                oldMember.Organization = memberVM.Organization;
                oldMember.JobTitle = memberVM.JobTitle;
                oldMember.StreetAddress = memberVM.StreetAddress;
                oldMember.Zip = memberVM.Zip;
                oldMember.City = memberVM.City;
                oldMember.CountryName = memberVM.CountryName;
                oldMember.Phone = memberVM.Phone;
                oldMember.Mobile = memberVM.Mobile;
                oldMember.Email = memberVM.Email;
                oldMember.HomePage = memberVM.HomePage;
                oldMember.Facebook = memberVM.Facebook;
                oldMember.Twitter = memberVM.Twitter;
                oldMember.Insta = memberVM.Insta;
                oldMember.Linkedin = memberVM.Twitter;
                oldMember.Whatsapp = memberVM.Whatsapp;
                oldMember.UpdatedAt = DateTime.Now;
                oldMember.Active = memberVM.Active;

                db.SaveChanges();

            return Json(new { message = "done" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "ExternalAccount");
        }
    }
}