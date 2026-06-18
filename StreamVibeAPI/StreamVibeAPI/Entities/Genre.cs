using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? CoverImage1 { get; set; }

    public string? CoverImage2 { get; set; }

    public string? CoverImage3 { get; set; }

    public string? CoverImage4 { get; set; }

    public virtual ICollection<ContentGenre> ContentGenres { get; set; } = new List<ContentGenre>();
}
