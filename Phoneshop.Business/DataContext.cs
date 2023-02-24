using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Phoneshop.Domain.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Phoneshop.Business
{
    [ExcludeFromCodeCoverage]
    public class DataContext : IdentityDbContext<IdentityUser>//, IdentityRole, string> //DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Log> Logs { get; set; }

        //private string _connStr;

        #region Constructors
        public DataContext()
        {

        }

        // needed when calling options with dependency injection in the startup project:
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        #endregion

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // note: an ASP.NET web app will set its working directory
        //    // to be the project folder, unlike other apps.
        //    string path = "../../../../../appsettings.json";
        //    string conn = AppSettingsReader.GetAppSettings(path)
        //        .GetSection("ConnectionStrings:DatabaseConnection").Value;

        //    optionsBuilder.UseSqlServer(conn);
        //    //@"Server=(localdb)\mssqllocaldb;Database=phoneshopEF");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //"keys of Identity tables are mapped in OnModelCreating method of
            //IdentityDbContext and if this method is not called, you will end up
            //getting an error"
            base.OnModelCreating(modelBuilder); // added for microsoft identity

            modelBuilder.Entity<Phone>().Property(o => o.Price).HasPrecision(18, 2);
            // the following line is not needed (EF already ignores the function)
            //modelBuilder.Entity<Phone>().Ignore(o => o.FullName);

            List<Brand> brands = new()
            {
                new Brand { Name = "Huawei", Id = 1 },
                new Brand { Name = "Samsung", Id = 2 },
                new Brand { Name = "Apple", Id = 3 },
                new Brand { Name = "Google", Id = 4 },
                new Brand { Name = "Xiaomi", Id = 5 }
            };

            List<Phone> phones = new()
            {
                new Phone
                {
                    BrandId = 1,
                    Id = 1,
                    Type = "Type 1",
                    Description = "Test 1",
                    Price = 0,
                    Stock = 1,
                },
                new Phone
                {
                    BrandId = 1,
                    Id = 2,
                    Type = "Type 2",
                    Description = "Test 2",
                    Price = 0,
                    Stock = 1,
                },
                new Phone
                {
                    BrandId = 1,
                    Id = 3,
                    Type = "Type 3",
                    Description = "Test 3",
                    Price = 0,
                    Stock = 1,
                }
            };

            modelBuilder.Entity<Phone>().HasData(phones);
            modelBuilder.Entity<Brand>().HasData(brands);
        }
    }
}
