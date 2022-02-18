namespace Microsoft.eShopOnDapr.Web.Shopping.HttpAggregator.Controllers;

[Route("api/v1/[controller]")]
//[Authorize]
[ApiController]
public class FlowController : ControllerBase
{
    private readonly IFlowService _flow;

    public FlowController(IFlowService flowService)
    {
        _flow = flowService;
    }

    [HttpPost]
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(FlowData), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<FlowData>> UpdateFlowAsync(
        [FromBody] UpdateFlowRequest data,
        [FromHeader] string authorization)
    {
        // Save the updated shopping basket.
        await _flow.UpdateAsync(data.FlowData, authorization.Substring("Bearer ".Length));

        return data.FlowData;
    }
}
