using System.ComponentModel.DataAnnotations;

namespace LegalSaaS.Api.Models.Requests;

public class UpdateCustomerRequestPayload
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    [RegularExpression(@"^(\+\d{1,3}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$|^(\+\d{1,3}\s?)?\d{8,15}$",
        ErrorMessage = "Please enter a valid phone number")]
    public string Phone { get; set; } = string.Empty;
}
