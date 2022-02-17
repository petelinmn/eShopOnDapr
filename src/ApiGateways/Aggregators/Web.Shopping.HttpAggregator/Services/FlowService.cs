namespace Microsoft.eShopOnDapr.Web.Shopping.HttpAggregator.Services;

public class FlowService : IFlowService
{
    private readonly HttpClient _httpClient;

    public FlowService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task UpdateAsync(FlowData currentFlow, string accessToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/flow")
        {
            Content = JsonContent.Create(currentFlow)
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}
