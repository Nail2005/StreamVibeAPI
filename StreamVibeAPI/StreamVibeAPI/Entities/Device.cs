using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class Device
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? IconName { get; set; }
}
