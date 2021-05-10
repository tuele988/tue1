using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shopaoquan.Models;
using PagedList;

namespace shopaoquan.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        ShopAoQuanDBontext db = new ShopAoQuanDBontext();
        public ActionResult Index(String slug=null, int? page=1)
        {
            if (slug == null)
            {
                return this.Home();
            }
            else
            {
                ModelLink rowlink = db.Link.Where(m => m.Slug == slug).FirstOrDefault();
                if (rowlink != null)
                {
                    string typeslug = rowlink.Type;
                    switch (typeslug)
                    {
                        case "category": return this.ProductCategory(slug,page);
                        case "topic": return this.Postopic(slug);
                        case "page": return this.Postpage(slug, page);
                        default: return this.Error404(slug);
                    }
                }
                else
                {
                    int countproduct = db.Product.Where(m => m.Slug == slug && m.Status == 1).Count();
                    if (countproduct != 0)
                    {
                        return this.ProductDetail(slug);
                    }
                    else
                    {
                        int countpost = db.Post.Where(m => m.Slug == slug && m.Status == 1 &&m.Type=="post").Count();
                        if (countpost != 0)
                        {
                            return this.postDeatil(slug);
                        }
                        else
                        {
                            return this.Error404(slug);
                        }
                    }

                    
                }
            }

        }
        public ActionResult Home()
        {
            var listcat = db.Category.Where(m => m.Status == 1 && m.Parentid == 0).ToList();
            return View("Site", listcat);
        }
        public ActionResult ProductHome(int catid, String namecat)
        {
            List<int> listcatid = db.Category.Where(m => m.Status == 1 && m.Parentid == catid).Select(m => m.Id).ToList();
            ViewBag.NameCat = namecat;
            List<int> listcatid1;
            foreach (var row in db.Category.Where(m => m.Status == 1 && m.Parentid == catid).ToList())
            {

                listcatid1 = db.Category.Where(m => m.Status == 1 && m.Parentid == row.Id).Select(m => m.Id).ToList();
                listcatid.AddRange(listcatid1);
            }
            listcatid.Add(catid);
            var listproduct = db.Product.Where(m => m.Status == 1 && listcatid.Contains(m.Catid)).OrderByDescending(m => m.Created_at).Take(6).ToList();
            return View("ProductHome", listproduct);
        }
        public ActionResult Product(int? page)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var list = db.Product.Where(m => m.Status == 1).OrderByDescending(m => m.Created_at);
            return View("Product",list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult post(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var list = db.Post.Where(m => m.Status == 1).OrderByDescending(m => m.Created_at);
            return View("post", list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ProductCategory(String slug, int?page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            ViewBag.NameCa = slug;
            var row_cat = db.Category.Where(m => m.Slug == slug).First();
            int catid = row_cat.Id;
            List<int> listcatid = db.Category.Where(m => m.Status == 1 && m.Parentid == catid).Select(m => m.Id).ToList();
            List<int> listcatid1;
            foreach (var row in db.Category.Where(m => m.Status == 1 && m.Parentid == catid).ToList())
            {

                listcatid1 = db.Category.Where(m => m.Status == 1 && m.Parentid == row.Id).Select(m => m.Id).ToList();
                listcatid.AddRange(listcatid1);
            }
            listcatid.Add(catid);
            var list = db.Product.Where(m => m.Status == 1 && listcatid.Contains(m.Catid)).OrderByDescending(m => m.Created_at);
            return View("ProductCategory", list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Postopic(String slug)
        {
            return View("Postopic");
        }
        public ActionResult Postpage(String slug, int? page)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var row_cat = db.ToPic.Where(m => m.Slug == slug).First();
            int catid = row_cat.Id;
            List<int> listcatid = db.ToPic.Where(m => m.Status == 1 && m.Id == catid).Select(m => m.Id).ToList();
            List<int> listcatid1;
            foreach (var row in db.ToPic.Where(m => m.Status == 1 && m.Parentid == catid).ToList())
            {

                listcatid1 = db.ToPic.Where(m => m.Status == 1 && m.Id == row.Id).Select(m => m.Id).ToList();
                listcatid.AddRange(listcatid1);
            }
            listcatid.Add(catid);
            var list = db.Post.Where(m => m.Status == 1 && listcatid.Contains(m.Id)).OrderByDescending(m => m.Created_at);
            return View("Postpage", list.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Error404(String slug)
        {
            return View("Error404");
        }
        public ActionResult ProductDetail(String slug)
        {
            var row_product = db.Product.Where(m => m.Slug == slug && m.Status == 1).First();
            int catid = row_product.Catid;
            List<int> listcatid = db.Category.Where(m => m.Status == 1 && m.Parentid == catid).Select(m => m.Id).ToList();
            List<int> listcatid1;
            foreach (var row in db.Category.Where(m => m.Status == 1 && m.Parentid == catid).ToList())
            {

                listcatid1 = db.Category.Where(m => m.Status == 1 && m.Parentid == row.Id).Select(m => m.Id).ToList();
                listcatid.AddRange(listcatid1);
            }
            listcatid.Add(catid);
            var listother = db.Product.Where(m => m.Status == 1 &&  m.Id!= row_product.Id && listcatid.Contains(m.Catid)).OrderByDescending(m => m.Created_at).Take(4);
            ViewBag.listOther = listother;
            return View("ProductDetail", row_product);
        }
        public ActionResult postDeatil(String slug)
        {
            var row_post = db.Post.Where(m => m.Slug == slug && m.Status == 1).First();
            int catid = (int)row_post.Topid;
            List<int> listcatid = db.ToPic.Where(m => m.Status == 1 && m.Id == catid).Select(m => m.Id).ToList();
            List<int> listcatid1;
            foreach (var row in db.ToPic.Where(m => m.Status == 1 && m.Id == catid).ToList())
            {

                listcatid1 = db.ToPic.Where(m => m.Status == 1 && m.Id == row.Id).Select(m => m.Id).ToList();
                listcatid.AddRange(listcatid1);
            }
            listcatid.Add(catid);
            var listother = db.Post.Where(m => m.Status == 1 && m.Id != row_post.Id && listcatid.Contains((int)m.Topid)).OrderByDescending(m => m.Created_at).Take(4);
            ViewBag.listOther = listother;
            return View("postDeatil", row_post);
        }


    }
}