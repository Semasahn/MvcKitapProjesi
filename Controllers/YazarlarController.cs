using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcKitapProjesi1.Models;

namespace MvcKitapProjesi1.Controllers
{
    public class YazarlarController : Controller
    {
        private KitapDBModel db = new KitapDBModel();

        // GET: Yazarlar
        public ActionResult Index()// index yazarların listelendiği sayfa 
        {
            return View(db.Yazarlar.ToList()); // tüm yazarları liste haline getirir ve view ile index.cshtml sayfasına gönderir 
        }

        // GET: Yazarlar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Yazarlar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YazarID,AdSoyad")] Yazarlar yazar)// bind sadece belirtilen alanların alınmasını sağlar 
        {
            if (ModelState.IsValid)
            {
                db.Yazarlar.Add(yazar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yazar);
        }

        // GET: Yazarlar/Edit/5
        public ActionResult Edit(int? id)// edit id var mı yoksa hatta verir edit/1 .. 
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Yazarlar yazar = db.Yazarlar.Find(id);
            if (yazar == null)
                return HttpNotFound();

            return View(yazar);
        }

        // POST: Yazarlar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YazarID,AdSoyad")] Yazarlar yazar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yazar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");// eyleme geçmek 
            }
            return View(yazar);
        }

        // GET: Yazarlar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Yazarlar yazar = db.Yazarlar.Find(id);
            if (yazar == null)
                return HttpNotFound();

            return View(yazar);
        }

        // POST: Yazarlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]// güvenlik için 
        public ActionResult DeleteConfirmed(int id)
        {
            Yazarlar yazar = db.Yazarlar.Find(id);
            db.Yazarlar.Remove(yazar);
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
    }
}
