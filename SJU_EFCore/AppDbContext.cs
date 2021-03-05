using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SJU_DataModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SJU_EFCore
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<ContactModel> Contacts { get; set; }

        public DbSet<AirportModel> Airports { get; set; }

        public DbSet<AirplaneModel> Airplanes { get; set; }
        public DbSet<AirplaneSeatingModel> AirplaneSeatings { get; set; }

    }
}
