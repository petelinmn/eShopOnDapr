namespace Microsoft.eShopOnDapr.Services.Flow.API.Infrastructure.Repositories;

public class DaprFlowRepository : IFlowRepository
{
    private const string StoreName = "eshop-statestore";

    private readonly DaprClient _daprClient;
    private readonly ILogger _logger;

    private string CustomerId(int id) => $"flowId{id}";

    public DaprFlowRepository(DaprClient daprClient, ILogger<DaprFlowRepository> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    public Task DeleteFlowAsync(string id) =>
        _daprClient.DeleteStateAsync(StoreName, id);

    public Task<FlowData> GetFlowAsync(int flowId) =>
        _daprClient.GetStateAsync<FlowData>(StoreName, CustomerId(flowId));

    public async Task<FlowData> UpdateFlowAsync(FlowData flow)
    {
        var state = await _daprClient.GetStateEntryAsync<FlowData>(StoreName, CustomerId(flow.Id));
        state.Value = flow;

        await state.SaveAsync();

        _logger.LogInformation("Flow item persisted successfully.");

        return await GetFlowAsync(flow.Id);
    }
}
