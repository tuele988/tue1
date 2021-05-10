using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace shopaoquan.Models
{
    [Table("Oder")]
    public class ModelOder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Code { get; set; }
        public int Userid { get; set; }
        public DateTime Createdate { get; set; }
        public DateTime? Exportdate { get; set; }
        public String DeliveryAddress { get; set; }
        public String DeliveryName { get; set; }
        
        public String DeliveryPhone { get; set; }
      
        public String Deliveryemail { get; set; }
        public DateTime? Update_at { get; set; }
        public int? Update_by { get; set; }
        public int Status { get; set; }
    }
}