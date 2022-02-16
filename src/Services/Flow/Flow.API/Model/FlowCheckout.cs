namespace Microsoft.eShopOnDapr.Services.Flow.API.Model;

public record FlowCheckout(
    string UserEmail,
    string City,
    string Street,
    string State,
    string Country,
    string CardNumber,
    string CardHolderName,
    DateTime CardExpiration,
    string CardSecurityCode
);


