using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace QueueService.Controllers;

[ApiController]
[Route("api/queue")]
public class QueueController : ControllerBase
{
    private readonly IDatabase _redis;

    public QueueController(IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }

    [HttpPost("next")]
    public async Task<IActionResult> GenerateNext()
    {
        var next = await _redis.StringIncrementAsync("queue:next");
        return Ok(new { ticketNumber = next });
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent()
    {
        var current = await _redis.StringGetAsync("queue:current");
        return Ok(new { current = current.HasValue ? current.ToString() : "0" });
    }

    [HttpPost("serve")]
    public async Task<IActionResult> ServeNext()
    {
        var next = await _redis.StringGetAsync("queue:next");
        await _redis.StringSetAsync("queue:current", next);
        return Ok(new { serving = next });
    }
}
