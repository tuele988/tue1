using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace shopaoquan.Models
{
    [Table("Post")]
    public class ModelPost
    {
        [Key]
        public int Id { get; set; }
        public int? Topid { get; set; }
        [Required]
        public String Title { get; set; }
        public String Slug { get; set; }
        public String Detail { get; set; }
        public String Img { get; set; }
        [Required]
        public String Type { get; set; }
        public String Metakey { get; set; }
        public String Metadesc { get; set; }
        public DateTime? Created_at { get; set; }
        public int? Create_by { get; set; }
        public DateTime? Update_at { get; set; }
        public int? Update_by { get; set; }
        public int Status { get; set; }
    }
}