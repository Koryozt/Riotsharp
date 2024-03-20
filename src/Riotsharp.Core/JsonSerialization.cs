using System.Text.Json;

namespace Riotsharp.Core;

public static class JsonSerialization<T>
{
    public static T? DeserializeResponse(
        HttpResponseMessage response)
    {
        try
        {
            response.EnsureSuccessStatusCode();

            var content = JsonSerializer.Deserialize<T>(
                response
                .Content
                .ToString()!);

            return content;

        } catch(HttpRequestException)
        {
            var ex = JsonSerializer
                .Deserialize<Status>(
                    response
                    .Content
                    .ToString()!);

            Console
                .WriteLine($"{ex!.StatusCode} - {ex.Message}");
            
            return default;       
        }
    }

    public static string SerializeResponse(
        T jsonContent)
    {
        if (jsonContent is null)
        {
            return string.Empty;
        }

        return JsonSerializer.Serialize(jsonContent);
    }
}
