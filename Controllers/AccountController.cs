using MvcKitapProjesi1.Models;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace MvcKitapProjesi1.Controllers
{
    public class AccountController : Controller
    {
        private KitapDBModel db = new KitapDBModel();

        // Giriş Sayfası (GET)
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // Giriş İşlemi (POST)
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            var user = db.Kullanicilar
                         .FirstOrDefault(k => k.Email == model.Email && k.Sifre == model.Password);

            if (user != null)
            {
                Session["KullaniciID"] = user.KullaniciID;
                Session["KullaniciAdi"] = user.KullaniciAdi;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "E-posta veya şifre hatalı.");
            return View(model);
        }

        // Kayıt Sayfası (GET)
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // Kayıt İşlemi (POST)
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            // Aynı email var mı kontrol et
            var existing = db.Kullanicilar.FirstOrDefault(k => k.Email == model.Email);
            if (existing != null)
            {
                ModelState.AddModelError("", "Bu e-posta adresi zaten kayıtlı.");
                return View(model);
            }

            // Yeni kullanıcı oluştur
            var yeni = new Kullanicilar
            {
                KullaniciAdi = model.KullaniciAdi,
                Email = model.Email,
                Sifre = model.Password // Not: Gerçek projede şifre hashlenmelidir!
            };

            try
            {
                db.Kullanicilar.Add(yeni);
                db.SaveChanges();

                // Oturum başlat
                Session["KullaniciID"] = yeni.KullaniciID;
                Session["KullaniciAdi"] = yeni.KullaniciAdi;

                return RedirectToAction("Index", "Home");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("HATA - Alan: " + validationError.PropertyName +
                                                           " | Mesaj: " + validationError.ErrorMessage);
                        ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                return View(model);
            }
        }

        // Çıkış İşlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
