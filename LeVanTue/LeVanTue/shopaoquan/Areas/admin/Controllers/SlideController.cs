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
    public class SlideController : BaseController
    {
        private ShopAoQuanDBontext db = new ShopAoQuanDBontext();

        // GET: admin/Slide
        public ActionResult Index()
        {
            var model = db.Slider
                .Where(m => m.Status != 0)
                .ToList();
            return View("Index",model);
        }
        public ActionResult Trash()
        {
            var model = db.Slider
                .Where(m => m.Status == 0)
                .ToList();
            return View("Trash", model);
        }

        // GET: admin/Slide/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderModel sliderModel = db.Slider.Find(id);
            if (sliderModel == null)
            {
                return HttpNotFound();
            }
            return View(sliderModel);
        }

        // GET: admin/Slide/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Slide/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( SliderModel sliderModel)
        {
            if (ModelState.IsValid)
            {
                sliderModel.Update_by = 1;
                sliderModel.Create_by = 1;
                sliderModel.Created_at = DateTime.Now;
                sliderModel.Update_at = DateTime.Now;
                db.Slider.Add(sliderModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sliderModel);
        }

        // GET: admin/Slide/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderModel sliderModel = db.Slider.Find(id);
            if (sliderModel == null)
            {
                return HttpNotFound();
            }
            return View(sliderModel);
        }

        // POST: admin/Slide/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Link,Position,Img,Oder,Created_at,Create_by,Update_at,Update_by,Status")] SliderModel sliderModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sliderModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sliderModel);
        }

        // GET: admin/Slide/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderModel sliderModel = db.Slider.Find(id);
            if (sliderModel == null)
            {
                return HttpNotFound();
            }
            return View(sliderModel);
        }

        // POST: admin/Slide/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SliderModel sliderModel = db.Slider.Find(id);
            db.Slider.Remove(sliderModel);
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
            SliderModel sliderModel = db.Slider.Find(id);
            if (sliderModel != null)
            {
                sliderModel.Status = (sliderModel.Status == 1) ? 2 : 1;
                db.Entry(sliderModel).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Slide");
        }
        public ActionResult Deltrash(int id)
        {
            SliderModel sliderModel = db.Slider.Find(id);
            if (sliderModel != null)
            {
                sliderModel.Status = 0;
                db.Entry(sliderModel).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Slide");
        }
    }
}
