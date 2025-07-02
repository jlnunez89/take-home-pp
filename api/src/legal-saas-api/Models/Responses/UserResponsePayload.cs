namespace LegalSaaS.Api.Models.Responses;

public class UserResponsePayload
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirmName { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
}
