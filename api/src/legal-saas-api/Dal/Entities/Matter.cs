using System.ComponentModel.DataAnnotations;

namespace LegalSaaS.Api.Dal.Entities;

public class Matter
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "Open";

    // Foreign key to Customer
    public int CustomerId { get; set; }

    // Navigation property
    public Customer Customer { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
