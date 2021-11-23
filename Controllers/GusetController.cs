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
    public class GusetController : Controller
    {
        EventAttendaceDbContext db = new EventAttendaceDbContext();
        // GET: Guset
        public ActionResult Index()
        {
            ViewBag.errorMsg = TempData["errorMsg"];
            return View();
        }
        public ActionResult Search(string Code)
        {
            Member member = db.Members.Where(s => s.Code == Code).FirstOrDefault();
            if (member != null)
                return View(member);
            else
            {
                TempData["errorMsg"] = "Not Found";
                return RedirectToAction("Index");
            }
        }

        public FileResult GenerateVCard(string Code)
        {
            Member member = db.Members.Where(s => s.Code == Code).FirstOrDefault();
            VCard.VCard myCard = new VCard.VCard

            {
                FirstName = member.FirstName,
                Organization = member.Organization,
                JobTitle = member.JobTitle,
                StreetAddress = member.StreetAddress,
                City = member.City,
                CountryName = member.CountryName,
                Phone = member.Phone,
                Mobile = member.Mobile,
                Email = member.Email,
                HomePage = member.HomePage

            };
            if(!String.IsNullOrEmpty(member.Image))
                myCard.Image = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~"+ member.Image)));

            Guid guid = Guid.NewGuid();
            string fileName = member.FirstName + "_" + member.LastName+guid.ToString() + "_VCard.vcf";
            var ServerSavePath = Path.Combine(Server.MapPath("~/Files/VCard/") + fileName);
            using (var file = System.IO.File.OpenWrite(ServerSavePath))
            using (var writer = new StreamWriter(file))
            {
                writer.Write(myCard.ToString());
            }
            byte[] bytes = System.IO.File.ReadAllBytes(ServerSavePath);

            return File(bytes, "application/octet-stream", member.FirstName + " "+ member.LastName + "_"+member.Code+".vcf");
        }
     

    }
}