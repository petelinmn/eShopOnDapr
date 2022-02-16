namespace Microsoft.eShopOnDapr.Services.Flow.API.IntegrationEvents.Events;

public record OrderStatusChangedToSubmittedIntegrationEvent(
    Guid OrderId,
    string OrderStatus,
    string BuyerId)
    : IntegrationEvent;
