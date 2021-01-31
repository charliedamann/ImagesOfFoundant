using Microsoft.EntityFrameworkCore;

namespace ImagesOfFoundant.Models
{
    public class ImageSharingContext : DbContext
    {
        public ImageSharingContext(DbContextOptions<ImageSharingContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Image>()
            .HasMany(p => p.Tags)
            .WithMany(p => p.Images)
            .UsingEntity(j => j.ToTable("ImageTags"));
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
