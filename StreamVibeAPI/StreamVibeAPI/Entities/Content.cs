using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class Content
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string PosterUrl { get; set; } = null!;

    public string BackgroundUrl { get; set; } = null!;

    public string? TrailerUrl { get; set; }

    public string Type { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public decimal? ImdbRating { get; set; }

    public decimal? StreamvibeRating { get; set; }

    public bool? IsTrending { get; set; }

    public bool? IsNewRelease { get; set; }

    public bool? IsMustWatch { get; set; }

    public bool? IsFeatured { get; set; }

    public int? TopTenRank { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ContentGenre> ContentGenres { get; set; } = new List<ContentGenre>();

    public virtual ICollection<ContentLanguage> ContentLanguages { get; set; } = new List<ContentLanguage>();

    public virtual ICollection<ContentPerson> ContentPeople { get; set; } = new List<ContentPerson>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Season> Seasons { get; set; } = new List<Season>();
}
