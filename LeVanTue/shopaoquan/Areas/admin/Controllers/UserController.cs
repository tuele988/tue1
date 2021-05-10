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
    public class UserController : BaseController
    {
        private ShopAoQuanDBontext db = new ShopAoQuanDBontext();

        // GET: admin/User
        public ActionResult Index()
        {
            var model = db.User
                .Where(m => m.Status != 0)
                .OrderByDescending(m => m.Created_at)
                .ToList();

            return View("Index", model);
        }
        public ActionResult Trash()
        {
            var model = db.User
                .Where(m => m.Status != 0)
                .OrderByDescending(m => m.Created_at)
                .ToList();

            return View("Trash", model);
        }

        // GET: admin/User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelUser modelUser = db.User.Find(id);
            if (modelUser == null)
            {
                return HttpNotFound();
            }
            return View(modelUser);
        }

        // GET: admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ModelUser modelUser)
        {
            if (ModelState.IsValid)
            {
                modelUser.Update_by = 1;
                modelUser.Create_by = 1;
                modelUser.Created_at = DateTime.Now;
                modelUser.Update_at = DateTime.Now;
                db.User.Add(modelUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modelUser);
        }

        // GET: admin/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelUser modelUser = db.User.Find(id);
            if (modelUser == null)
            {
                return HttpNotFound();
            }
            return View(modelUser);
        }

        // POST: admin/User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fullname,UserName,Password,Email,Gender,Phone,Img,Access,Created_at,Create_by,Update_at,Update_by,Status")] ModelUser modelUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modelUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modelUser);
        }

        // GET: admin/User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelUser modelUser = db.User.Find(id);
            if (modelUser == null)
            {
                return HttpNotFound();
            }
            return View(modelUser);
        }

        // POST: admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModelUser modelUser = db.User.Find(id);
            db.User.Remove(modelUser);
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
            ModelUser modelUser = db.User.Find(id);
            if (modelUser != null)
            {
                modelUser.Status = (modelUser.Status == 1) ? 2 : 1;
                db.Entry(modelUser).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "User");
        }
        public ActionResult Deltrash(int id)
        {
            ModelUser modelUser = db.User.Find(id);
            if (modelUser != null)
            {
                modelUser.Status = 0;
                db.Entry(modelUser).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "User");
        }
    }
}
