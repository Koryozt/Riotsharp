namespace Riotsharp.Core;

// Riot says this does not always apply so... let keep it nullable at all

public sealed record Status(
    string? Message,
    int? StatusCode
);
