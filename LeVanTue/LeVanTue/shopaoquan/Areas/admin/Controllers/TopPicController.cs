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
    public class TopPicController : BaseController
    {
        private ShopAoQuanDBontext db = new ShopAoQuanDBontext();

        // GET: admin/TopPic
        public ActionResult Index()
        {
            var model = db.ToPic
                .Where(m=>m.Status!=0)
                .ToList();
            return View("Index", model);
        }
        public ActionResult Trash()
        {
            var model = db.ToPic
                .Where(m => m.Status == 0)
                .ToList();
            return View("Index", model);
        }
        // GET: admin/TopPic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelTopPic modelTopPic = db.ToPic.Find(id);
            if (modelTopPic == null)
            {
                return HttpNotFound();
            }
            return View(modelTopPic);
        }

        // GET: admin/TopPic/Create
        public ActionResult Create()
        {
            var list = db.ToPic.Where(m => m.Status != 0).ToList();
            List<SelectListItem> modelItemTopPic = new List<SelectListItem>();
            foreach (var row in list)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = row.Id + "",
                    Text = row.Name
                };
                modelItemTopPic.Add(listItem);
            }
            modelItemTopPic.Insert(0, new SelectListItem()
            {
                Value = "0",
                Text = "-- Please select --"
            });
            ViewBag.ListCat = modelItemTopPic;
            ViewBag.ListOder = new SelectList(db.ToPic.Where(m => m.Status != 0).ToList(), "Oder", "Name", 0);
            return View();
        }

        // POST: admin/TopPic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelTopPic modelTopPic)
        {
            var list = db.ToPic.Where(m => m.Status != 0).ToList();
            List<SelectListItem> modelItemTopPic = new List<SelectListItem>();
            foreach (var row in list)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = row.Id + "",
                    Text = row.Name
                };
                modelItemTopPic.Add(listItem);
            }
            modelItemTopPic.Insert(0, new SelectListItem()
            {
                Value = "0",
                Text = "-- Please select --"
            });
            ViewBag.ListCat = modelTopPic;
            ViewBag.ListOder = new SelectList(db.ToPic.Where(m => m.Status != 0).ToList(), "Oder", "Name", 0);
            if (ModelState.IsValid)
            {
                if (modelTopPic.Parentid == null)
                {
                    modelTopPic.Parentid = 0;
                }
                String slug = Library.myString.GenerateSeoFriendlyURL(modelTopPic.Name);
                modelTopPic.Slug = slug;
                modelTopPic.Oder += 1;
                modelTopPic.Update_by = 1;
                modelTopPic.Create_by = 1;
                modelTopPic.Created_at = DateTime.Now;
                modelTopPic.Update_at = DateTime.Now;
                db.ToPic.Add(modelTopPic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modelTopPic);
        }

        // GET: admin/TopPic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelTopPic modelTopPic = db.ToPic.Find(id);
            if (modelTopPic == null)
            {
                return HttpNotFound();
            }
            return View(modelTopPic);
        }

        // POST: admin/TopPic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Slug,Parentid,Oder,MetataKey,MetataDesc,Created_at,Create_by,Update_at,Update_by,Status")] ModelTopPic modelTopPic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modelTopPic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modelTopPic);
        }

        // GET: admin/TopPic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelTopPic modelTopPic = db.ToPic.Find(id);
            if (modelTopPic == null)
            {
                return HttpNotFound();
            }
            return View(modelTopPic);
        }

        // POST: admin/TopPic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModelTopPic modelTopPic = db.ToPic.Find(id);
            db.ToPic.Remove(modelTopPic);
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
            ModelTopPic modelTopPic = db.ToPic.Find(id);
            if (modelTopPic != null)
            {
                modelTopPic.Status = (modelTopPic.Status == 1) ? 2 : 1;
                db.Entry(modelTopPic).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "TopPic");
        }
        public ActionResult Deltrash(int id)
        {
            ModelTopPic modelTopPic = db.ToPic.Find(id);
            if (modelTopPic != null)
            {
                modelTopPic.Status = 0;
                db.Entry(modelTopPic).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "TopPic");
        }
    }
}
