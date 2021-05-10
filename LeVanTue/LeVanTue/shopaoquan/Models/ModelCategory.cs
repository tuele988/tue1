using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace shopaoquan.Models
{
    [Table("Category")]
    public class ModelCategory 
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "không để tên loại sản phẩm trống")]
        [DisplayName("ten san pham")]
        public String Name { get; set; }
        public String Slug { get; set; }
        public int Parentid { get; set; }
        public int Oders { get; set; }
        public String Metakey { get; set; }
        public String Metadesc { get; set; }
        public DateTime? Created_at { get; set; }
        public int? Create_by { get; set; }
        public DateTime? Update_at { get; set; }
        public int? Update_by { get; set; }
        public int Status { get; set; }
    }
}