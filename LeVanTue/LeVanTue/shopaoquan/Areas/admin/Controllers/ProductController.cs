using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using shopaoquan.Library;
using shopaoquan.Models;

namespace shopaoquan.Areas.admin.Controllers
{
    public class ProductController : BaseController
    {
        private ShopAoQuanDBontext db = new ShopAoQuanDBontext();

        // GET: admin/Product
        public ActionResult Index()
        {
            var list = db.Product.
                Join(db.Category,
                p => p.Catid,
                c => c.Id,
                (p, c) => new productcategory
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductImg =p.Img,
                    ProductStatus = p.Status,
                    CategoryName = c.Name
                })
                .Where(m => m.ProductStatus != 0);
            return View(list.ToList());
        }
        public ActionResult Trash()
        {
            var moder = db.Product
                .Where(m => m.Status == 0)
                .OrderByDescending(m => m.Created_at)
                .ToList();
            return View("Index", moder);
        }

        // GET: admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModerProduct moderProduct = db.Product.Find(id);
            if (moderProduct == null)
            {
                return HttpNotFound();
            }
            return View(moderProduct);
        }

        // GET: admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListCatid = new SelectList(db.Category, "Id", "Name", 0);
            return View();
        }


        // POST: admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ModerProduct moderProduct)
        {
 
            if (ModelState.IsValid)
            {
                
                    try
                    {
                        var file = Request.Files["Img"];
                        if (file == null)
                        {
                            ModelState.AddModelError("HINHANH", "them khong thành công");
                        }
                        else
                        {
                            String[] FileExtensions = new string[] { ".jpg", ".gif", ".png" };
                            if (!FileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                            {
                                ModelState.AddModelError("img", "Kiểu tâp tin" + string.Join(",", FileExtensions) + "Không cho phep");
                            }

                            String slug = myString.GenerateSeoFriendlyURL(moderProduct.Name);
                            String fileName = slug + file.FileName.Substring(file.FileName.LastIndexOf("."));
                            moderProduct.Img = fileName;
                            String Strpath = Path.Combine(Server.MapPath("~/public/img/Product"), fileName);
                            file.SaveAs(Strpath);
                            moderProduct.Slug = slug;
                            moderProduct.Update_by = 1;
                            moderProduct.Create_by = 1;
                            moderProduct.Created_at = DateTime.Now;
                            moderProduct.Update_at = DateTime.Now;
                            db.Product.Add(moderProduct);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("LETUE", "them khong thành công");
                    }
                
            }
            ViewBag.ListCatid = new SelectList(db.Category, "Id", "Name", 0);
            return View(moderProduct);
        }

        // GET: admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListCatid = new SelectList(db.Category, "Id", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModerProduct moderProduct = db.Product.Find(id);
            if (moderProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCatid = new SelectList(db.Category, "Id", "Name", 0);
            return View(moderProduct);
        }

        // POST: admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Catid,Name,Slug,Img,Detail,number,Price,PriceSale,Metakey,Metadesc,Created_at,Create_by,Update_at,Update_by,Status")] ModerProduct moderProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moderProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListCatid = new SelectList(db.Category, "Id", "Name", 0);
            return View(moderProduct);
        }

        // GET: admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModerProduct moderProduct = db.Product.Find(id);
            if (moderProduct == null)
            {
                return HttpNotFound();
            }
            return View(moderProduct);
        }

        // POST: admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModerProduct moderProduct = db.Product.Find(id);
            db.Product.Remove(moderProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Status(int id)
        {
            ModerProduct moderProduct = db.Product.Find(id);
            if (moderProduct != null)
            {
                moderProduct.Status = (moderProduct.Status == 1) ? 2 : 1;
                db.Entry(moderProduct).State = EntityState.Modified;
                db.SaveChanges();
                TempData["thongbao"] = new XMessage("success","Thành Công");
            }
            else
            {
                TempData["thongbao"] = new XMessage("danger", "Không Thành Công");
            }

            return RedirectToAction("Index", "Product");
        }
        public ActionResult Deltrash(int id)
        {
            ModerProduct moderProduct = db.Product.Find(id);
            if (moderProduct != null)
            {
                moderProduct.Status = 0;
                db.Entry(moderProduct).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Product");
        }
    }
}
