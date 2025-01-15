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
        public DbSet<DataFileColumns> DataFileColumns { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<DataSetTable> DataSetTables { get; set; }
        public DbSet<DataSetColumns> DataSetColumns { get; set; }
        public DbSet<ColumnTypes> ColumnTypes { get; set; }


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

            builder.Entity<DataFileColumns>(entity =>
            {
                entity.ToTable("data_file_columns");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Name).HasColumnName("name");
                entity.Property(x => x.DataFileId).HasColumnName("data_file_id");
                entity.Property(x => x.ColumnTypeId).HasColumnName("column_type_id");

                entity.HasOne(x => x.ColumnTypes)
                    .WithMany(x => x.Columns)
                    .HasForeignKey(x => x.ColumnTypeId);

                entity.HasOne(x => x.DataFile)
                    .WithMany(x => x.Columns)
                    .HasForeignKey(x => x.DataFileId);

            });

            builder.Entity<Queue>(entity =>
            {
                entity.ToTable("queue");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.EntityId).HasColumnName("entity_id");
                entity.Property(x => x.QueueType).HasColumnName("queue_type");
                entity.Property(x => x.DateCreate).HasColumnName("date_create").HasColumnType("timestamp");
                entity.Property(x => x.DateUpdate).HasColumnName("date_update").HasColumnType("timestamp");
                entity.Property(x => x.IsCompleted).HasColumnName("is_complited");
                entity.Property(x => x.DatasetTableGuid).HasColumnName("dataset_table_guid");
            });

            builder.Entity<DataSetTable>(entity =>
            {
                entity.ToTable("dataset_table");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Name).HasColumnName("name");
                entity.Property(x => x.CreateDate).HasColumnName("create_date").HasColumnType("timestamp");
                entity.Property(x => x.UpdateDate).HasColumnName("update_date").HasColumnType("timestamp");
                entity.Property(x => x.DataFileId).HasColumnName("data_file_id");

                entity.HasMany(x => x.Columns)
                    .WithOne(x => x.DataSetTable)
                    .HasForeignKey(x => x.DataSetTableId);

                entity.HasOne(x => x.DataFiles)
                    .WithMany(x => x.DataSetTables)
                    .HasForeignKey(x => x.DataFileId);
            });

            builder.Entity<DataSetColumns>(entity =>
            {
                entity.ToTable("dataset_column");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Name).HasColumnName("name");
                entity.Property(x => x.Type).HasColumnName("type");
                entity.Property(x => x.Description).HasColumnName("description");
                entity.Property(x => x.Length).HasColumnName("length");
                entity.Property(x => x.DataSetTableId).HasColumnName("dataset_table_id");

                entity.HasOne(x => x.DataSetTable)
                    .WithMany(x => x.Columns) 
                    .HasForeignKey(x => x.DataSetTableId);
            });

            builder.Entity<ColumnTypes>(entity =>
            {
                entity.ToTable("column_types");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Name).HasColumnName("name");

                entity.HasData(new List<ColumnTypes>()
                {
                    new ColumnTypes(){ Id = 1, Name = "Dimension" },
                    new ColumnTypes(){ Id = 2, Name = "Fact Value" }
                });
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=webapptest;Username=postgres;Password=postgres"); //TODO: use appsettings.json
        }
    }
}
