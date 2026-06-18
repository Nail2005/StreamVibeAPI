using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<ContentGenre> ContentGenres { get; set; }

    public virtual DbSet<ContentLanguage> ContentLanguages { get; set; }

    public virtual DbSet<ContentPerson> ContentPeople { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<Faq> Faqs { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PricingPlan> PricingPlans { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Season> Seasons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LenovoSlim3\\SqlExpress01;Database=StreamVibeDb;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Content__3214EC0714D40E56");

            entity.ToTable("Content");

            entity.Property(e => e.BackgroundUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Background_url");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ImdbRating)
                .HasColumnType("decimal(3, 1)")
                .HasColumnName("Imdb_rating");
            entity.Property(e => e.IsFeatured)
                .HasDefaultValue(false)
                .HasColumnName("Is_featured");
            entity.Property(e => e.IsMustWatch)
                .HasDefaultValue(false)
                .HasColumnName("Is_must_watch");
            entity.Property(e => e.IsNewRelease)
                .HasDefaultValue(false)
                .HasColumnName("Is_new_release");
            entity.Property(e => e.IsTrending)
                .HasDefaultValue(false)
                .HasColumnName("Is_trending");
            entity.Property(e => e.PosterUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Poster_url");
            entity.Property(e => e.ReleaseYear).HasColumnName("Release_year");
            entity.Property(e => e.StreamvibeRating)
                .HasColumnType("decimal(3, 1)")
                .HasColumnName("Streamvibe_rating");
            entity.Property(e => e.Title)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.TopTenRank).HasColumnName("Top_ten_rank");
            entity.Property(e => e.TrailerUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Trailer_url");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ContentGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Content___3214EC07EDFD7187");

            entity.ToTable("Content_genres");

            entity.Property(e => e.ContentId).HasColumnName("Content_id");
            entity.Property(e => e.GenreId).HasColumnName("Genre_id");

            entity.HasOne(d => d.Content).WithMany(p => p.ContentGenres)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Content_g__Conte__5CD6CB2B");

            entity.HasOne(d => d.Genre).WithMany(p => p.ContentGenres)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Content_g__Genre__5DCAEF64");
        });

        modelBuilder.Entity<ContentLanguage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Content___3214EC07538CE99F");

            entity.ToTable("Content_languages");

            entity.Property(e => e.ContentId).HasColumnName("Content_id");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Content).WithMany(p => p.ContentLanguages)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Content_l__Conte__60A75C0F");
        });

        modelBuilder.Entity<ContentPerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Content___3214EC079B6485F5");

            entity.ToTable("Content_people");

            entity.Property(e => e.CharacterName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Character_name");
            entity.Property(e => e.ContentId).HasColumnName("Content_id");
            entity.Property(e => e.PersonId).HasColumnName("Person_id");
            entity.Property(e => e.RoleType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Role_type");

            entity.HasOne(d => d.Content).WithMany(p => p.ContentPeople)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Content_p__Conte__6C190EBB");

            entity.HasOne(d => d.Person).WithMany(p => p.ContentPeople)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Content_p__Perso__6D0D32F4");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Devices__3214EC07DF5949B9");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IconName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Icon_name");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Episode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Episodes__3214EC07B47B6ECA");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.DurationMinutes).HasColumnName("Duration_minutes");
            entity.Property(e => e.EpisodeNumber).HasColumnName("Episode_number");
            entity.Property(e => e.SeasonId).HasColumnName("Season_id");
            entity.Property(e => e.ThumbnailUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Thumbnail_url");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Season).WithMany(p => p.Episodes)
                .HasForeignKey(d => d.SeasonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Episodes__Season__66603565");
        });

        modelBuilder.Entity<Faq>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Faqs__3214EC07A552516E");

            entity.Property(e => e.Answer).HasColumnType("text");
            entity.Property(e => e.OrderNumber).HasColumnName("Order_number");
            entity.Property(e => e.Question)
                .HasMaxLength(300)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genres__3214EC076750D77D");

            entity.HasIndex(e => e.Slug, "UQ__Genres__BC7B5FB65B6BF934").IsUnique();

            entity.Property(e => e.CoverImage1)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Cover_image_1");
            entity.Property(e => e.CoverImage2)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Cover_image_2");
            entity.Property(e => e.CoverImage3)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Cover_image_3");
            entity.Property(e => e.CoverImage4)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Cover_image_4");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Slug)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__People__3214EC0708B3E735");

            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Avatar_url");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nationality)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PricingPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pricing___3214EC0725331B8A");

            entity.ToTable("Pricing_plans");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsPopular)
                .HasDefaultValue(false)
                .HasColumnName("Is_popular");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PriceMonthly)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("Price_monthly");
            entity.Property(e => e.PriceYearly)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("Price_yearly");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC0710E314A7");

            entity.Property(e => e.ContentId).HasColumnName("Content_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.Rating).HasColumnType("decimal(3, 1)");
            entity.Property(e => e.ReviewText)
                .HasColumnType("text")
                .HasColumnName("Review_text");
            entity.Property(e => e.ReviewerLocation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Reviewer_location");
            entity.Property(e => e.ReviewerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Reviewer_name");

            entity.HasOne(d => d.Content).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__Content__6FE99F9F");
        });

        modelBuilder.Entity<Season>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seasons__3214EC07CBD9880B");

            entity.Property(e => e.ContentId).HasColumnName("Content_id");
            entity.Property(e => e.EpisodeCount).HasColumnName("Episode_count");
            entity.Property(e => e.SeasonNumber).HasColumnName("Season_number");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Content).WithMany(p => p.Seasons)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Seasons__Content__6383C8BA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
