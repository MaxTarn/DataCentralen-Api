using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCentralen_Db.Models.DTOModels
{
    public class ArticleCardDisplay
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string? ColorCodeOne { get; set; } = null!;
        public string? ColorCodeTwo { get; set; } = null!;
    }
}