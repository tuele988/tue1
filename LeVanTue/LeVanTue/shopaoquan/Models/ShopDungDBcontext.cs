using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;


namespace shopaoquan.Models
{
    public class ShopDungDBcontext : DbContext
    {
        public ShopDungDBcontext() : base("name=ChuoiKN") { }
        public D<ModelCategory> Category { get; set; }
        public DbSet<ModelContact> Contact { get; set; }
        public DbSet<ModelLink> Link { get; set; }
        public DbSet<ModelMenu> Menu { get; set; }
        public DbSet<ModelOder> Order { get; set; }
        public DbSet<ModelOderdeatil> Oderdeati { get; set; }
        public DbSet<ModelUser> User { get; set; }
        public DbSet<ModerProduct> Product{ get; set; }
        public DbSet<SliderModel> Slider { get; set; }
        public DbSet<ModelPost> Post { get; set; }
        public DbSet<ModelTopPic> ToPic{ get; set; }

    }
}