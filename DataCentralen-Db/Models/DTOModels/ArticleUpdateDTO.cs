using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCentralen_Db.Models.DTOModels
{
    public class ArticleUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? ColorCodeOne { get; set; }
        public string? ColorCodeTwo { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
