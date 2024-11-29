using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class WebAppContext: DbContext
    {
        public WebAppContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<DataFile> DataFiles { get; set; }

        //builder
        //TODO: move into partial class
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbo");

            builder.Entity<DataFile>(entity =>
            {
                entity.ToTable("data_file");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Name).HasColumnName("name");
                entity.Property(x => x.Description).HasColumnName("description");
                entity.Property(x => x.ContentType).HasColumnName("content_type");
                entity.Property(x => x.UploadDate).HasColumnName("upload_date").HasColumnType("timestamp"); ;
                entity.Property(x => x.Data).HasColumnName("data");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=webapptest;Username=postgres;Password=postgres"); //TODO: use appsettings.json
        }
    }
}
