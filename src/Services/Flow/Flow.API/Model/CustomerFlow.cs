namespace Microsoft.eShopOnDapr.Services.Flow.API.Model;

public class CustomerFlow
{
    public string BuyerId { get; set; } = "";

    public List<FlowItem> Items { get; set; } = new List<FlowItem>();

    public CustomerFlow()
    {

    }

    public CustomerFlow(string customerId)
    {
        BuyerId = customerId;
    }
}
