using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace EventAttendance.Helpers
{
    public class Services
    {
        public static void SendMail(string to,string subject,string body)
        {
            MailMessage mail =
                 new MailMessage(
                     System.Configuration.ConfigurationManager.AppSettings["system_mail"],
                     to,
                     subject,
                     body
                     );

            SmtpClient client = new SmtpClient("smtp.live.com", 587);
            client.UseDefaultCredentials = true;

            NetworkCredential credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["system_mail"], System.Configuration.ConfigurationManager.AppSettings["mail_password"]);

            client.Credentials = credentials;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.Send(mail);
        }
    }
}