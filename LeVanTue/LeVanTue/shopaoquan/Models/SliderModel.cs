using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace shopaoquan.Models
{
    [Table("Slider")]
    public class SliderModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        public String Link { get; set; }
        public String Position { get; set; }
        public String Img { get; set; }
        public int Oder { get; set; }
        public DateTime Created_at { get; set; }
        public int Create_by { get; set; }
        public DateTime Update_at { get; set; }
        public int Update_by { get; set; }
        public int? Status { get; set; }
    }
}