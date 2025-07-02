using System.ComponentModel.DataAnnotations;

namespace LegalSaaS.Api.Dal.Entities;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string FirmName { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
