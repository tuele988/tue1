using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace shopaoquan.Models
{
    [Table("Product")]
    public class ModerProduct
    {
        [Key]
        public int Id { get; set; }
        public int Catid { get; set; }
        [Required]
        public String Name { get; set; }
        public String Slug { get; set; }
        
        public String Img { get; set; }
        public String Detail { get; set; }
        public int number { get; set; }
        public float Price { get; set; }
        public float PriceSale { get; set; }
        [Required]
        public String Metakey { get; set; }
        public String Metadesc { get; set; }
        public DateTime? Created_at { get; set; }
        public int? Create_by { get; set; }
        public DateTime? Update_at { get; set; }
        public int? Update_by { get; set; }
        public int Status { get; set; }
    }
}