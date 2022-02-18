namespace Microsoft.eShopOnDapr.Services.Flow.API.Model;

public class FlowData : IValidatableObject
{
    public int Id { get; set; }
    public string State { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();

        return results;
    }
}
