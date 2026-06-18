using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class Person
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public string? Nationality { get; set; }

    public virtual ICollection<ContentPerson> ContentPeople { get; set; } = new List<ContentPerson>();
}
