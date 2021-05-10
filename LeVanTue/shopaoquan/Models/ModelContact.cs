using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopaoquan.Models
{
    [Table("Contact")]
    public class ModelContact 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Fullame { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String Phone { get; set; }
        [Required]
        public String Title { get; set; }
        [Required]
        public String Detail { get; set; }
        public int? ReplayId { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Update_at { get; set; }
        public int? Update_by { get; set; }
        public int? Status { get; set; }
    }
}