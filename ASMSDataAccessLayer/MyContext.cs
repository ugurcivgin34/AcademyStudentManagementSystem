﻿using ASMSEntityLayer.IdentityModels;
using ASMSEntityLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSDataAccessLayer
{
    public class MyContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Neighbourhood> Neighbourhoods { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseGroup> CourseGroups { get; set; }
        public virtual DbSet<StudentsCourseGroup> StudentsCourseGroups { get; set; }
        public virtual DbSet<StudentAttendance> StudentAttendances { get; set; }
        public virtual DbSet<UsersAddress> UsersAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CourseGroup>().HasIndex(cg => new { cg.PortalCode }).IsUnique(true);
            base.OnModelCreating(builder);
            //İlişki buradada kurulabilir
            //builder.Entity<District>().HasOne(d=>d.City) //Bire
            //                          .WithMany(c=>c.Districts)//Çok ilişki
            //                          .HasForeignKey(d => d.CityId)//ne üzerinden
            //                          .OnDelete(DeleteBehavior.NoAction);//hangi davrnaışla(İlçe silinemz)

            //builder.Entity<Neighbourhood>().HasOne(n => n.District) //Bire
            //                          .WithMany(d => d.Neighbourhoods)//Çok ilişki
            //                          .HasForeignKey(n => n.DistrictId)//ne üzerinden
            //                          .OnDelete(DeleteBehavior.NoAction);//hangi davrnaışla(mahalle silinemz)

        }

    }
}
