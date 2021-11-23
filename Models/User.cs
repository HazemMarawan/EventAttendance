using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventAttendance.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string image { get; set; }
        public int? type { get; set; }
        public int? active { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
    }
}