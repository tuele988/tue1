using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace shopaoquan.Models
{
    [Table("Links")]
    public class ModelLink
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Slug { get; set; }
        [Required]
        public String Type { get; set; }
        public int Tableid { get; set; }
    }
}