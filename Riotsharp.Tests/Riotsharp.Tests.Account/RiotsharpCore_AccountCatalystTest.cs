using Riotsharp.Account;
using Riotsharp.Account.Models;
using Riotsharp.Core;
using Riotsharp.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Riotsharp.Tests.Riotsharp.Tests.Account;

public class RiotsharpCore_AccountCatalystTest
{
    private readonly AccountCatalyst _accountCatalyst;
    private readonly ITestOutputHelper _output;
    private static string TestingApiKey = File
        .ReadLines("../../../apiKey.txt")
        .First();

    // I'm from LA1 so every endpoint
    // will be tested with that value.
    public RiotsharpCore_AccountCatalystTest(
        ITestOutputHelper output)
    {
        _output = output;
        _accountCatalyst = new(
            TestingApiKey, 
            RoutingValue.americas);
    }

    [Fact]
    public async Task Account_GetByRiotID_ParametersValid_Success()
    {
        AccountResponse response = await _accountCatalyst.GetAccountByRiotID(
            "LD Zette",
            "00001");

        _output.WriteLine(response.Puuid.ToString());

        Assert.NotNull(response);
        Assert.True(response.GameName == "LD Zette");
        Assert.IsType<AccountResponse>(response);
    }

    [Fact]
    public async Task Account_GetByRiotID_ParametersInvalid_ThrowsException()
    {
        var exception = await Record.ExceptionAsync(async () =>
        {
            await _accountCatalyst.GetAccountByRiotID(
                "LD Mamayensi",
                "zzzzz");
        });

        Assert.IsType<UnsuccesfulRequestException>(exception);
    }

    [Fact]
    public async Task Account_GetByPuuid_ParametersValid_Success()
    {

        AccountResponse response = await _accountCatalyst
            .GetAccountByPUUID(
                "VoyZ1SNaTVlafo1AC3dETxWpx01z1crbQjb3fYHg_OHNcx-r91coDcJ5BxiA8EFQHqVDS0aRMuFNjw");

        Assert.NotNull(response);
        Assert.True(response.GameName == "LD Zette");
        Assert.IsType<AccountResponse>(response);
    }

    [Fact]
    public async Task Account_GetActiveShards_ParametersValid_Success()
    {
        ActiveShardResponse response = await _accountCatalyst
            .GetAccountActiveShards(
                "val",
                "VoyZ1SNaTVlafo1AC3dETxWpx01z1crbQjb3fYHg_OHNcx-r91coDcJ5BxiA8EFQHqVDS0aRMuFNjw");

		Assert.NotNull(response);
		Assert.True(response.Game == "val");
		Assert.IsType<ActiveShardResponse>(response);
	}
}
