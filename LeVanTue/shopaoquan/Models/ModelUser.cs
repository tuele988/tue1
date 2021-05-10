using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace shopaoquan.Models
{
    [Table("User")]
    public class ModelUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Fullname { get; set; }
        [Required]
        public String UserName  { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        public String Email { get; set; }
        public int Gender { get; set; }
        [Required]
        public String Phone { get; set; }
        public String Img { get; set; }
        public int Access { get; set; }
        public DateTime? Created_at { get; set; }
        public int? Create_by { get; set; }
        public DateTime? Update_at { get; set; }
        public int? Update_by { get; set; }
        public int Status { get; set; }
    }
}