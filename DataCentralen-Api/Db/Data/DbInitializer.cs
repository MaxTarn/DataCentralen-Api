using DataCentralen_Api.DbContext;
using DataCentralen_Db.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DataCentralen_Api.Db.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        try
        {


    
            if (!context.Users.Any())
            {
                var adminUser = new AppUser
                {
                    UserName = "admin",
                    IsAdmin = true,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123")
                };

                context.Users.Add(adminUser);
                context.SaveChanges();
            }


        }
        catch
        {

            throw;
        }
    }
}