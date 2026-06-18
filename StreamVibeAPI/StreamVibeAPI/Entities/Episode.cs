using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class Episode
{
    public int Id { get; set; }

    public int SeasonId { get; set; }

    public int EpisodeNumber { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? ThumbnailUrl { get; set; }

    public int DurationMinutes { get; set; }

    public virtual Season Season { get; set; } = null!;
}
