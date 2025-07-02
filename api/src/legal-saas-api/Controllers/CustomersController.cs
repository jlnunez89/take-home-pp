using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LegalSaaS.Api.Models;
using LegalSaaS.Api.Models.Requests;
using LegalSaaS.Api.Domain.Interfaces;

namespace LegalSaaS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly ICustomerHandler _customerHandler;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ICustomerHandler customerHandler, ILogger<CustomersController> logger)
    {
        _customerHandler = customerHandler;
        _logger = logger;
    }

    /// <summary>
    /// Retrieve a list of customers
    /// </summary>
    /// <returns>List of customers</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        try
        {
            _logger.LogInformation("Getting all customers");
            var customers = await _customerHandler.GetAllAsync();
            return Ok(customers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting customers");
            return StatusCode(500, new { message = "An error occurred while retrieving customers" });
        }
    }

    /// <summary>
    /// Create a new customer
    /// </summary>
    /// <param name="createCustomerDto">Customer data</param>
    /// <returns>Created customer</returns>
    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomer(CreateCustomerRequestPayload createCustomerDto)
    {
        try
        {
            _logger.LogInformation("Creating new customer with name: {Name}", createCustomerDto.Name);
            var customer = await _customerHandler.CreateAsync(createCustomerDto);
            return CreatedAtAction(nameof(GetCustomer), new { customerId = customer.Id }, customer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating customer");
            return StatusCode(500, new { message = "An error occurred while creating the customer" });
        }
    }

    /// <summary>
    /// Retrieve details of a customer
    /// </summary>
    /// <param name="customerId">Customer ID</param>
    /// <returns>Customer details</returns>
    [HttpGet("{customerId}")]
    public async Task<ActionResult<Customer>> GetCustomer(int customerId)
    {
        try
        {
            _logger.LogInformation("Getting customer with ID: {CustomerId}", customerId);
            var customer = await _customerHandler.GetByIdAsync(customerId);
            return Ok(customer);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Customer not found: {Message}", ex.Message);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting customer with ID: {CustomerId}", customerId);
            return StatusCode(500, new { message = "An error occurred while retrieving the customer" });
        }
    }

    /// <summary>
    /// Update a customer
    /// </summary>
    /// <param name="customerId">Customer ID</param>
    /// <param name="updateCustomerDto">Updated customer data</param>
    /// <returns>Updated customer</returns>
    [HttpPut("{customerId}")]
    public async Task<ActionResult<Customer>> UpdateCustomer(int customerId, UpdateCustomerRequestPayload updateCustomerDto)
    {
        try
        {
            _logger.LogInformation("Updating customer with ID: {CustomerId}", customerId);
            var customer = await _customerHandler.UpdateAsync(customerId, updateCustomerDto);
            return Ok(customer);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Customer not found: {Message}", ex.Message);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating customer with ID: {CustomerId}", customerId);
            return StatusCode(500, new { message = "An error occurred while updating the customer" });
        }
    }

    /// <summary>
    /// Delete a customer
    /// </summary>
    /// <param name="customerId">Customer ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{customerId}")]
    public async Task<ActionResult> DeleteCustomer(int customerId)
    {
        try
        {
            _logger.LogInformation("Deleting customer with ID: {CustomerId}", customerId);
            await _customerHandler.DeleteAsync(customerId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Customer not found: {Message}", ex.Message);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting customer with ID: {CustomerId}", customerId);
            return StatusCode(500, new { message = "An error occurred while deleting the customer" });
        }
    }
}
