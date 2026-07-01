using System;
using System.Collections.Generic;

namespace StreamVibeAPI.Entities;

public partial class PlanFeature
{
    public int Id { get; set; }

    public int PlanId { get; set; }

    public string FeatureName { get; set; } = null!;

    public string FeatureValue { get; set; } = null!;

    public int OrderNumber { get; set; }

    public virtual PricingPlan Plan { get; set; } = null!;
}
