namespace LegalSaaS.Api.Models.Responses;

public class AuthTokenResponsePayload
{
    public string Token { get; set; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; set; }
    public UserResponsePayload User { get; set; } = new();
}
