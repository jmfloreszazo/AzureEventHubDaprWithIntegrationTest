using System;
using System.Net;
using System.Threading.Tasks;
using CloudNative.CloudEvents;
using CloudNative.CloudEvents.Http;
using CloudNative.CloudEvents.NewtonsoftJson;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PubSubDaprSample.Contracts;
using Xunit;

namespace TestProject;

public class DemoOptions
{
    public string OptionsConfigProperty { get; set; }
}

public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _tuneFactory;

    public UnitTest1(WebApplicationFactory<Program> factory)
    {
        _tuneFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.Configure<DemoOptions>(opts => { opts.OptionsConfigProperty = "OverriddenValue"; });
            });
        });
    }

    [Fact]
    public async Task Test1()
    {
        //Arrange
        var someObject = new SomeObject {PropA = "Test1", PropB = "Test2"};
        var url = "test/subscriptor";
        var evt = new CloudEvent
        {
            Type = "test",
            Source = new Uri("http://localhost"),
            Time = DateTimeOffset.Now,
            DataContentType = "application/json",
            Id = Guid.NewGuid().ToString(),
            Data = JsonConvert.SerializeObject(someObject)
        };
        var content = evt.ToHttpContent(ContentMode.Structured, new JsonEventFormatter());

        //Act
        var client = _tuneFactory.CreateClient();
        var response = await client.PostAsync(url, content);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}