using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class ContentLanguage
{
    public int Id { get; set; }

    public int ContentId { get; set; }

    public string Language { get; set; } = null!;

    public virtual Content Content { get; set; } = null!;
}
