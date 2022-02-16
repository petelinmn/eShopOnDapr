namespace Microsoft.eShopOnDapr.Services.Flow.API.Infrastructure.Repositories;

public class DaprFlowRepository : IFlowRepository
{
    private const string StoreName = "eshop-statestore";

    private readonly DaprClient _daprClient;
    private readonly ILogger _logger;

    public DaprFlowRepository(DaprClient daprClient, ILogger<DaprFlowRepository> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    public Task DeleteFlowAsync(string id) =>
        _daprClient.DeleteStateAsync(StoreName, id);

    public Task<CustomerFlow> GetFlowAsync(string customerId) =>
        _daprClient.GetStateAsync<CustomerFlow>(StoreName, customerId);

    public async Task<CustomerFlow> UpdateFlowAsync(CustomerFlow flow)
    {
        var state = await _daprClient.GetStateEntryAsync<CustomerFlow>(StoreName, flow.BuyerId);
        state.Value = flow;

        await state.SaveAsync();

        _logger.LogInformation("Flow item persisted successfully.");

        return await GetFlowAsync(flow.BuyerId);
    }
}
