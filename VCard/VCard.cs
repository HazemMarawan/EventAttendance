﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EventAttendance.VCard
{
    public class VCard
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string JobTitle { get; set; }
        public string StreetAddress { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string CountryName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string HomePage { get; set; }
        public string Facebook { get; set; }
        public string Insta { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public string Whatsapp { get; set; }
        public byte[] Image { get; set; }
        public override string ToString()
        {

            var builder = new StringBuilder();
            builder.AppendLine("BEGIN:VCARD");
            builder.AppendLine("VERSION:2.1");
            // Name
            builder.AppendLine("N:" + LastName + ";" + FirstName);
            // Full name
            builder.AppendLine("FN:" + FirstName + " " + LastName);
            // Address
            builder.Append("ADR;HOME;PREF:;;");
            builder.Append(StreetAddress + ";");
            builder.Append(City + ";;");
            builder.Append(Zip + ";");
            builder.Append(City + ";;");
            builder.Append(Facebook + ";");
            builder.Append(Whatsapp + ";;");
            builder.Append(Linkedin + ";;");
            builder.Append(Insta + ";;");
            builder.Append(Twitter + ";;");
            builder.AppendLine(CountryName);
            // Other data
            builder.AppendLine("ORG:" + Organization);
            builder.AppendLine("TITLE:" + JobTitle);
            builder.AppendLine("TEL;HOME;VOICE:" + Phone);
            builder.AppendLine("TEL;CELL;VOICE:" + Mobile);
            builder.AppendLine("URL;" + HomePage);
            builder.AppendLine("EMAIL;PREF;INTERNET:" + Email);
            builder.AppendLine("END:VCARD");
            return builder.ToString();
        }

    }
}