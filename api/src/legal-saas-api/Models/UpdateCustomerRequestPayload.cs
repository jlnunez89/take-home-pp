using System.ComponentModel.DataAnnotations;

namespace LegalSaaS.Api.Models;

public class UpdateCustomerRequestPayload
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;
}
