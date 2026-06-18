using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class ContentPerson
{
    public int Id { get; set; }

    public int ContentId { get; set; }

    public int PersonId { get; set; }

    public string RoleType { get; set; } = null!;

    public string? CharacterName { get; set; }

    public virtual Content Content { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
