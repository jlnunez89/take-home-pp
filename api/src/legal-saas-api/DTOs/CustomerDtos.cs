using System.ComponentModel.DataAnnotations;

namespace LegalSaaS.Api.DTOs;

public class CreateCustomerDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;
}

public class UpdateCustomerDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;
}

public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
