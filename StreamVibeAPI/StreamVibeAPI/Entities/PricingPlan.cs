using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class PricingPlan
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal PriceMonthly { get; set; }

    public decimal PriceYearly { get; set; }

    public bool? IsPopular { get; set; }

    public virtual ICollection<PlanFeature> PlanFeatures { get; set; } = new List<PlanFeature>();

    public virtual ICollection<SubscriptionHistory> SubscriptionHistories { get; set; } = new List<SubscriptionHistory>();

    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
}
