using DataCentralen_Db.Models.DbModels;
using DataCentralen_Db.Models.DTOModels;
using DataCentralen_Db.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DataCentralen_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController(ArticleRepo articleRepo) : ControllerBase
{
    private readonly ArticleRepo _articleRepo = articleRepo;
    private const decimal MaxFileSize = 10 * 1024 * 1024; // 10 MB

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

    [HttpGet("CardDisplay")]
    public ActionResult<Article> GetCardDisplay()
    {
        var articles = _articleRepo.GetArticleCardDTO();
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
    [HttpPut("with-file/{id}")]
    public async Task<IActionResult> UploadFile(int id, IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

        if (file.Length > MaxFileSize) return BadRequest("File size exceeds the maximum limit of 10 MB.");

        // Ensure file is .html or .md
        if (!file.FileName.EndsWith(".html", StringComparison.OrdinalIgnoreCase) &&
            !file.FileName.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Only HTML and Markdown files are allowed.");
        }

        // Read the file 
        string fileContent;
        try
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                fileContent = await reader.ReadToEndAsync();
            }
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error reading the file.");
        }

        // Assuming the file content is already in HTML or Markdown format
        string content = fileContent;

        // Find the article by id
        var article = await _articleRepo.GetByIdAsync(id);
        if (article == null)
        {
            return NotFound("Article not found.");
        }

        // Update the article content
        article.Content = content;
        await _articleRepo.UpdateAsync(article);

        return NoContent();
    }



    [HttpPut("with-file-as-string")]
    public async Task<IActionResult> UploadFileAsString(DtoArticleFileAsString requestObj)
    {
        if (string.IsNullOrWhiteSpace(requestObj.FileAsRawString)) return BadRequest("No content provided.");
        if (Encoding.UTF8.GetByteCount(requestObj.FileAsRawString) > MaxFileSize) return BadRequest("Content size exceeds the maximum limit of 10 MB.");
        if (requestObj.Id == null) return BadRequest("ERROR: Given Id was null");


        Article? article = null;
        // Find the article by id
        try
        {
            article = await _articleRepo.GetByIdAsync((int)requestObj.Id);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the article.");
        }

        if (article == null) return NotFound("Article not found.");



        // Update the article content
        article.Content = requestObj.FileAsRawString;
        await _articleRepo.UpdateAsync(article);

        return NoContent();
    }

    [HttpPut("remove-content/{Id}")]
    public async Task<IActionResult> RemoveContent(int? Id)
    {
        if (Id == null) return BadRequest("ERROR: Given Id was null");
        Article? article = null;
        // Find the article by id
        try
        {
            article = await _articleRepo.GetByIdAsync((int)Id);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the article.");
        }

        if (article == null) return NotFound("Article not found.");
        article.Content = "";
        await articleRepo.UpdateAsync(article);
        return NoContent();

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
    private string ConvertToHtml(string content)
    {
        // Simple conversion to HTML (you can customize this as needed)
        return $"<html><body><pre>{System.Net.WebUtility.HtmlEncode(content)}</pre></body></html>";
    }
}
