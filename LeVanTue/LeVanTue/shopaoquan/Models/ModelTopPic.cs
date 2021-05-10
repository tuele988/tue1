using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace shopaoquan.Models
{
    [Table("TopPic")]
    public class ModelTopPic
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Slug { get; set; }
        [Required]
        public int Parentid { get; set; }
        public int Oder { get; set; }
        public String MetataKey { get; set; }
        public String MetataDesc{ get; set; }
        public DateTime? Created_at { get; set; }
        public int? Create_by { get; set; }
        public DateTime Update_at { get; set; }
        public int Update_by { get; set; }
        public int Status { get; set; }
    }
}