namespace Microsoft.eShopOnDapr.Services.Flow.API.Controllers;

[Route("api/v1/[controller]")]
/*[Authorize(Policy = "ApiScope")]*/
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
    [ProducesResponseType(typeof(FlowData), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<FlowData>> GetFlowAsync()
    {
        return Ok(new
        {
            Id = 4,
            State = "Hello world!"
        });
    }

    // DELETE api/values/5
    [HttpDelete]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task DeleteFlowAsync()
    {
        await Task.Run(() => { });
    }
}
