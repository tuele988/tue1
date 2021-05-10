using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace shopaoquan.Models
{
    [Table("OderDeatil")]
    public class ModelOderdeatil
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Orderid { get; set; }
        public int  Productid{ get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float Amount { get; set; }
    }
}