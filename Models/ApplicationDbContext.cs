using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace MvcKitapProjesi1.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // Veritabanında temsil edilecek tablolar
       
        public DbSet<BookReviews> BookReviews { get; set; } // ← EKLENDİ

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // BookReviews için birleşik birincil anahtar (KitapID + KullaniciID)
            modelBuilder.Entity<BookReviews>()
                .HasKey(br => new { br.KitapID, br.KullaniciID });

            // Kitaplar → BookReviews ilişkisi (1 kitaba çok yorum olabilir)
            modelBuilder.Entity<BookReviews>()
                .HasRequired(br => br.Kitaplar)
                .WithMany(k => k.BookReviews) // ← Kitaplar modelinde ICollection<BookReviews> olmalı
                .HasForeignKey(br => br.KitapID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
