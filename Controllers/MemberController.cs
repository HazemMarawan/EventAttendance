using EventAttendance.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventAttendance.Models;
using EventAttendance.ViewModel;
using System.IO;

namespace EventAttendance.Controllers
{
    [CustomAuthenticationFilter]
    public class MemberController : Controller
    {
        // GET: Member
        // GET: User
        EventAttendaceDbContext db = new EventAttendaceDbContext();
        // GET: User
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                // Getting all data    
                var userData = (from member in db.Members
                                select new MemberViewModel
                                {
                                    Id = member.Id,
                                    Code = member.Code,
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
                                    ImagePath = member.Image,
                                    active = member.active,
                                    Facebook = member.Facebook,
                                    Insta = member.Insta,
                                    Twitter = member.Twitter,
                                    Linkedin = member.Linkedin,
                                    Whatsapp = member.Whatsapp
                                });

                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    userData = userData.Where(m => m.FirstName.ToLower().Contains(searchValue.ToLower()) || m.Id.ToString().ToLower().Contains(searchValue.ToLower()) ||
                     m.LastName.ToLower().Contains(searchValue.ToLower()) || m.Mobile.ToLower().Contains(searchValue.ToLower()));
                }

                //total number of rows count     
                var displayResult = userData.OrderByDescending(u => u.Id).Skip(skip)
                     .Take(pageSize).ToList();
                var totalRecords = userData.Count();

                return Json(new
                {
                    draw = draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = totalRecords,
                    data = displayResult

                }, JsonRequestBehavior.AllowGet);

            }

            return View();
        }
        [HttpPost]
        public JsonResult saveMember(MemberViewModel memberVM)
        {

            if (memberVM.Id == 0)
            {
                Member member = AutoMapper.Mapper.Map<MemberViewModel, Member>(memberVM);

                member.created_at = DateTime.Now;
                if(memberVM.Image != null)
                { 
                    Guid guid = Guid.NewGuid();
                    var InputFileName = Path.GetFileName(memberVM.Image.FileName);
                    var ServerSavePath = Path.Combine(Server.MapPath("~/Members/Profile/") + guid.ToString() + "_Profile" + Path.GetExtension(memberVM.Image.FileName));
                    memberVM.Image.SaveAs(ServerSavePath);
                    member.Image = "/Members/Profile/" + guid.ToString() + "_Profile" + Path.GetExtension(memberVM.Image.FileName);
                }
                db.Members.Add(member);
                db.SaveChanges();
            }
            else
            {

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

                if (memberVM.Image != null)
                {
                    Guid guid = Guid.NewGuid();
                    var InputFileName = Path.GetFileName(memberVM.Image.FileName);
                    var ServerSavePath = Path.Combine(Server.MapPath("~/Members/Profile/") + guid.ToString() + "_Profile" + Path.GetExtension(memberVM.Image.FileName));
                    memberVM.Image.SaveAs(ServerSavePath);
                    oldMember.Image = "/Members/Profile/" + guid.ToString() + "_Profile" + Path.GetExtension(memberVM.Image.FileName);
                }
                oldMember.updated_at = DateTime.Now;
                db.Entry(oldMember).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new { message = "done" }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult deleteMember(int id)
        {
            Member deleteMember = db.Members.Find(id);
            db.Members.Remove(deleteMember);
            db.SaveChanges();

            return Json(new { message = "done" }, JsonRequestBehavior.AllowGet);
        }
    }
}