using DataCentralen_Db.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataCentralen_Api.DbContext;

public class AppDbContext(DbContextOptions<AppDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Article>()
            .HasOne(a => a.ArticleContent)
            .WithOne(ac => ac.Article)
            .HasForeignKey<Article>(a => a.ArticleContentId)
            .OnDelete(DeleteBehavior.Restrict); // no cascade delete, in both directions

        //always include the articleContent when fetching an article, when it is not null
        modelBuilder.Entity<Article>()
            .Navigation(a => a.ArticleContent)
            .AutoInclude();
    }

    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<ArticleContentModel> ArticleContents { get; set; } = null!;
    public DbSet<AppUser> Users { get; set; } = null!;
}
