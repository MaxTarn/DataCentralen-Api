using DataCentralen_Db.Models.DTOModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCentralen_Db.Models.DbModels;

public class Article
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    public string Author { get; set; }
    public DateTime Posted { get; set; }
    public DateTime LastEdited { get; set; }
    public int Likes { get; set; }
    public int? ArticleContentId { get; set; } // Foreign key to ArticleContentModel
    public ArticleContentModel? ArticleContent { get; set; }

    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // If it is a "sortingalgorithm" or "datastructure" or "other"

    // To store colorcodes for the cards in frontend
    public string? ColorCodeOne { get; set; }
    public string? ColorCodeTwo { get; set; }

    public ArticleDTO ToArticleDTO()
    {
        return new ArticleDTO
        {
            Id = this.Id,
            Title = this.Title,
            Author = this.Author,
            Posted = this.Posted,
            LastEdited = this.LastEdited,
            Likes = this.Likes,
            Description = this.Description,
            Type = this.Type,
            ColorCodeOne = this.ColorCodeOne,
            ColorCodeTwo = this.ColorCodeTwo,
            Content = this.ArticleContent?.Content
        };
    }

}
