using Newtonsoft.Json;
using Riotsharp.Account.Interfaces;
using Riotsharp.Account.Models;
using Riotsharp.Core;
using Riotsharp.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riotsharp.Account;

public sealed class AccountCatalyst : ApiHandler, IAccount
{
    private string DefaultInputMethod { get; }
    private HttpMethod DefaultMethod { get; }
    private Platform DefaultPlaftorm { get; }
    private Versioning DefaultVersion { get; }
    public RoutingValue RoutingValue { get; set; }
    
    public AccountCatalyst(
        string apiKey,
        RoutingValue value
        ) : base(apiKey)
    {
        DefaultInputMethod = "account";
        DefaultPlaftorm = Platform.riot;
        DefaultMethod = HttpMethod.Get;
        DefaultVersion = Versioning.v1;
        RoutingValue = value;
    }

    public async Task<AccountResponse> GetAccountByPUUID(string puuid)
    {
        Dictionary<string, string?> queryOPs = new()
        {
            ["accounts"] = string.Empty,
            ["by-puuid"] = puuid
        };

        HttpResponseMessage response = await ExecuteRawAsync(
            RoutingValue,
            DefaultPlaftorm,
            DefaultVersion,
            DefaultInputMethod,
            queryOPs,
            null,
            DefaultMethod);

        return JsonConvert
            .DeserializeObject<AccountResponse>(
                await response.Content.ReadAsStringAsync())!;
    }

    public async Task<AccountResponse> GetAccountByRiotID(string gameName, string tagLine)
    {
        Dictionary<string, string?> queryOPs = new()
        {
            ["accounts"] = string.Empty,
            ["by-riot-id"] = $"{gameName}/{tagLine}"
        };

        HttpResponseMessage response = await ExecuteRawAsync(
            RoutingValue,
            DefaultPlaftorm,
            DefaultVersion,
            DefaultInputMethod,
            queryOPs,
            null,
            DefaultMethod);

        return JsonConvert
            .DeserializeObject<AccountResponse>(
                await response.Content.ReadAsStringAsync())!;
    }

    public async Task<ActiveShardResponse> GetAccountActiveShards(string game, string puuid)
    {
        Dictionary<string, string?> queryOPs = new()
        {
            ["active-shards"] = string.Empty,
            ["by-game"] = game,
            ["by-puuid"] = puuid
        };

        HttpResponseMessage response = await ExecuteRawAsync(
            RoutingValue,
            DefaultPlaftorm,
            DefaultVersion,
            DefaultInputMethod,
            queryOPs,
            null,
            DefaultMethod);

        return JsonConvert
            .DeserializeObject<ActiveShardResponse>(
                await response.Content.ReadAsStringAsync())!;
    }
}
