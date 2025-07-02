using System.ComponentModel.DataAnnotations;

namespace LegalSaaS.Api.Models.Requests;

public class SignupRequestPayload
{
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string FirmName { get; set; } = string.Empty;
}
