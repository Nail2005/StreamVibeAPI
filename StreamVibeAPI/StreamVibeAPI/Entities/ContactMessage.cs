using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class ContactMessage
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneCountryCode { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Message { get; set; } = null!;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }
}
