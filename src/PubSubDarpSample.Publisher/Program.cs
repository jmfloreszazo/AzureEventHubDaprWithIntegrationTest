// See https://aka.ms/new-console-template for more information

using Dapr.Client;
using PubSubDarpSample.Publisher;

await GenerateGuestActionsMessages("pub-microservice", "mydaprdemoeventhub");

async Task GenerateGuestActionsMessages(string pubSubName, string topic)
{
    var pubService = new EventService();
    await pubService.SendEventMessages(2, pubSubName, topic);

    var client = new DaprClientBuilder().Build();
    var secret = await client.GetSecretAsync("secret-local-store", "test");
}