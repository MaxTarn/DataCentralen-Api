using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public string Content { get; set; } = null!;
}
