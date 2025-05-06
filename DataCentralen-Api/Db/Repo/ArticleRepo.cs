using DataCentralen_Api.DbContext;

using DataCentralen_Db.Models.DbModels;
using DataCentralen_Db.Models.DTOModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCentralen_Db.Repo;

public class ArticleRepo(AppDbContext context)
{
    private readonly AppDbContext _context = context;


    public async Task<IEnumerable<Article>> GetAllAsync()
    {
        return await _context.Articles.ToListAsync();
    }

    public async Task<Article?> GetByIdAsync(int id)
    {
        return await _context.Articles.FindAsync(id);
    }

    public async Task AddAsync(Article article)
    {
        await _context.Articles.AddAsync(article);
        await _context.SaveChangesAsync();
    }

    public IQueryable<ArticleTitleAndDescription> GetArticleDTO()
    {
        IQueryable<ArticleTitleAndDescription> articles = _context.Articles.Where(article => article.Description.Length > 0).Select(article => new ArticleTitleAndDescription() { Id = article.Id, Title = article.Title, Description = article.Description });

        return articles;
    }

    public IQueryable<ArticleCardDisplay> GetArticleCardDTO()
    {
        IQueryable<ArticleCardDisplay> articles = _context.Articles.Where(article => article.Description.Length > 0).Select(article => new ArticleCardDisplay() { Id = article.Id, Title = article.Title, Description = article.Description, Type = article.Type, ColorCodeOne = article.ColorCodeOne, ColorCodeTwo = article.ColorCodeTwo });

        return articles;
    }

    public async Task UpdateAsync(Article article)
    {
        _context.Articles.Update(article);
        _context.Entry(article).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateLikesAsync(int id, bool increment)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article != null)
        {
            article.Likes += increment ? 1 : -1;
            if (article.Likes < 0) article.Likes = 0;
            _context.Entry(article).Property(a => a.Likes).IsModified = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article != null)
        {
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }
    }
}
