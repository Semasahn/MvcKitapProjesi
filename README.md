# 📚 MvcKitapProjesi1 - Kitap Takip ve Değerlendirme Sistemi

**MvcKitapProjesi1**, ASP.NET MVC mimarisi ile geliştirilmiş, kullanıcıların kitap ekleyebildiği, kitaplara yorum ve puan verebildiği, favori kitaplarını listeleyebildiği ve okuma durumlarını takip edebildiği kapsamlı bir web uygulamasıdır. Proje hem kullanıcı hem de yönetici (admin) rolleriyle çalışmakta olup, gerçek bir kitap inceleme ve koleksiyon yönetim deneyimi sunar.

---

## 🎯 Proje Amacı

Bu projenin temel amacı, kitap okumayı seven kullanıcıların kitapları organize edebilmesi, değerlendirebilmesi ve yorum paylaşabilmesini sağlamaktır. Aynı zamanda, yazılım geliştiricileri için ASP.NET MVC teknolojileriyle Entity Framework, kullanıcı kimlik doğrulama (Identity) ve veritabanı işlemlerinin nasıl entegre edileceğini gösteren pratik bir örnek sunmaktadır.

---

## 🚀 Öne Çıkan Özellikler

- 👥 **Kullanıcı Yönetimi**  
  - Kayıt ve giriş işlemleri
  - Roller (Kullanıcı, Admin)
  - Identity sistemi ile kimlik doğrulama

- 📖 **Kitap Yönetimi**  
  - Kitap ekleme, düzenleme ve silme (CRUD)
  - Kitaplara yazar ve tür bilgisi iliştirme
  - Kitap durumlarını takip (Okundu / Okunmadı / Devam Ediyor)

- ✍️ **Yorum ve Puanlama**  
  - Yalnızca okunan kitaplara yorum yapılabilir
  - 1-5 yıldız arasında puanlama özelliği
  - Yorum tarihi bilgisiyle listelenme

- 🛠️ **Admin Paneli**  
  - Tüm kullanıcıları ve yorumları görüntüleme
  - Yorumları silme veya onaylama
  - Kitap ve kullanıcı işlemleri üzerinde tam yetki

- 📊 **Veri İlişkileri**  
  - Entity Framework kullanımıyla ilişkili tablolar
  - Navigation property örnekleri
  - LINQ ve Include işlemleri

---

## 🧰 Kullanılan Teknolojiler

| Teknoloji | Açıklama |
|----------|----------|
| ASP.NET MVC | Katmanlı mimari ile web uygulama geliştirme |
| Entity Framework | Veritabanı işlemleri için ORM (Database-First) |
| SQL Server | Veritabanı yönetimi |
| Identity | Kullanıcı kimlik doğrulama sistemi |
| Bootstrap | Responsive tasarım için CSS framework |
| Razor View Engine | Dinamik sayfa rendering |

---

## ⚙️ Kurulum ve Çalıştırma

1. Projeyi klonlayın:
   ```bash
   git clone https://github.com/kullaniciAdi/MvcKitapProjesi1.git
