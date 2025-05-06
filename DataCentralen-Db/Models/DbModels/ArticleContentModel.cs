using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataCentralen_Db.Models.DbModels;

public class ArticleContentModel
{
    [Key]
    public int Id { get; set; }


    public string Content { get; set; } = string.Empty;
    public int? ArticleId { get; set; } // Foreign key to Article

    [JsonIgnore]
    public Article? Article { get; set; }
}

