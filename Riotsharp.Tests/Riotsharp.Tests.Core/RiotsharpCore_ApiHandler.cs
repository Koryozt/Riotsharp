using Riotsharp.Core;
using Riotsharp.Core.Exceptions;
using System.Net;
using Xunit.Abstractions;

namespace Riotsharp.Tests;

public class RiotsharpCore_ApiHandler :
    ApiHandler
{
    private readonly ITestOutputHelper _output;
    private static string TestingApiKey = File
        .ReadLines("../../../apiKey.txt")
        .First();

    public RiotsharpCore_ApiHandler(ITestOutputHelper output) : 
        base(TestingApiKey)
    {
        _output = output;
    }

    [Fact]
    public async Task Request_ApiKeyAdded_Return200()
    {
        var response =  await ExecuteRawAsync(
            RoutingValue.la1,
            Platform.lol,
            Versioning.v3,
            "platform",
            new Dictionary<string, string?>{ 
                ["champion-rotations"] = string.Empty 
            },
            null,
            HttpMethod.Get);

        _output.WriteLine(
            (await response.Content.ReadAsStringAsync())
            .ToString());

        Assert.True(response.StatusCode == HttpStatusCode.OK);
    }

    [Fact]
    public async Task Request_InvalidUrl_Return403()
    {
        var exception = await Record.ExceptionAsync(async () =>
        {
            var response = await ExecuteRawAsync(
                 RoutingValue.americas,
                 Platform.lol,
                 Versioning.v3,
                 "whatever",
                 [],
                 null,
                 HttpMethod.Get);
        });

        Assert.IsType<UnsuccesfulRequestException>(exception);
    }


}
