using Microsoft.EntityFrameworkCore;

namespace MVCTicariOtomasyonWeb.Models.sınıflar
{
    public class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cariler> Carilers { get; set; }
        public DbSet<Departman> Departmans { get; set; }
        public DbSet<FaturaKalem> FaturaKalems { get; set; }
        public DbSet<Faturalar> Faturalars { get; set; }
        public DbSet<Gider> Giders { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Personel> Personels { get; set; }
        public DbSet<SatisHareket> SatisHarekets { get; set; }
        public DbSet<Urun> Uruns { get; set; }
        public DbSet<UrunOzellik> UrunOzellikler { get; set; }
        public DbSet<UrunOzellikDeger> UrunOzellikDegerleri { get; set; }
        public DbSet<UrunAciklama> UrunAciklamalar { get; set; }
        public DbSet<UrunYorum> UrunYorumlar { get; set; }
        public DbSet<KategoriOzellik> KategoriOzellikler { get; set; }
        public DbSet<Kargo> Kargos { get; set; }
        public DbSet<KargoDetay> KargoDetays { get; set; }
        public DbSet<Iade> Iades { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Kategori kolon kısıtlamaları
            modelBuilder.Entity<Kategori>()
                .Property(k => k.KategoriAd)
                .HasMaxLength(30)
                .IsRequired()
                .IsUnicode(false);

            // Varsayılan aktiflik
            modelBuilder.Entity<Kategori>()
                .Property(k => k.Durum)
                .HasDefaultValue(true);

            modelBuilder.Entity<Urun>()
                .Property(u => u.Durum)
                .HasDefaultValue(true);

            // Kategori silinince ürünler silinmesin (Restrict)
            modelBuilder.Entity<Urun>()
                .HasOne(u => u.Kategori)
                .WithMany(k => k.Uruns)
                .HasForeignKey(u => u.KategoriId)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<KargoDetay>()
                .HasOne(kd => kd.Kargo)
                .WithMany(k => k.KargoDetaylar)
                .HasForeignKey(kd => kd.KargoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KargoDetay>()
                .HasOne(kd => kd.SatisHareket)
                .WithMany()
                .HasForeignKey(kd => kd.SatisId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
            
        }
    }
}
