using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class ContentGenre
{
    public int Id { get; set; }

    public int ContentId { get; set; }

    public int GenreId { get; set; }

    public virtual Content Content { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;
}
