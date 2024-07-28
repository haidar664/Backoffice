using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Backoffice.Models;
using System;

namespace Backoffice.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Initialize Roles

            builder.Entity<IdentityRole>().HasData(
                new { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new { Id = "2", Name = "Member", NormalizedName = "MEMBER" },
                new { Id = "3", Name = "Guide", NormalizedName = "GUIDE" }
                );

            // Initialize the Manager with credentials: Email: manager@mu.edu.lb, Password: Manager@123

            var adminID = Guid.NewGuid().ToString();

            builder.Entity<AppUser>().HasData(
                new AppUser
                {

                    Id = adminID,
                    FullName = "Suha Mneimneh",
                    UserName = "admin@ids.lb",
                    NormalizedUserName = "ADMIN@IDS.LB",
                    Email = "admin@ids.lb",
                    NormalizedEmail = "ADMIN@IDS.LB",
                    PhoneNumber = "12345678",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Gender = 'M',
                    Profession = "member",
                    Nationality = "Lebanese",
                    EmergencyPhoneNumber = "123-456-7890",
                    CreatedAt = DateTime.Now,
                    Deleted = false,
                    LockoutEnabled = false,
                    TwoFactorEnabled = false,
                    PasswordHash = "d0d03d56d98cc2e80457f179b7e07480b9d8ad7b8b60ec515bbf64b4a8e6f7bb"
                }
            );

            // Assign the User to Manager Role

            builder.Entity<IdentityUserRole<string>>().HasData(
                    new { UserId = adminID, RoleId = "1" }
                );


            //Many-To-Many table "EventsToGuides"
            builder.Entity<EventsToGuides>()
               .HasKey(r => new { r.EventId, r.GuideId });
            //Many-To-Many table "EventsToMembers"
            builder.Entity<EventsToMembers>()
               .HasKey(r => new { r.EventId, r.MemberId });

        }

        public DbSet<EventModel> Models { get; set; }
        public DbSet<LookUpModel> Lookups { get; set; }
        public DbSet<EventsToGuides> EventsToGuides { get; set; }
        public DbSet<EventsToMembers> EventsToMembers { get; set; }


    }
}
