using Newtonsoft.Json;

namespace Riotsharp.Core;

public sealed record Status(
    string Message,
    [JsonProperty("status_code")] int Code
);
