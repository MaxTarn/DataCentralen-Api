using DataCentralen_Db.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataCentralen_Api.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Article> Articles { get; set; } = null!;
}
