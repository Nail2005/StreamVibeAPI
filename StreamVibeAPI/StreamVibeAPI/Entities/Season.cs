using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class Season
{
    public int Id { get; set; }

    public int ContentId { get; set; }

    public int SeasonNumber { get; set; }

    public int EpisodeCount { get; set; }

    public string? Title { get; set; }

    public virtual Content Content { get; set; } = null!;

    public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();
}
