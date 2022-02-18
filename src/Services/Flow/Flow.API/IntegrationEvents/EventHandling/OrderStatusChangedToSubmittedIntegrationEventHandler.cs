namespace Microsoft.eShopOnDapr.Services.Flow.API.IntegrationEvents.EventHandling;

public class OrderStatusChangedToSubmittedIntegrationEventHandler
    : IIntegrationEventHandler<OrderStatusChangedToSubmittedIntegrationEvent>
{
    private readonly IFlowRepository _repository;

    public OrderStatusChangedToSubmittedIntegrationEventHandler(
        IFlowRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(OrderStatusChangedToSubmittedIntegrationEvent @event) =>
        _repository.DeleteFlowAsync(@event.BuyerId);
}

