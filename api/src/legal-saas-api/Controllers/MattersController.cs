using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LegalSaaS.Api.Domain.Interfaces;
using LegalSaaS.Api.Models;
using LegalSaaS.Api.Models.Requests;

namespace LegalSaaS.Api.Controllers;

[ApiController]
[Route("api/Customers/{customerId}/matters")]
[Authorize]
public class MattersController : ControllerBase
{
    private readonly IMatterHandler _matterHandler;
    private readonly ILogger<MattersController> _logger;

    public MattersController(IMatterHandler matterHandler, ILogger<MattersController> logger)
    {
        _matterHandler = matterHandler;
        _logger = logger;
    }

    /// <summary>
    /// Retrieve matters for a customer
    /// </summary>
    /// <param name="customerId">Customer ID</param>
    /// <returns>List of matters for the customer</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Matter>>> GetMatters(int customerId)
    {
        try
        {
            _logger.LogInformation("Getting matters for customer ID: {CustomerId}", customerId);
            var matters = await _matterHandler.GetByCustomerIdAsync(customerId);
            return Ok(matters);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Customer not found: {Message}", ex.Message);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting matters for customer ID: {CustomerId}", customerId);
            return StatusCode(500, new { message = "An error occurred while retrieving matters" });
        }
    }

    /// <summary>
    /// Create a new matter for a customer
    /// </summary>
    /// <param name="customerId">Customer ID</param>
    /// <param name="createMatterDto">Matter data</param>
    /// <returns>Created matter</returns>
    [HttpPost]
    public async Task<ActionResult<Matter>> CreateMatter(int customerId, CreateMatterRequestPayload createMatterDto)
    {
        try
        {
            _logger.LogInformation("Creating new matter for customer ID: {CustomerId}, Title: {Title}", customerId, createMatterDto.Title);
            var matter = await _matterHandler.CreateAsync(customerId, createMatterDto);
            return CreatedAtAction(nameof(GetMatter), new { customerId = customerId, matterId = matter.Id }, matter);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Customer not found: {Message}", ex.Message);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating matter for customer ID: {CustomerId}", customerId);
            return StatusCode(500, new { message = "An error occurred while creating the matter" });
        }
    }

    /// <summary>
    /// Retrieve details of a specific matter
    /// </summary>
    /// <param name="customerId">Customer ID</param>
    /// <param name="matterId">Matter ID</param>
    /// <returns>Matter details</returns>
    [HttpGet("{matterId}")]
    public async Task<ActionResult<Matter>> GetMatter(int customerId, int matterId)
    {
        try
        {
            _logger.LogInformation("Getting matter ID: {MatterId} for customer ID: {CustomerId}", matterId, customerId);
            var matter = await _matterHandler.GetByIdAsync(customerId, matterId);
            return Ok(matter);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Matter not found: {Message}", ex.Message);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting matter ID: {MatterId} for customer ID: {CustomerId}", matterId, customerId);
            return StatusCode(500, new { message = "An error occurred while retrieving the matter" });
        }
    }
}
