using Microsoft.AspNetCore.Mvc;
using PubSubDaprSample.Contracts;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dapr;
using Newtonsoft.Json;

namespace PubSubDarpSample.Subscriptor.Controllers
{
    [ApiController]
    [Route("test")]
    [Consumes("application/json")]
    public class SubscriptorController : ControllerBase
    {

        private readonly ILogger<SubscriptorController> _logger;

        public SubscriptorController(ILogger<SubscriptorController> logger)
        {
            _logger = logger;
        }

        [HttpPost("subscriptor")]
        [Topic("sub-microservice", "mydaprdemoeventhub")]
        public async Task<IActionResult> Subscriptor(JsonElement obj)
        {
            var eventPayload = JsonConvert.DeserializeObject<SomeObject>(obj.ToString());
            return new OkResult();
        }
    }
}