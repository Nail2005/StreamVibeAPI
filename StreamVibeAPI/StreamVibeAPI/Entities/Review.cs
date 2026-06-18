using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class Review
{
    public int Id { get; set; }

    public int ContentId { get; set; }

    public string ReviewerName { get; set; } = null!;

    public string? ReviewerLocation { get; set; }

    public decimal Rating { get; set; }

    public string ReviewText { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Content Content { get; set; } = null!;
}
