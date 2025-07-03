using System.ComponentModel.DataAnnotations;

namespace LegalSaaS.Api.Models.Requests;

public class CreateMatterRequestPayload
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [StringLength(50)]
    public string Status { get; set; } = "Open";
}
