using MvcKitapProjesi1.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

public class BookReviewsController : Controller
{
    public KitapDBModel db = new KitapDBModel();

    // POST: BookReviews/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(BookReviews review)
    {
        // Kullanıcı giriş yapmamışsa hata dön
        if (Session["KullaniciID"] == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        int userId = (int)Session["KullaniciID"];

        // Kullanıcının kitabın durumunu kontrol et
        var kitapDurumu = db.Kitaplar
            .Include(k => k.Durumlar)
            .FirstOrDefault(k => k.KitapID == review.KitapID && k.Durumlar.DurumAdi == "Okundu");

        if (kitapDurumu == null)
        {
            TempData["Uyari"] = "Yalnızca 'Okundu' durumundaki kitaplara yorum yapabilirsiniz.";
            return RedirectToAction("Details", "Kitaplar", new { id = review.KitapID });
        }

        // Kullanıcı ID'sini ata
        review.KullaniciID = userId;

        // Yorum ekle ve kaydet
        db.BookReviews.Add(review);
        db.SaveChanges();

        // Kitap detay sayfasına dön
        return RedirectToAction("Details", "Kitaplar", new { id = review.KitapID });
    }
}
