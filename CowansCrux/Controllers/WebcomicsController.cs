using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CowansCrux.Models;
using System.IO;

namespace CowansCrux.Controllers
{
    public class WebcomicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Webcomics
        public ActionResult Index()
        {
            return View(db.Webcomics.ToList());
        }

        // GET: Webcomics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Webcomic webcomic = db.Webcomics.Find(id);
            if (webcomic == null)
            {
                return HttpNotFound();
            }
            return View(webcomic);
        }

        // GET: Webcomics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Webcomics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,url,dateToDisplay")] Webcomic webcomic, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    if (image.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);
                        var fileExtension = Path.GetExtension(fileName);
                        if ((fileExtension == ".jpg") || (fileExtension == ".gif") || (fileExtension == ".png"))
                        {
                            var path = Server.MapPath("~/img/Webcomic/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            webcomic.url = "/img/Webcomic/" + fileName;

                            image.SaveAs(Path.Combine(path, fileName));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("image", "Invalid image extension.  Only .gif, .jpg, and .png are allowed.");
                        return View(webcomic);
                    }
                }
                webcomic.dateUploaded = DateTimeOffset.Now;
                db.Webcomics.Add(webcomic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(webcomic);
        }

        // GET: Webcomics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Webcomic webcomic = db.Webcomics.Find(id);
            if (webcomic == null)
            {
                return HttpNotFound();
            }
            return View(webcomic);
        }

        // POST: Webcomics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,url,dateUploaded,dateToDisplay")] Webcomic webcomic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webcomic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(webcomic);
        }

        // GET: Webcomics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Webcomic webcomic = db.Webcomics.Find(id);
            if (webcomic == null)
            {
                return HttpNotFound();
            }
            return View(webcomic);
        }

        // POST: Webcomics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Webcomic webcomic = db.Webcomics.Find(id);
            db.Webcomics.Remove(webcomic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult First()
        {
            Webcomic webcomic = db.Webcomics.OrderBy(w => w.dateUploaded).FirstOrDefault();
            return View(webcomic);
        }

        public ActionResult Last()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Next(int id)
        {
            Webcomic webcomic = null;
            if (id + 1 < db.Webcomics.Count())
            {
                webcomic = db.Webcomics.Find(id + 1);
                return View(webcomic);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
        
        public ActionResult Prev(int id)
        {
            Webcomic webcomic = null;
            if (id != db.Webcomics.OrderBy(w => w.dateUploaded).FirstOrDefault().id)
            {
                webcomic = db.Webcomics.Find(id - 1);
                return View(webcomic);
            }
            else
            {
                return View();
            }
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
