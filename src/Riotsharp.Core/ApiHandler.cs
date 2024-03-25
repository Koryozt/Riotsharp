using System.Text;

namespace Riotsharp.Core;

public abstract class ApiHandler
{
    private readonly HttpClient _client = new();
    private readonly string _baseAddress = "https://x.api.riotgames.com/";
    protected string ApiKey { get; }
    
    protected ApiHandler(string apiKey)
    {
        ApiKey = apiKey;
    }

    protected async Task<HttpResponseMessage> ExecuteRawAsync(
        RoutingValue value,
        Platform platform,
        Versioning version,
        string method,
        Dictionary<string, string?> queryOps,
        StringContent? content,
        HttpMethod httpMethod)
    {
        Uri uri = BuildUrl(
            value,
            platform,
            version,
            method,
            queryOps);

        HttpRequestMessage request = new()
        {
            Method = httpMethod,
            Content = content,
            RequestUri = uri
        };

        request.Headers.Add("X-Riot-Token", ApiKey);

        HttpResponseMessage response = await 
            _client.SendAsync(request);

        return response;
    }

    private Uri BuildUrl(
        RoutingValue value,
        Platform platform,
        Versioning version,
        string method,
        Dictionary<string, string?> queryOps)
    {
		StringBuilder sb = new(
            _baseAddress.Replace("x", value.ToString()));
    
        sb.AppendJoin(
            '/',
            platform,
            method,
            version,
            string.Join('/',
                queryOps
                    .Select(kv => !string.IsNullOrEmpty(kv.Value) ? 
                        $"{kv.Key}/{kv.Value}" :
                        $"{kv.Key}")
            )
        );

        Uri url = new(sb.ToString());

        return url;
    }
}
