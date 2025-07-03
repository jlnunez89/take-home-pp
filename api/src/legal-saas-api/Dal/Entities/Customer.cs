using System.ComponentModel.DataAnnotations;

namespace LegalSaaS.Api.Dal.Entities;

public class Customer
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    // Navigation property
    public ICollection<Matter> Matters { get; set; } = new List<Matter>();

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
