namespace Microsoft.eShopOnDapr.Services.Flow.API.Model;

public interface IFlowRepository
{
    Task<CustomerFlow> GetFlowAsync(string customerId);
    Task<CustomerFlow> UpdateFlowAsync(CustomerFlow flow);
    Task DeleteFlowAsync(string id);
}
