namespace Microsoft.eShopOnDapr.Services.Flow.API.IntegrationEvents.Events;

public record UserCheckoutAcceptedIntegrationEvent(
    string UserId,
    string UserEmail,
    string City,
    string Street,
    string State,
    string Country,
    string CardNumber,
    string CardHolderName,
    DateTime CardExpiration,
    string CardSecurityNumber,
    Guid RequestId,
    CustomerFlow Flow)
    : IntegrationEvent;
