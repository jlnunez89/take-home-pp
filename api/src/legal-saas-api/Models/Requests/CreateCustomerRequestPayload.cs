using System.ComponentModel.DataAnnotations;

namespace LegalSaaS.Api.Models.Requests;

public class CreateCustomerRequestPayload
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;
}
