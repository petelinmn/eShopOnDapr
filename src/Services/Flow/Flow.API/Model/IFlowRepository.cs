namespace Microsoft.eShopOnDapr.Services.Flow.API.Model;

public interface IFlowRepository
{
    Task<FlowData> GetFlowAsync(int flowId);
    Task<FlowData> UpdateFlowAsync(FlowData flow);
    Task DeleteFlowAsync(string id);
}
