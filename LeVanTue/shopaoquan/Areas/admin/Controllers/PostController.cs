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
    public class PostController : BaseController
    {
        private ShopAoQuanDBontext db = new ShopAoQuanDBontext();

        // GET: admin/Post
        public ActionResult Index()
        {
            var list = db.Post.
               Join(db.ToPic,
               p => p.Topid,
               c => c.Id,
               (p, c) => new producttoppic
               {
                   PostId = p.Id,
                   Posttitle = p.Title,
                   PostImg = p.Img,
                   PostStatus = p.Status,
                   toppictitle = c.Name
               })
               .Where(m => m.PostStatus != 0);
            return View(list.ToList());

           /* var model = db.Post
              .Where(m => m.Status != 0)
              .ToList();
            return View("Index", model);*/
           
        }
        public ActionResult Trash()
        {
            var model = db.Post
                .Where(m => m.Status == 0)
                .ToList();
            return View("Trash", model);
        }
        // GET: admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelPost modelPost = db.Post.Find(id);
            if (modelPost == null)
            {
                return HttpNotFound();
            }
            return View(modelPost);
        }

        // GET: admin/Post/Create
        public ActionResult Create()
        {
            ViewBag.Listtopic = new SelectList(db.ToPic, "id", "Name", 0);
            return View();
        }

        // POST: admin/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelPost modelPost)
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
                                ModelState.AddModelError("Img", "Kiểu tâp tin" + string.Join(",", FileExtensions) + "Không cho phep");
                            }

                            String slug = myString.GenerateSeoFriendlyURL(modelPost.Title);
                            String fileName = slug + file.FileName.Substring(file.FileName.LastIndexOf("."));
                            modelPost.Img = fileName;
                            String Strpath = Path.Combine(Server.MapPath("~/public/img/Post"), fileName);
                            file.SaveAs(Strpath);
                            modelPost.Slug = slug;
                            modelPost.Update_by = 1;
                            modelPost.Create_by = 1;
                            modelPost.Created_at = DateTime.Now;
                            modelPost.Update_at = DateTime.Now;
                            db.Post.Add(modelPost);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("LETUE", "them khong thành công");
                    }
                
            }
            ViewBag.Listtopic = new SelectList(db.ToPic, "Oder", "Name", 0);
            return View(modelPost);
        }

        // GET: admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelPost modelPost = db.Post.Find(id);
            if (modelPost == null)
            {
                return HttpNotFound();
            }
            return View(modelPost);
        }

        // POST: admin/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Topid,Title,Slug,Detail,Img,Type,Metakey,Metadesc,Created_at,Create_by,Update_at,Update_by,Status")] ModelPost modelPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modelPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modelPost);
        }

        // GET: admin/Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelPost modelPost = db.Post.Find(id);
            if (modelPost == null)
            {
                return HttpNotFound();
            }
            return View(modelPost);
        }

        // POST: admin/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModelPost modelPost = db.Post.Find(id);
            db.Post.Remove(modelPost);
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
            ModelPost modelPost = db.Post.Find(id);
            if (modelPost != null)
            {
                modelPost.Status = (modelPost.Status == 1) ? 2 : 1;
                db.Entry(modelPost).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Post");
        }
        public ActionResult Deltrash(int id)
        {
            ModelPost modelPost = db.Post.Find(id);
            if (modelPost != null)
            {
                modelPost.Status = 0;
                db.Entry(modelPost).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Post");
        }

    }
}
