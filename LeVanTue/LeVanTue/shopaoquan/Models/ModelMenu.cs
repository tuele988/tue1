using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopaoquan.Models
{
    [Table("Menu")]
    public class ModelMenu
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Link { get; set; }
        [Required]
        public String Type { get; set; }

        public int? Tableid { get; set; }
        public int? Orders { get; set; }
        [Required]
        public String Position { get; set; }
        public int Parenid{get; set; }
        public DateTime? Created_at { get; set; }
        public int? Create_by { get; set; }
        public DateTime? Update_at { get; set; }
        public int? Update_by { get; set; }
        public int Status { get; set; }
    }
}