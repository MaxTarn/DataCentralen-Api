using DataCentralen_Db.Models.DbModels;
using DataCentralen_Db.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataCentralen_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController(ArticleRepo articleRepo) : ControllerBase
{
    private readonly ArticleRepo _articleRepo = articleRepo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> GetAll()
    {
        var articles = await _articleRepo.GetAllAsync();
        return Ok(articles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> GetById(int id)
    {
        var article = await _articleRepo.GetByIdAsync(id);
        if (article == null)
        {
            return NotFound();
        }
        return Ok(article);
    }


    [HttpGet("TitleDescription")]
    public ActionResult<Article> GetTitleDescription()
    {
        var articles = _articleRepo.GetArticleDTO();

        return Ok(articles);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Article>> Add(Article article)
    {
        await _articleRepo.AddAsync(article);
        return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Article>> Update(int id, Article article)
    {
        if (id != article.Id)
        {
            return BadRequest();
        }
        await _articleRepo.UpdateAsync(article);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Article>> Delete(int id)
    {
        await _articleRepo.DeleteAsync(id);
        return NoContent();
    }
}
