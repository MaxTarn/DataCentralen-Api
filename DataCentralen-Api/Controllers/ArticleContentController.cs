using DataCentralen_Api.DbContext;
using DataCentralen_Db.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataCentralen_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleContentController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ArticleContentModel>>> GetAll()
    {
        try
        {
            return Ok(await context.ArticleContents.ToListAsync());
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the article.");
        }
    }

    [HttpGet("id/{Id}")]
    public async Task<ActionResult<ArticleContentModel>> GetById(int? Id)
    {
        if (Id == null)
        {
            return BadRequest("Id cannot be null");
        }
        try
        {
            var articleContent = await context.ArticleContents.FirstOrDefaultAsync(article => article.Id == Id);
            if (articleContent == null)
            {
                return NotFound("Article content not found");
            }
            return Ok(articleContent);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the article.");
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ArticleContentModel>> Create([FromBody] ArticleContentModel newContent)
    {
        if (newContent == null)
        {
            return BadRequest("Article content cannot be null");
        }

        newContent.ArticleId = null;
        newContent.Article = null;
        try
        {
            context.ArticleContents.Add(newContent);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { Id = newContent.Id }, newContent);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the article content.");
        }
    }
    [Authorize]
    [HttpPut("{Id}")]
    public async Task<ActionResult> Update(int Id, [FromBody] ArticleContentModel updatedContent)
    {
        if (updatedContent == null || Id != updatedContent.Id)
        {
            return BadRequest("Invalid Id");
        }

        try
        {
            var existingContent = await context.ArticleContents.FirstOrDefaultAsync(article => article.Id == Id);
            if (existingContent == null)
            {
                return NotFound("Article content not found");
            }

            existingContent.Content = updatedContent.Content;
            existingContent.ArticleId = updatedContent.ArticleId;

            context.ArticleContents.Update(existingContent);
            await context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the article content.");
        }
    }

    [Authorize]
    [HttpDelete("{Id}")]
    public async Task<ActionResult> Delete(int Id)
    {
        if (Id == null)
        {
            return BadRequest("Id cannot be null");
        }
        try
        {
            var articleContent = await context.ArticleContents.FirstOrDefaultAsync(article => article.Id == Id);
            if (articleContent == null)
            {
                return NotFound("Article content not found");
            }
            context.ArticleContents.Remove(articleContent);
            await context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the article content.");
        }
    }



}

