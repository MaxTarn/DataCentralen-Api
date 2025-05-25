using DataCentralen_Api.DbContext;
using DataCentralen_Db.Models.DbModels;
using DataCentralen_Db.Models.DTOModels;
using DataCentralen_Db.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DataCentralen_Api.Controllers;


//All the enpoints that can mutate the .Content of an article simply create a new ArticleContentModel instead
// resets the foreign key in the old ArticleContentModel
[Route("api/[controller]")]
[ApiController]
public class ArticleController(ArticleRepo articleRepo, AppDbContext context) : ControllerBase
{
    private readonly ArticleRepo _articleRepo = articleRepo;
    private const decimal MaxFileSize = 10 * 1024 * 1024; // 10 MB

    /// <summary>
    /// Retrieves all articles from the database.
    /// </summary>
    /// <returns>A list of articles as DTOs.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetAll()
    {
        var articles = await _articleRepo.GetAllAsync();
        var articleDTOs = articles.Select(article => article.ToArticleDTO()).ToList();
        return Ok(articleDTOs);
    }

    /// <summary>
    /// Retrieves a specific article by its ID.
    /// </summary>
    /// <param name="id">The ID of the article to retrieve.</param>
    /// <returns>The article as a DTO if found, otherwise a NotFound result.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ArticleDTO>> GetById(int id)
    {
        var article = await _articleRepo.GetByIdAsync(id);
        if (article == null)
        {
            return NotFound();
        }
        return Ok(article.ToArticleDTO());
    }

    /// <summary>
    /// Retrieves a list of articles with only their titles and descriptions.
    /// </summary>
    /// <returns>A list of articles with titles and descriptions.</returns>
    [HttpGet("TitleDescription")]
    public ActionResult<ArticleTitleAndDescription> GetTitleDescription()
    {
        var articles = _articleRepo.GetArticleDTO();

        return Ok(articles);
    }

    /// <summary>
    /// Retrieves a list of articles formatted for card display.
    /// </summary>
    /// <returns>A list of articles formatted for card display.</returns>
    [HttpGet("CardDisplay")]
    public ActionResult<ArticleCardDisplay> GetCardDisplay()
    {
        var articles = _articleRepo.GetArticleCardDTO();
        return Ok(articles);
    }

    /// <summary>
    /// Adds a new article to the database.
    /// </summary>
    /// <param name="article">The article DTO containing the details of the article to add.</param>
    /// <returns>The created article as a DTO.</returns>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ArticleDTO>> Add(ArticleDTO article)
    {
        Article articleToAdd = new Article
        {
            Title = article.Title,
            Author = article.Author,
            Posted = DateTime.Now,
            LastEdited = DateTime.Now,
            Likes = 0,
            Description = article.Description,
            Type = article.Type,
            ColorCodeOne = article.ColorCodeOne,
            ColorCodeTwo = article.ColorCodeTwo
        };
        ArticleContentModel articleContent = new ArticleContentModel
        {
            Content = article.Content
        };

        await _articleRepo.AddAsync(articleToAdd);

        articleContent.ArticleId = articleToAdd.Id;
        await context.ArticleContents.AddAsync(articleContent);
        await context.SaveChangesAsync();
        articleToAdd.ArticleContentId = articleContent.Id;
        await context.SaveChangesAsync();

        var articleAsInDb = await _articleRepo.GetByIdAsync(articleToAdd.Id);

        return CreatedAtAction(nameof(GetById), new { id = articleAsInDb.Id }, articleAsInDb.ToArticleDTO());

    }

    /// <summary>
    /// Updates the content of an article using a raw string.
    /// </summary>
    /// <param name="requestObj">The DTO containing the article ID and the new content as a string.</param>
    /// <returns>A NoContent result if successful, otherwise an error response.</returns>
    [Authorize]
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
            article = await _articleRepo.GetByIdAsync((int)requestObj.Id);// Find the article by id
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the article.");
        }

        if (article == null) return NotFound("Article not found.");
        article.ArticleContent!.ArticleId = null;
        article.ArticleContent = null;
        await context.SaveChangesAsync();

        ArticleContentModel newContent = new()
        {
            Content = requestObj.FileAsRawString,
            ArticleId = article.Id
        };
        await context.ArticleContents.AddAsync(newContent);
        await context.SaveChangesAsync();


        article.ArticleContentId = newContent.Id;
        await context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Removes the content of a specific article.
    /// </summary>
    /// <param name="Id">The ID of the article to remove content from.</param>
    /// <returns>A NoContent result if successful, otherwise an error response.</returns>
    [Authorize]
    [HttpPut("remove-content/{Id}")]
    public async Task<IActionResult> RemoveContent(int? Id)
    {
        if (Id == null) return BadRequest("ERROR: Given Id was null");
        Article? article = null;

        try
        {
            article = await _articleRepo.GetByIdAsync((int)Id);// Find the article by id
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the article.");
        }

        if (article == null) return NotFound("Article not found.");


        if (article.ArticleContent == null || article.ArticleContentId == null)
        {
            return Ok("Content Was Already empty");
        }
        article.ArticleContentId = null;
        article.ArticleContent.ArticleId = null;
        article.ArticleContent = null;
        await articleRepo.UpdateAsync(article);
        return NoContent();

    }

    /// <summary>
    /// Changes the content of an article by linking it to a new content ID.
    /// </summary>
    /// <param name="articleId">The ID of the article to update.</param>
    /// <param name="articleContentId">The ID of the new content to link to the article.</param>
    /// <returns>A NoContent result if successful, otherwise an error response.</returns>
    [HttpPut("article-id/{articleId}/article-content-id/{articleContentId}")]
    public async Task<ActionResult<Article>> ChangeContent(int articleId, int articleContentId)
    {
        if (articleId == 0 || articleContentId == 0)
        {
            return BadRequest("A Given id was not valid.");
        }

        try
        {

            var article = await _articleRepo.GetByIdAsync(articleId);
            if (article == null)
            {
                return NotFound("Article not found.");
            }


            var articleContent = await context.ArticleContents.FirstOrDefaultAsync(a => a.Id == articleContentId);
            if (articleContent == null)
            {
                return NotFound("Article content not found.");
            }


            var existingArticle = await context.Articles.FirstOrDefaultAsync(a => a.ArticleContentId == articleContentId);
            if (existingArticle != null)
            {
                existingArticle.ArticleContentId = null;
            }


            if (article.ArticleContent != null)
            {
                article.ArticleContent.ArticleId = null;
            }


            article.ArticleContentId = articleContent.Id;
            articleContent.ArticleId = article.Id;


            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
        }

        return NoContent();
    }

    /// <summary>
    /// Updates an existing article in the database.
    /// </summary>
    /// <param name="id">The ID of the article to update.</param>
    /// <param name="article">The updated article DTO.</param>
    /// <returns>A NoContent result if successful, otherwise an error response.</returns>
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Article>> Update(int id, ArticleDTO article)
    {
        if (id != article.Id) return BadRequest();
        Article? articleInDb = await context.Articles.FirstOrDefaultAsync(a => a.Id == article.Id);
        if (articleInDb == null) return NotFound("Article with given id not found.");



        articleInDb.Title = article.Title;
        articleInDb.Author = article.Author;
        articleInDb.Posted = article.Posted;
        articleInDb.LastEdited = DateTime.Now; // Update the last edited timestamp
        articleInDb.Likes = article.Likes;
        articleInDb.Description = article.Description;
        articleInDb.Type = article.Type;
        articleInDb.ColorCodeOne = article.ColorCodeOne;
        articleInDb.ColorCodeTwo = article.ColorCodeTwo;


        ArticleContentModel updatedContent = new()
        {
            Content = article.Content,
            ArticleId = article.Id
        };

        await context.ArticleContents.AddAsync(updatedContent);
        await context.SaveChangesAsync();

        if (articleInDb.ArticleContent != null)
        {
            articleInDb.ArticleContent.ArticleId = null;
            articleInDb.ArticleContentId = null;
        }

        await context.SaveChangesAsync();

        articleInDb.ArticleContentId = updatedContent.Id;


        await context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// Deletes an article by its ID.
    /// </summary>
    /// <param name="id">The ID of the article to delete.</param>
    /// <returns>A NoContent result if successful, otherwise an error response.</returns>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Article>> Delete(int id)
    {
        var article = await _articleRepo.GetByIdAsync(id);
        if (article == null) return NotFound("Article not found.");

        if (article.ArticleContent != null)
        {
            article.ArticleContent.ArticleId = null;
        }

        await _articleRepo.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("GroupedDropdown/{id}")]
    public async Task<ActionResult> GetGroupedForDropdown(int id)
    {
        var allArticles = await _articleRepo.GetArticleCardDTO().ToListAsync();

        var sorting = allArticles
            .Where(a => a.Type == "Sorteringsalgoritm")
            .OrderByDescending(a => a.Id)
            .Take(id)
            .ToList();

        var structures = allArticles
            .Where(a => a.Type == "Datastruktur")
            .OrderByDescending(a => a.Id)
            .Take(id)
            .ToList();

        return Ok(new
        {
            sortingAlgorithms = sorting,
            dataStructures = structures
        });
    }
    //[Authorize]
    //[HttpPut("with-file/{id}")]
    //public async Task<IActionResult> UploadFile(int id, IFormFile file)
    //{
    //    if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

    //    if (file.Length > MaxFileSize) return BadRequest("File size exceeds the maximum limit of 10 MB.");

    //    // Ensure file is .html or .md
    //    if (!file.FileName.EndsWith(".html", StringComparison.OrdinalIgnoreCase) &&
    //        !file.FileName.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
    //    {
    //        return BadRequest("Only HTML and Markdown files are allowed.");
    //    }

    //    // Read the file 
    //    string fileContent;
    //    try
    //    {
    //        using (var reader = new StreamReader(file.OpenReadStream()))
    //        {
    //            fileContent = await reader.ReadToEndAsync();
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, "Error reading the file.");
    //    }

    //    // Assuming the file content is already in HTML or Markdown format
    //    string content = fileContent;

    //    // Find the article by id
    //    var article = await _articleRepo.GetByIdAsync(id);
    //    if (article == null)
    //    {
    //        return NotFound("Article not found.");
    //    }

    //    // Update the article content
    //    article.ArticleContent.Content = content;
    //    await _articleRepo.UpdateAsync(article);

    //    return NoContent();
    //}

    private string ConvertToHtml(string content)
    {
        // Simple conversion to HTML (you can customize this as needed)
        return $"<html><body><pre>{System.Net.WebUtility.HtmlEncode(content)}</pre></body></html>";
    }
}
