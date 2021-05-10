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
    public class OrderController : BaseController
    {
        private ShopAoQuanDBontext db = new ShopAoQuanDBontext();

        // GET: admin/Order
        public ActionResult Index()
        {
            ViewBag.Title = "order";
            var model = db.Order
                .Where(m => m.Status != 0)
                .ToList();
            return View("index", model);
        }

        public ActionResult Trash()
        {
            ViewBag.Title = "order";
            var model = db.Order
                .Where(m => m.Status == 0)
                .ToList();
            return View("Trash", model);
        }

        // GET: admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelOder modelOder = db.Order.Find(id);
            if (modelOder == null)
            {
                return HttpNotFound();
            }
            return View(modelOder);
        }

        // GET: admin/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Userid,Createdate,Exportdate,DeliveryAddress,DeliveryName,DeliveryPhone,Deliveryemail,Update_at,Update_by,Status")] ModelOder modelOder)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(modelOder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modelOder);
        }

        // GET: admin/Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelOder modelOder = db.Order.Find(id);
            if (modelOder == null)
            {
                return HttpNotFound();
            }
            return View(modelOder);
        }

        // POST: admin/Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Userid,Createdate,Exportdate,DeliveryAddress,DeliveryName,DeliveryPhone,Deliveryemail,Update_at,Update_by,Status")] ModelOder modelOder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modelOder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modelOder);
        }

        // GET: admin/Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelOder modelOder = db.Order.Find(id);
            if (modelOder == null)
            {
                return HttpNotFound();
            }
            return View(modelOder);
        }

        // POST: admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModelOder modelOder = db.Order.Find(id);
            db.Order.Remove(modelOder);
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
            ModelOder modelOder = db.Order.Find(id);
            if (modelOder != null)
            {
                modelOder.Status = (modelOder.Status == 1) ? 2 : 1;
                db.Entry(modelOder).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Order");
        }
        public ActionResult Deltrash(int id)
        {
            ModelOder modelOder = db.Order.Find(id);
            if (modelOder != null)
            {
                modelOder.Status = 0;
                db.Entry(modelOder).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Order");
        }
    }
}
