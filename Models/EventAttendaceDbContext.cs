using System;
using System.Data.Entity;
using System.Linq;

namespace EventAttendance.Models
{
    public class EventAttendaceDbContext : DbContext
    {
        // Your context has been configured to use a 'EventAttendaceDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'EventAttendance.Models.EventAttendaceDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'EventAttendaceDbContext' 
        // connection string in the application configuration file.
        public EventAttendaceDbContext()
            : base("name=EventAttendaceDbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<User> Users { get; set; }
         public virtual DbSet<Member> Members { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}