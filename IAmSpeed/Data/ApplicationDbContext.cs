using System;
using System.Collections.Generic;
using System.Text;
using IAmSpeed.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IAmSpeed.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Segment> Segments { get; set; }


    }
}
