namespace Microsoft.eShopOnDapr.BlazorClient.Flow;

public class FlowClient
{
    private readonly HttpClient _httpClient;

    public FlowClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<FlowData> GetItemsAsync()
    {
        var flowData = await _httpClient.GetFromJsonAsync<FlowData>(
            "f/api/v1/flow/");

        return flowData;
    }

    public async Task<FlowData> SaveItemsAsync(FlowData flowData)
    {
        // Save items is a request to the Aggregator service.
        var response = await _httpClient.PostAsJsonAsync(
            "api/v1/flow/",
            flowData);

        response.EnsureSuccessStatusCode();

        var flowDataResponse = await response.Content.ReadFromJsonAsync<FlowData>();
        return flowDataResponse;
    }
}
