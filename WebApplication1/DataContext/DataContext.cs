using Microsoft.EntityFrameworkCore;
using Store99.Models;

namespace Store99.AppContext
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        { 
        
        }

        // definimos los db set, es decir, las entidades de la db
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<StockShoeSize> StockShoeSizes { get; set; }
        public DbSet<ShoeFile> ShoeFiles { get; set; }

        // esto lo usamos para referenciar las relaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockShoeSize>()
                 // aca digo que la primary key es una combinación de sizeId y  
                 .HasKey(ss => new { ss.SizeId, ss.ShoeId });
            modelBuilder.Entity<StockShoeSize>()
                // establecemos la relación de un size con muchos en la lista de stockshoesizes
                .HasOne(siz => siz.SizeNumber)
                .WithMany(ss => ss.StockShoesSizes)
                .HasForeignKey(siz => siz.SizeId);
            modelBuilder.Entity<StockShoeSize>()
                // establecemos la relación de un sshoe con muchos en la lista de stockshoesizes
                .HasOne(sh => sh.Shoe)
                .WithMany(ss => ss.StockShoesSizes)
                .HasForeignKey(sh => sh.ShoeId);
            modelBuilder.Entity<Shoe>()
                .HasOne(b => b.Brand)
                .WithMany(s => s.Shoes)
                .HasForeignKey(s => s.BrandId);
            modelBuilder.Entity<Shoe>()
                .HasMany(s => s.ShoeFile)
                .WithOne(s => s.Shoe)
                .HasForeignKey(sf => sf.ShoeId);
            modelBuilder.Entity<Brand>()
               .HasMany(s => s.Shoes)
               .WithOne(b => b.Brand)
               .HasForeignKey(s => s.BrandId);
            modelBuilder.Entity<ShoeFile>()
               .HasOne(sf => sf.Shoe)
               .WithMany(s => s.ShoeFile)
               .HasForeignKey(sf => sf.ShoeId);

        }
    }
}
