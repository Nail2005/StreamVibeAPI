using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DTOs;
using StreamVibeAPI.Entities;
using StreamVibeAPI.Exceptions;

namespace StreamVibeAPI.Business.Concrete
{
    public class SubscribeManager : ISubscribeService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;

        public SubscribeManager(ISubscriptionRepository subscriptionRepository, IPlanRepository planRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task CancelSubscriptionAsync(int userId)
        {
            var existingSubscription = await _subscriptionRepository.GetUserSubscriptionAsync(userId);

            if (existingSubscription == null)
            {
                throw new NotFoundException("No active subscription");
            }

            var history = new SubscriptionHistory
            {
                UserId = userId,
                PlanId = existingSubscription.PlanId,
                BillingCycle = existingSubscription.BillingCycle,
                IsTrial = existingSubscription.IsTrial,
                Status = "cancelled",
                StartedAt = existingSubscription.StartedAt,
                ExpiredAt = existingSubscription.ExpiresAt,
                CreatedAt = existingSubscription.CreatedAt
            };

            await _subscriptionRepository.AddHistoryAsync(history);
            
            await _subscriptionRepository.DeleteAsync(existingSubscription);    

            await _subscriptionRepository.SaveChangesAsync();   

        }

        public async Task<MySubscriptionDto> GetMySubscriptionAsync(int userId)
        {
            var existingSubscription = await _subscriptionRepository.GetUserSubscriptionAsync(userId);

            if (existingSubscription == null)
            {
                throw new NotFoundException("No active subscription");
            }

            var result = new MySubscriptionDto
            {
                Id = existingSubscription.Id,
                BillingCycle = existingSubscription.BillingCycle,
                IsTrial = existingSubscription.IsTrial,
                Status = existingSubscription.Status,
                StartedAt = existingSubscription.StartedAt,
                ExpiresAt = existingSubscription.ExpiresAt,

                Plan = new SubscriptionPlanInfoDto
                {
                    Id = existingSubscription.Plan.Id,
                    Name = existingSubscription.Plan.Name,
                    Description = existingSubscription.Plan.Description,
                    Price = existingSubscription.BillingCycle == "yearly" ? existingSubscription.Plan.PriceYearly
                                                                          : existingSubscription.Plan.PriceMonthly
                }
            };

            return result;

        }

        public async Task<List<SubscriptionPlanDto>> GetPlansAsync(string billing)
        {
            billing = (billing ?? "monthly").ToLower();
            var plans = await _planRepository.GetAllAsync();

            var result = plans.Select(x => new SubscriptionPlanDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = billing == "yearly" ? x.PriceYearly : x.PriceMonthly,
                IsPopular = x.IsPopular ?? false,
                BillingCycle = billing,
                Features = x.PlanFeatures
                                    .OrderBy(f => f.OrderNumber)
                                    .Select(f => new PlanFeatureDto
                                    {
                                        FeatureName = f.FeatureName,
                                        FeatureValue = f.FeatureValue,
                                        OrderNumber = f.OrderNumber
                                    }).ToList()

            }).ToList();

            return result;
        }

        public async Task<MySubscriptionDto> SubscribeAsync(int userId, SubscripeRequestDto request)
        {
            if (request.PlanId <= 0)
            {
                throw new BadRequestException("PlanId is required.");
            }

            if (request.BillingCycle != "monthly" && request.BillingCycle != "yearly")
            {
                throw new BadRequestException("Invalid billing cycle");
            }

            var plan = _planRepository.GetByIdAsync(request.PlanId);

            if (plan == null)
            {
                throw new NotFoundException("Plan not found");
            }

            var existingSubscription = await _subscriptionRepository.GetUserSubscriptionAsync(userId);

            if (existingSubscription != null)
            {
                throw new ConflictException("You already have an active subscription");
            }

            DateTime expiresAt;

            if(request.IsTrial)
            {
                expiresAt = DateTime.UtcNow.AddDays(7);
            }
            else if(request.BillingCycle == "montly")
            {
                expiresAt = DateTime.UtcNow.AddDays(30);   
            }
            else
            {
                expiresAt = DateTime.UtcNow.AddDays(365);
            }

            var subcription = new UserSubscription
            { 
                UserId = userId,
                PlanId = request.PlanId,
                BillingCycle = request.BillingCycle,
                IsTrial = request.IsTrial,      
                Status = "active",
                StartedAt = DateTime.UtcNow,
                ExpiresAt = expiresAt,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow 
            };

            await _subscriptionRepository.AddAsync(subcription);
            await _subscriptionRepository.SaveChangesAsync();

            var result = new MySubscriptionDto
            { 
                Id = subcription.Id,
                BillingCycle = subcription.BillingCycle,
                IsTrial = subcription.IsTrial,
                Status = subcription.Status,
                StartedAt = subcription.StartedAt,
                ExpiresAt = subcription.ExpiresAt,
            };

            return result;
        }
    }
}
