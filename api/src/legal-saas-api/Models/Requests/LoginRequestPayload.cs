using System.ComponentModel.DataAnnotations;

namespace LegalSaaS.Api.Models.Requests;

public class LoginRequestPayload
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
