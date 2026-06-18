using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class Faq
{
    public int Id { get; set; }

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public int OrderNumber { get; set; }
}
