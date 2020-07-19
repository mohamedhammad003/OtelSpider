namespace OtelSpider.Core.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using OtelSpider.Core.DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OtelSpider.Core.DAL.Data.OtelSpiderContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        protected override void Seed(OtelSpider.Core.DAL.Data.OtelSpiderContext context)
        {
            var sysRoles = new List<string> { "SuperAdmin", "AppAdmin", "eCommerceManager", "eCommerceEmployee" };
            var mainReservationStatuses = new List<string> { "OK", "Confirmed", "Modified", "Cancelled", "No Show" };
            var Currecies = new List<string> { "EGP", "USD", "EUR", "GBP" };
            var mainMealPlans = new Dictionary<string, string>()
            {
                { "RO", "Room Only" },
                { "BB", "Bed and Breakfast" },
                { "HB", "Half Board" },
                { "FB", "Full Board" },
                { "AI", "All Inclusive" },
                { "UAI", "Ultra All Inclusive" },
                { "SAI", "Soft All Inclusive" }
            };

            string superAdminId = Guid.NewGuid().ToString();
            string adminId = Guid.NewGuid().ToString();

            //  This method will be called after migrating to the latest version.
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<SystemUser>(context);
            var userManager = new UserManager<SystemUser>(userStore);
            PasswordHasher passwordHasher = new PasswordHasher();
            foreach (var role in sysRoles)
            {
                //Add Roles
                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleManager.Create(new IdentityRole { Name = role });
                }
            }

            //add Users
            if (!context.Users.Any(u => u.UserName == "oteladmin"))
            {
                string hashedPassword = passwordHasher.HashPassword("adminP@ss");
                var user = new SystemUser { Id = adminId, UserName = "oteladmin", Email = "ramy.ramzy@otelspider.com", FirstName = "admin", LastName = "user", isAdmin = true, isActive = true, PasswordHash = hashedPassword, SecurityStamp = Guid.NewGuid().ToString() };
                context.Users.Add(user);
                userManager.Create(user);
                userManager.AddToRole(adminId, "AppAdmin");
                userManager.AddToRole(adminId, "SuperAdmin");
            }
            
            AddMealPlans(context, mainMealPlans);
            AddReservationStatus(context, mainReservationStatuses);
            AddCurrencies(context, Currecies);
            context.SaveChanges();
        }

        private void AddMealPlans(OtelSpider.Core.DAL.Data.OtelSpiderContext context, Dictionary<string, string> lstMealPlan)
        {
            foreach (var item in lstMealPlan)
            {
                if (!context.MealPlans.Any(x => x.Abbreviation == item.Key))
                {
                    context.MealPlans.Add(new MealPlan { Name = item.Value, Abbreviation = item.Key });
                }
            }
        }
        private void AddReservationStatus(OtelSpider.Core.DAL.Data.OtelSpiderContext context, List<string> lstStatuses)
        {
            foreach (var item in lstStatuses)
            {
                if (!context.ReservationStatuses.Any(x => x.Status == item))
                {
                    context.ReservationStatuses.Add(new ReservationStatus { Status = item });
                }
            }
        }
        private void AddCurrencies(OtelSpider.Core.DAL.Data.OtelSpiderContext context, List<string> lstCurrencies)
        {
            foreach (var item in lstCurrencies)
            {
                if (!context.Currencies.Any(x => x.CurrencyCode == item))
                {
                    context.Currencies.Add(new Currency { Name = item, CurrencyCode = item });
                }
            }
        }
    }
}
