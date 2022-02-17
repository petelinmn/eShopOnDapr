namespace Microsoft.eShopOnDapr.Web.Shopping.HttpAggregator.Models;

public record FlowDataItem(int ProductId, string ProductName, decimal UnitPrice, int Quantity, string PictureFileName);
