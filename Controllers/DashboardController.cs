using EventAttendance.Auth;
using EventAttendance.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventAttendance.Controllers
{
    [StaffAuthenticationFilter]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            
            return View();
        }
    }
}