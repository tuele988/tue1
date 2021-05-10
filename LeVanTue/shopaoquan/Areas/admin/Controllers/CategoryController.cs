using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using shopaoquan.Library;
using shopaoquan.Models;

namespace shopaoquan.Areas.admin.Controllers
{
    public class CategoryController : BaseController
    {
        private ShopAoQuanDBontext db = new ShopAoQuanDBontext();

        // GET: admin/Category
        public ActionResult Index()
        {
            ViewBag.Title = " Category";
            var model =db.Category
                .Where(m => m.Status != 0)
                .OrderByDescending(m=>m.Created_at)
                .ToList() ;
            return View("index", model);
        }

        public ActionResult Trash()
        {
            ViewBag.Title = "Category";
            var model = db.Category
                .Where(m => m.Status == 0)
                .OrderByDescending(m => m.Created_at)
                .ToList();
            return View("index", model);
        }

        // GET: admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelCategory modelCategory = db.Category.Find(id);
            if (modelCategory == null)
            {
                return HttpNotFound();
            }
            return View(modelCategory);
        }
        [HttpGet]
        // GET: admin/Category/Create
        public ActionResult Create()
        {
            var list = db.Category.Where(m => m.Status != 0).ToList();
            List<SelectListItem> selectlistcategory = new List<SelectListItem>();
            foreach (var row in list)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = row.Id + "",
                    Text = row.Name
                };
                selectlistcategory.Add(listItem);
            }
            selectlistcategory.Insert(0, new SelectListItem()
            {
                Value = "0",
                Text = "-- Please select --"
            });
            ViewBag.ListCat = selectlistcategory;
            ViewBag.ListOder = new SelectList(db.Category.Where(m => m.Status != 0).ToList(), "Oders", "Name",0);
            return View();
        }

        // POST: admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelCategory modelCategory)
        {
            var list = db.Category.Where(m => m.Status != 0).ToList();
            List<SelectListItem> selectlistcategory = new List<SelectListItem>();
            foreach (var row in list)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = row.Id + "",
                    Text = row.Name
                };
                selectlistcategory.Add(listItem);
            }
            selectlistcategory.Insert(0, new SelectListItem()
            {
                Value = "0",
                Text = "-- Please select --"
            });
            ViewBag.ListCat = selectlistcategory;
            ViewBag.ListOder = new SelectList(db.Category.Where(m => m.Status != 0).ToList(), "Oders", "Name",0);
            if (ModelState.IsValid)
            {
                if(modelCategory.Parentid ==null)
                {
                    modelCategory.Parentid = 0;
                }
                String slug = myString.GenerateSeoFriendlyURL(modelCategory.Name);
                modelCategory.Slug = slug;
                modelCategory.Oders += 1;
                modelCategory.Update_by = 1;
                modelCategory.Create_by = 1;
                modelCategory.Created_at = DateTime.Now;
                modelCategory.Update_at = DateTime.Now;
                db.Category.Add(modelCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modelCategory);
        }

        // GET: admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelCategory modelCategory = db.Category.Find(id);
            if (modelCategory == null)
            {
                return HttpNotFound();
            }
            return View(modelCategory);
        }

        // POST: admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Slug,Parentid,Oders,Metakey,Metadesc,Created_at,Create_by,Update_at,Update_by,Status")] ModelCategory modelCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modelCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modelCategory);
        }

        // GET: admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelCategory modelCategory = db.Category.Find(id);
            if (modelCategory == null)
            {
                return HttpNotFound();
            }
            return View(modelCategory);
        }

        // POST: admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModelCategory modelCategory = db.Category.Find(id);
            db.Category.Remove(modelCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Status(int id)
        {
            ModelCategory modelCategory = db.Category.Find(id);
            if (modelCategory != null)
            {
                modelCategory.Status = (modelCategory.Status == 1) ? 2 : 1;
                db.Entry(modelCategory).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index","Category");
        }
        public ActionResult Deltrash(int id)
        {
            ModelCategory modelCategory = db.Category.Find(id);
            if (modelCategory != null)
            {
                modelCategory.Status = 0;
                db.Entry(modelCategory).State = EntityState.Modified;
                db.SaveChanges();
            }
      
            return RedirectToAction("Index", "Category");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
