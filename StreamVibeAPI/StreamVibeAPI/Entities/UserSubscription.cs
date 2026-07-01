using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class UserSubscription
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PlanId { get; set; }

    public string BillingCycle { get; set; } = null!;

    public bool IsTrial { get; set; }

    public string Status { get; set; } = null!;

    public DateTime StartedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual PricingPlan Plan { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
