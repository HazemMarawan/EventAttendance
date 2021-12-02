using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EventAttendance.Models;
using EventAttendance.ViewModel;
using EventAttendance.VCard;
namespace EventAttendance.Controllers
{
    public class ProfileController : Controller
    {
        EventAttendaceDbContext db = new EventAttendaceDbContext();
        // GET: Guset
        public ActionResult Search()
        {
            ViewBag.errorMsg = TempData["errorMsg"];
            return View();
        }
        public ActionResult Show(string Code)
        {
            MemberViewModel selectedMember = (from member in db.Members
                                      join user in db.Users on member.Id equals user.Id
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
                                          ImagePath = user.Image,
                                          Facebook = member.Facebook,
                                          Insta = member.Insta,
                                          Twitter = member.Twitter,
                                          Linkedin = member.Linkedin,
                                          Whatsapp = member.Whatsapp,


                                      }).Where(s=>s.Code == Code).FirstOrDefault();
            //Member member = db.Members.Where(s => s.Code == Code).FirstOrDefault();
            //User user = db.Users.Find(member.Id);
          
            if (selectedMember != null)
                return View(selectedMember);
            else
            {
                TempData["errorMsg"] = "Not Found";
                return RedirectToAction("Index");
            }
        }

        public FileResult GenerateVCard(string Code)
        {
            MemberViewModel selectedMember = (from member in db.Members
                                              join user in db.Users on member.Id equals user.Id
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
                                                  ImagePath = user.Image,
                                                  Facebook = member.Facebook,
                                                  Insta = member.Insta,
                                                  Twitter = member.Twitter,
                                                  Linkedin = member.Linkedin,
                                                  Whatsapp = member.Whatsapp,


                                              }).Where(s => s.Code == Code).FirstOrDefault();
            VCard.VCard myCard = new VCard.VCard
            {
                FirstName = selectedMember.FirstName,
                Organization = selectedMember.Organization,
                JobTitle = selectedMember.JobTitle,
                StreetAddress = selectedMember.StreetAddress,
                City = selectedMember.City,
                CountryName = selectedMember.CountryName,
                Phone = selectedMember.Phone,
                Mobile = selectedMember.Mobile,
                Email = selectedMember.Email,
                HomePage = selectedMember.HomePage,
                Whatsapp = selectedMember.Whatsapp,
                Facebook = selectedMember.Facebook,
                Linkedin = selectedMember.Linkedin,
                Insta = selectedMember.Insta,
                Twitter = selectedMember.Twitter
            };
            if(!String.IsNullOrEmpty(selectedMember.ImagePath))
                myCard.Image = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~"+ selectedMember.ImagePath)));

            Guid guid = Guid.NewGuid();
            string fileName = selectedMember.FirstName + "_" + selectedMember.LastName+guid.ToString() + "_VCard.vcf";
            var ServerSavePath = Path.Combine(Server.MapPath("~/Files/VCard/") + fileName);
            using (var file = System.IO.File.OpenWrite(ServerSavePath))
            using (var writer = new StreamWriter(file))
            {
                writer.Write(myCard.ToString());
            }
            byte[] bytes = System.IO.File.ReadAllBytes(ServerSavePath);

            return File(bytes, "application/octet-stream", selectedMember.FirstName + " "+ selectedMember.LastName + "_"+selectedMember.Code+".vcf");
        }
     

    }
}