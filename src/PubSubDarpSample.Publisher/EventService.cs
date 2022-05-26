using System.Text.Json;
using Dapr.Client;
using PubSubDaprSample.Contracts;

namespace PubSubDarpSample.Publisher;

public class EventService
{
    private readonly DaprClient _client;

    public EventService()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false
        };
        _client = new DaprClientBuilder().UseJsonSerializationOptions(options).Build();
    }

    public async Task SendEventMessages(int numberOfActions, string pubSubName, string topicName)
    {
        var objects = new List<SomeObject>();

        for (var i = 0; i < numberOfActions; i++)
        {
            var obj = createSomeObject();
            objects.Add(obj);
        }

        await PublishMessages(objects, pubSubName, topicName);
    }

    private SomeObject createSomeObject()
    {
        var guid = Guid.NewGuid().ToString();
        return new SomeObject
        {
            PropA = "Test A: " + guid,
            PropB = "Test B: " + guid
        };
    }

    private async Task PublishMessages(List<SomeObject> actions, string pubSubName, string topicName)
    {
        foreach (var action in actions)
            await _client.PublishEventAsync(pubSubName, topicName, action);
    }
}