namespace Microsoft.eShopOnDapr.BlazorClient.Flow;

public class UserFlow
{
    private readonly FlowClient _flowClient;

    public UserFlow(FlowClient flowClient)
    {
        _flowClient = flowClient
            ?? throw new ArgumentNullException(nameof(flowClient));
    }

    public FlowData FlowData { get; set; } = null;


    public event EventHandler? FlowChanged;

    public async Task LoadAsync()
    {
        FlowData = (await _flowClient.GetItemsAsync());

        OnFlowChanged(EventArgs.Empty);
    }

    public async Task<FlowData> GetData()
    {
        return await _flowClient.GetItemsAsync();
    }

    private void OnFlowChanged(EventArgs e)
    => FlowChanged?.Invoke(this, e);


    /*
    public async Task AddItemAsync(CatalogItem item)
    {
        var existingItem = Items.Find(
            basketItem => basketItem.ProductId == item.Id);

        if (existingItem != null)
        {
            await SetItemQuantityAsync(existingItem, existingItem.Quantity + 1);
        }
        else
        {
            Items.Add(new BasketItem(
                item.Id,
                item.Name,
                item.Price,
                1,
                item.PictureFileName));
        }

        await SaveItemsAsync();
    }

    public async Task RemoveItemAsync(BasketItem item)
    {
        Items.Remove(item);

        await SaveItemsAsync();
    }

    public async Task SetItemQuantityAsync(BasketItem item, int quantity)
    {
        var index = Items.IndexOf(item);
        if (index > -1 && quantity >= 1)
        {
            Items[index] = Items[index] with { Quantity = quantity }; 

            await SaveItemsAsync();
        }
    }

    public async Task CheckoutAsync(OrderForm orderForm)
    {
        var basketCheckout = new BasketCheckout(
            orderForm.Email!,
            orderForm.Street!,
            orderForm.City!,
            orderForm.State!,
            orderForm.Country!,
            orderForm.CardNumber!,
            orderForm.CardHolderName!,
            CardExpirationDate.Parse(orderForm.CardExpirationDate!),
            orderForm.CardSecurityCode!);

        await _basketClient.CheckoutAsync(basketCheckout);

        // Drop basket.
        Items.Clear();
        OnItemsChanged(EventArgs.Empty);
    }

    private async Task SaveItemsAsync()
    {
        var verifiedItems = await _basketClient.SaveItemsAsync(Items);

        Items = verifiedItems.ToList();

        OnItemsChanged(EventArgs.Empty);
    }
    */
}
