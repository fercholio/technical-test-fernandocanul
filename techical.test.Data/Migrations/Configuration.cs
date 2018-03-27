namespace techical.test.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using techical.test.Utils;
    using technical.test.Model;

    public sealed class Configuration : DbMigrationsConfiguration<techical.test.Data.Contexts.TechnicalTestContext>
    {
        public Configuration()
        {
            //set true just for development
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(techical.test.Data.Contexts.TechnicalTestContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            
            context.Users.AddOrUpdate(
               u => u.Username,
               new User
               {
                   Enabled = true,
                   FirstName = "Fernando",
                   LastName = "Canul",
                   Role = Role.Admin,
                   Username = "fcanul@technical.com",
                   PasswordConfirmation = PasswordGenerator.EncryptPassword("Fernando1$"),
                   Password = PasswordGenerator.EncryptPassword("Fernando1$"),
                   Created = DateTime.Now,
                   UserData = new UserData { BirthDate = DateTime.Now.AddYears(-40), Gender = Gender.Unspecified, Email = "fcanul@technical.com" }
               }
           );

            context.Users.AddOrUpdate(
               u => u.Username,
               new User
               {
                   Enabled = true,
                   FirstName = "Guillermo",
                   LastName = "Centeno",
                   Role = Role.ApplicationUser,
                   Username = "gcenteno@technical.com",
                   PasswordConfirmation = PasswordGenerator.EncryptPassword("Fernando1$"),
                   Password = PasswordGenerator.EncryptPassword("Fernando1$"),
                   Created = DateTime.Now,
                   UserData = new UserData { BirthDate = DateTime.Now.AddYears(-40), Gender = Gender.Male, Email = "gcenteno@technical.com" }
               }
           );

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }

            base.Seed(context);
        }
    }
}
