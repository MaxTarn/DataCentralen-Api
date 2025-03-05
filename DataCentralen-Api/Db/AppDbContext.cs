using DataCentralen_Db.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataCentralen_Api.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{


    public DbSet<Article> Articles { get; set; } = null!;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DataCentralen;Trusted_Connection=True;MultipleActiveResultSets=true");
    }
}
