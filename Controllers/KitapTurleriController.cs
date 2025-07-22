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
    public class KitapTurleriController : Controller
    {
        private KitapDBModel db = new KitapDBModel();

        // GET: KitapTurleri
        public ActionResult Index()
        {
            return View(db.KitapTurleri.ToList());
        }

        // GET: KitapTurleri/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KitapTurleri/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TurID,TurAdi")] KitapTurleri kitapTurleri)
        {
            if (ModelState.IsValid)
            {
                db.KitapTurleri.Add(kitapTurleri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kitapTurleri);
        }

        // GET: KitapTurleri/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            KitapTurleri kitapTurleri = db.KitapTurleri.Find(id);
            if (kitapTurleri == null)
                return HttpNotFound();

            return View(kitapTurleri);
        }

        // POST: KitapTurleri/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TurID,TurAdi")] KitapTurleri kitapTurleri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kitapTurleri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kitapTurleri);
        }

        // GET: KitapTurleri/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            KitapTurleri kitapTurleri = db.KitapTurleri.Find(id);
            if (kitapTurleri == null)
                return HttpNotFound();

            return View(kitapTurleri);
        }

        // POST: KitapTurleri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] // Güvenlik için 
        public ActionResult DeleteConfirmed(int id)
        {
            KitapTurleri kitapTurleri = db.KitapTurleri.Find(id);
            db.KitapTurleri.Remove(kitapTurleri);   
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) // Veritabanı bağlantısını düzgün bir şekilde kapatmak için kullanılır 
        {
            if (disposing)
            {
                db.Dispose(); // işim bitti bağlantıyı kapat bellek temizliği yapar 
            }
            base.Dispose(disposing);
        }
    }
}
