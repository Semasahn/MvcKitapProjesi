using Microsoft.AspNet.Identity;
using MvcKitapProjesi1.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MvcKitapProjesi1.Controllers
{
    public class KitaplarController : Controller
    {
        private KitapDBModel db = new KitapDBModel();

        // GET: Kitaplar
        public ActionResult Index()
        {
            var kitaplar = db.Kitaplar
                .Include(k => k.Yazarlar)
                .Include(k => k.KitapTurleri)
                .Include(k => k.Durumlar)
                .Include(k => k.BookReviews) // Yorumlar kalabilir
                .ToList();

            return View(kitaplar);
        }

        // GET: Kitaplar/Create
        public ActionResult Create()
        {
            ViewBag.YazarID = new SelectList(db.Yazarlar.ToList(), "YazarID", "AdSoyad");
            ViewBag.TurID = new SelectList(db.KitapTurleri.ToList(), "TurID", "TurAdi");
            ViewBag.DurumID = new SelectList(db.Durumlar, "DurumID", "DurumAdi");
            return View();
        }

        // POST: Kitaplar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KitapID,KitapAdi,YazarID,TurID,BasimTarihi,Durumlar")] Kitaplar kitap)
        {
            if (ModelState.IsValid)
            {
                db.Kitaplar.Add(kitap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DurumID = new SelectList(db.Durumlar.ToList(), "DurumID", "DurumAdi", null);
            ViewBag.YazarID = new SelectList(db.Yazarlar.ToList(), "YazarID", "AdSoyad", kitap.YazarID);
            ViewBag.TurID = new SelectList(db.KitapTurleri.ToList(), "TurID", "TurAdi", kitap.TurID);
            return View(kitap);
        }

        public ActionResult SadeListe()
        {
            var kitaplar = db.Kitaplar.ToList(); // Sadece ID ve Ad kullanılacak
            return View(kitaplar); // SadeListe.cshtml'e gönderiyoruz
        }

        // GET: Kitaplar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Kitaplar kitap = db.Kitaplar.Find(id);
            if (kitap == null)
                return HttpNotFound();

            ViewBag.YazarID = new SelectList(db.Yazarlar.ToList(), "YazarID", "AdSoyad", kitap.YazarID);
            ViewBag.TurID = new SelectList(db.KitapTurleri.ToList(), "TurID", "TurAdi", kitap.TurID);
            ViewBag.DurumID = new SelectList(db.Durumlar, "DurumID", "DurumAdi");
            return View(kitap);
        }

        // POST: Kitaplar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KitapID,KitapAdi,YazarID,TurID,BasimTarihi,Durumlar")] Kitaplar kitap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kitap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DurumID = new SelectList(db.Durumlar.ToList(), "DurumID", "DurumAdi", kitap.DurumID);
            ViewBag.YazarID = new SelectList(db.Yazarlar.ToList(), "YazarID", "AdSoyad", kitap.YazarID);
            ViewBag.TurID = new SelectList(db.KitapTurleri.ToList(), "TurID", "TurAdi", kitap.TurID);
            return View(kitap);
        }

        // GET: Kitaplar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            db.Configuration.ProxyCreationEnabled = false;

            var kitap = db.Kitaplar
                          .Include(k => k.Yazarlar)
                          .Include(k => k.Durumlar)
                          .Include(k => k.KitapTurleri)
                          .Include(k => k.BookReviews) // Yorumlar kalabilir
                          .FirstOrDefault(k => k.KitapID == id);

            if (kitap == null)
                return HttpNotFound();

            return View(kitap);
        }

        // POST: Kitaplar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kitaplar kitap = db.Kitaplar.Find(id);
            db.Kitaplar.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var kitap = db.Kitaplar
                .Include(k => k.Yazarlar)
                .Include(k => k.KitapTurleri)
                .Include(k => k.Durumlar)
                .FirstOrDefault(k => k.KitapID == id);

            if (kitap == null)
            {
                return HttpNotFound();  
            }

            return View(kitap);
        }

    }
}
