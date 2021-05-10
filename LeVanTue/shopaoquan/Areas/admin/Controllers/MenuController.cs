using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using shopaoquan.Models;

namespace shopaoquan.Areas.admin.Controllers
{
    public class MenuController : BaseController
    {
        private ShopAoQuanDBontext db = new ShopAoQuanDBontext();

        // GET: admin/Menu
        public ActionResult Index()
        {
            ViewBag.Listcat = db.Category.Where(m=>m.Status==1).ToList();
            ViewBag.ListTopic = db.ToPic.Where(m => m.Status == 1).ToList();
            ViewBag.LisPost = db.Post.Where(m => m.Status == 1 && m.Type=="page").ToList();
            var model =db.Menu
                .Where(m=>m.Status !=0)
                .OrderByDescending(m => m.Created_at)
                .ToList();
            return View("Index",model);
        }
        [HttpPost]
        public ActionResult Index(FormCollection dulieu)
        {
            if (!string.IsNullOrEmpty(dulieu["THEMCATEGORY"]))
            {
                if (!string.IsNullOrEmpty(dulieu["itemCat"]))
                {
                    var itemCat = dulieu["itemCat"];
                    var arrcat = itemCat.Split(',');
                    int dem = 0;
                    foreach (var rcat in arrcat)
                    {
                        int id = int.Parse(rcat);
                        ModelCategory Mcategory = db.Category.Find(id);
                        ModelMenu mmenu = new ModelMenu();
                        mmenu.Name = Mcategory.Name;
                        mmenu.Link = Mcategory.Slug;
                        mmenu.Type = "category";
                        mmenu.Position = dulieu["position"];
                        mmenu.Tableid = id;
                        mmenu.Orders = 1;
                        mmenu.Parenid = 0;
                        mmenu.Status = 2;
                        mmenu.Created_at = DateTime.Now;
                        mmenu.Update_at = DateTime.Now;
                        db.Menu.Add(mmenu);
                        db.SaveChanges();
                        dem++;
                    }
                    TempData["thongbao"] = new XMessage("danger", "Đã đếm thành công" + dem.ToString());
                }
                else
                {
                    TempData["thongbao"] = new XMessage("danger", "chưa chon sản phẩm");
                }
            }

            if (!string.IsNullOrEmpty(dulieu["THEMTOPIC"]))
            {
                if (!string.IsNullOrEmpty(dulieu["itemToppic"]))
                {
                    var itemCat = dulieu["itemToppic"];
                    var arrcat = itemCat.Split(',');
                    int dem = 0;
                    foreach (var Tcat in arrcat)
                    {
                        int id = int.Parse(Tcat);
                        ModelTopPic modelTopPic = db.ToPic.Find(id);
                        ModelMenu mmenu = new ModelMenu();
                        mmenu.Name = modelTopPic.Name;
                        mmenu.Link = modelTopPic.Slug;
                        mmenu.Type = "Toppic";
                        mmenu.Position = dulieu["position"];
                        mmenu.Tableid = id;
                        mmenu.Orders = 1;
                        mmenu.Parenid = 0;
                        mmenu.Status = 2;
                        mmenu.Created_at = DateTime.Now;
                        mmenu.Update_at = DateTime.Now;
                        db.Menu.Add(mmenu);
                        db.SaveChanges();
                        dem++;
                    }
                    TempData["thongbao"] = new XMessage("danger", "Đã đếm thành công" + dem.ToString());
                }
                else
                {
                    TempData["thongbao"] = new XMessage("danger", "chưa chon sản phẩm");
                }

            }

            if (!string.IsNullOrEmpty(dulieu["THEMPAGE"]))
            {
                if (!string.IsNullOrEmpty(dulieu["itemPost"]))
                {
                    var itemCat = dulieu["itemPost"];
                    var arrcat = itemCat.Split(',');
                    int dem = 0;
                    foreach (var Tcat in arrcat)
                    {
                        int id = int.Parse(Tcat);
                        ModelPost modelPost = db.Post.Find(id);
                        ModelMenu mmenu = new ModelMenu();
                        mmenu.Name = modelPost.Title;
                        mmenu.Link = modelPost.Slug;
                        mmenu.Type = "page";
                        mmenu.Position = dulieu["position"];
                        mmenu.Tableid = id;
                        mmenu.Orders = 1;
                        mmenu.Parenid = 0;
                        mmenu.Status = 2;
                        mmenu.Created_at = DateTime.Now;
                        mmenu.Update_at = DateTime.Now;
                        db.Menu.Add(mmenu);
                        db.SaveChanges();
                        dem++;
                    }
                    TempData["thongbao"] = new XMessage("danger", "Đã đếm thành công" + dem.ToString());
                }
                else
                {
                    TempData["thongbao"] = new XMessage("danger", "chưa chon sản phẩm");
                }
            }
            
            if (!string.IsNullOrEmpty(dulieu["THEMCUSTOM"]))
            {
           
               if(!string.IsNullOrEmpty(dulieu["name"]) && !string.IsNullOrEmpty(dulieu["link"]))
                {
                    ModelMenu mmenu = new ModelMenu();
                    mmenu.Name = dulieu["name"];
                    mmenu.Link = dulieu["link"];
                    mmenu.Type = "custom";
                    mmenu.Position = dulieu["position"];
                    mmenu.Tableid = 1;
                    mmenu.Orders = 1;
                    mmenu.Parenid = 0;
                    mmenu.Status = 2;
                    mmenu.Created_at = DateTime.Now;
                    mmenu.Update_at = DateTime.Now;
                    db.Menu.Add(mmenu);
                    db.SaveChanges();
                    TempData["thongbao"] = new XMessage("success", "Đẫ thêm thành công");
                }
                else
                {
                    TempData["thongbao"] = new XMessage("danger", "Liên kết và tên không để trống");
                }
            }
           

            ViewBag.Listcat = db.Category.Where(m => m.Status == 1).ToList();
            ViewBag.ListTopic = db.ToPic.Where(m => m.Status == 1).ToList();
            ViewBag.LisPost = db.Post.Where(m => m.Status == 1 && m.Type == "page").ToList();
            var model = db.Menu
                .Where(m => m.Status != 0)
                .OrderByDescending(m => m.Created_at)
                .ToList();
            return View("Index", model);
        }
        public ActionResult Trash()
        {
            var model = db.Menu
                  .Where(m => m.Status == 0)
             
                  .ToList();
            return View("Trash", model);
        }
        // GET: admin/Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelMenu modelMenu = db.Menu.Find(id);
            if (modelMenu == null)
            {
                return HttpNotFound();
            }
            return View(modelMenu);
        }

        // GET: admin/Menu/Create
        

        // GET: admin/Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelMenu modelMenu = db.Menu.Find(id);
            if (modelMenu == null)
            {
                return HttpNotFound();
            }
            return View(modelMenu);
        }

        // POST: admin/Menu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Link,Type,Tableid,Orders,Position,Parenid,Created_at,Create_by,Update_at,Update_by,Status")] ModelMenu modelMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modelMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modelMenu);
        }

        // GET: admin/Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelMenu modelMenu = db.Menu.Find(id);
            if (modelMenu == null)
            {
                return HttpNotFound();
            }
            return View(modelMenu);
        }

        // POST: admin/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModelMenu modelMenu = db.Menu.Find(id);
            db.Menu.Remove(modelMenu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Status(int id)
        {
            ModelMenu modelMenu = db.Menu.Find(id);
            if (modelMenu != null)
            {
                modelMenu.Status = (modelMenu.Status == 1) ? 2 : 1;
                db.Entry(modelMenu).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Menu");
        }
        public ActionResult Deltrash(int id)
        {
            ModelMenu modelMenu = db.Menu.Find(id);
            if (modelMenu != null)
            {
                modelMenu.Status = 0;
                db.Entry(modelMenu).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Menu");
        }
    }
}
