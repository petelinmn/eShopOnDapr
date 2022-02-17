namespace Microsoft.eShopOnDapr.Web.Shopping.HttpAggregator.Services;

public interface IFlowService
{
    Task UpdateAsync(FlowData currentFlow, string accessToken);
}
