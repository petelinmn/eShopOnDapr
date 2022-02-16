namespace Microsoft.eShopOnDapr.Services.Flow.API.Controllers;

[Route("api/v1/[controller]")]
[Authorize(Policy = "ApiScope")]
[ApiController]
public class FlowController : ControllerBase
{
    private readonly IFlowRepository _repository;
    private readonly IIdentityService _identityService;
    private readonly IEventBus _eventBus;
    private readonly ILogger<FlowController> _logger;

    public FlowController(
        IFlowRepository repository,
        IIdentityService identityService,
        IEventBus eventBus,
        ILogger<FlowController> logger)
    {
        _repository = repository;
        _identityService = identityService;
        _eventBus = eventBus;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CustomerFlow), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CustomerFlow>> GetFlowAsync()
    {
        var userId = _identityService.GetUserIdentity();
        var flow = await _repository.GetFlowAsync(userId);

        return Ok(flow ?? new CustomerFlow(userId));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerFlow), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CustomerFlow>> UpdateFlowAsync([FromBody] CustomerFlow value)
    {
        var userId = _identityService.GetUserIdentity();

        value.BuyerId = userId;

        return Ok(await _repository.UpdateFlowAsync(value));
    }

    [HttpPost("checkout")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> CheckoutAsync(
        [FromBody] FlowCheckout flowCheckout,
        [FromHeader(Name = "X-Request-Id")] string requestId)
    {
        var userId = _identityService.GetUserIdentity();

        var flow = await _repository.GetFlowAsync(userId);
        if (flow == null)
        {
            return BadRequest();
        }

        var eventRequestId = Guid.TryParse(requestId, out Guid parsedRequestId)
            ? parsedRequestId : Guid.NewGuid();

        var eventMessage = new UserCheckoutAcceptedIntegrationEvent(
            userId,
            flowCheckout.UserEmail,
            flowCheckout.City,
            flowCheckout.Street,
            flowCheckout.State,
            flowCheckout.Country,
            flowCheckout.CardNumber,
            flowCheckout.CardHolderName,
            flowCheckout.CardExpiration,
            flowCheckout.CardSecurityCode,
            eventRequestId,
            flow);

        // Once flow is checkout, sends an integration event to
        // ordering.api to convert flow to order and proceed with
        // order creation process
        await _eventBus.PublishAsync(eventMessage);

        return Accepted();
    }

    // DELETE api/values/5
    [HttpDelete]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task DeleteFlowAsync()
    {
        var userId = _identityService.GetUserIdentity();

        _logger.LogInformation("Deleting flow for user {UserId}...", userId);

        await _repository.DeleteFlowAsync(userId);
    }
}
