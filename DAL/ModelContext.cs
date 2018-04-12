using DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL
{
    public class ModelContext : DbContext
    {
        public ModelContext() : base()
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();

            var config = builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var connectionString = config.GetConnectionString("DbConnectionString");

            optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly("Migrations"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UrlDboMap());
            modelBuilder.ApplyConfiguration(new RssChannelItemDboMap());
            modelBuilder.ApplyConfiguration(new LogDboMap());
            modelBuilder.ApplyConfiguration(new UserDboMap());
            modelBuilder.ApplyConfiguration(new CommentMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ItemDbo> Items { get; set; }     
        public DbSet<DatasourceDbo> Datasources { get; set; }
        public DbSet<LogDbo> Logs { get; set; }
        public DbSet<UserDbo> Users { get; set; }
        public DbSet<CommentDbo> Comments { get; set; }
    }

    class CommentMap : IEntityTypeConfiguration<CommentDbo>
    {
        public void Configure(EntityTypeBuilder<CommentDbo> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.PubDate).HasMaxLength(30).IsRequired(true);
            builder.Property(i => i.Text).HasMaxLength(2000).IsRequired(true);
            builder.HasOne(i => i.User).WithMany(i => i.UserComments);
            builder.HasOne(i => i.Item).WithMany(i => i.ItemComments);
        }
    }

    class LogDboMap : IEntityTypeConfiguration<LogDbo>
    {
        public void Configure(EntityTypeBuilder<LogDbo> builder)
        {
            builder.ToTable("Logs");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Durotation).HasMaxLength(30).IsRequired(true);
            builder.Property(i => i.StartDate).HasMaxLength(30).IsRequired(true);
            builder.Property(i => i.ComplitionStatus).HasMaxLength(10).IsRequired(true);
            builder.Property(i => i.Error).HasMaxLength(300).IsRequired(false);
            builder.HasOne(i => i.Datasource);
        }
    }

    class UrlDboMap : IEntityTypeConfiguration<DatasourceDbo>
    {
        public void Configure(EntityTypeBuilder<DatasourceDbo> builder)
        {
            builder.ToTable("Datasources");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.TypeOfData).HasMaxLength(10).IsRequired(true);
            builder.Property(i => i.Url).HasMaxLength(500).IsRequired(true);
            builder.HasMany(i => i.Items).WithOne(i => i.Datasource);
        }
    }

    class RssChannelItemDboMap : IEntityTypeConfiguration<ItemDbo>
    {
        public void Configure(EntityTypeBuilder<ItemDbo> builder)
        {
            builder.ToTable("News");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Author).HasMaxLength(200).IsRequired(false);
            builder.Property(i => i.CategoryUrl).HasMaxLength(500).IsRequired(false);
            builder.Property(i => i.Comments).HasMaxLength(500).IsRequired(false);
            builder.Property(i => i.Description).HasMaxLength(1000).IsRequired(true);
            builder.Property(i => i.EnclosureUrl).HasMaxLength(500).IsRequired(false);
            builder.Property(i => i.Fulltext).IsRequired(false);
            builder.Property(i => i.Guid).HasMaxLength(500).IsRequired(false);
            builder.Property(i => i.Image).HasMaxLength(500).IsRequired(false);
            builder.Property(i => i.ItemExternalId).HasMaxLength(80).IsRequired(false);
            builder.Property(i => i.Link).HasMaxLength(500).IsRequired(true);
            builder.Property(i => i.PubDate).HasMaxLength(30).IsRequired(true);
            builder.Property(i => i.SourceUrl).HasMaxLength(500).IsRequired(false);
            builder.Property(i => i.Tags).HasMaxLength(500).IsRequired(false);
            builder.Property(i => i.Title).HasMaxLength(500).IsRequired(true);
            builder.HasOne(i => i.Datasource).WithMany(i => i.Items);
            builder.HasMany(i => i.ItemComments).WithOne(i => i.Item);
        }
    }

    class UserDboMap : IEntityTypeConfiguration<UserDbo>
    {
        public void Configure(EntityTypeBuilder<UserDbo> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Email).HasMaxLength(150).IsRequired(true);
            builder.Property(i => i.FirstName).HasMaxLength(100).IsRequired(true);
            builder.Property(i => i.Password).HasMaxLength(500).IsRequired(true);
            builder.Property(i => i.SecondName).HasMaxLength(150).IsRequired(true);
            builder.Property(i => i.DateOfRegistration).HasMaxLength(30).IsRequired(true);
            builder.Property(i => i.EmailIsValidate).IsRequired(true);
            builder.HasMany(i => i.UserComments).WithOne(i => i.User);
        }
    }
}
