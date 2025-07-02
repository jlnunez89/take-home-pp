using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LegalSaaS.Api.Domain.Interfaces;
using LegalSaaS.Api.Models.Requests;
using LegalSaaS.Api.Models.Responses;

namespace LegalSaaS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthHandler _authHandler;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthHandler authHandler, ILogger<AuthController> logger)
    {
        _authHandler = authHandler;
        _logger = logger;
    }

    /// <summary>
    /// Create a new user account
    /// </summary>
    /// <param name="request">Signup request data</param>
    /// <returns>Authentication token and user info</returns>
    [HttpPost("signup")]
    public async Task<ActionResult<AuthTokenResponsePayload>> Signup(SignupRequestPayload request)
    {
        try
        {
            _logger.LogInformation("Creating new user account for email: {Email}", request.Email);
            var result = await _authHandler.SignupAsync(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Signup failed for email {Email}: {Message}", request.Email, ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during signup for email: {Email}", request.Email);
            return StatusCode(500, new { message = "An error occurred while creating the account" });
        }
    }

    /// <summary>
    /// Login with email and password
    /// </summary>
    /// <param name="request">Login request data</param>
    /// <returns>Authentication token and user info</returns>
    [HttpPost("login")]
    public async Task<ActionResult<AuthTokenResponsePayload>> Login(LoginRequestPayload request)
    {
        try
        {
            _logger.LogInformation("Login attempt for email: {Email}", request.Email);
            var result = await _authHandler.LoginAsync(request);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning("Login failed for email {Email}: {Message}", request.Email, ex.Message);
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during login for email: {Email}", request.Email);
            return StatusCode(500, new { message = "An error occurred while logging in" });
        }
    }

    /// <summary>
    /// Get current authenticated user info
    /// </summary>
    /// <returns>Current user info</returns>
    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserResponsePayload>> GetCurrentUser()
    {
        try
        {
            var userIdClaim = User.FindFirst("userId");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                _logger.LogWarning("Invalid or missing userId claim in token");
                return Unauthorized(new { message = "Invalid token" });
            }

            _logger.LogInformation("Getting current user info for userId: {UserId}", userId);
            var result = await _authHandler.GetCurrentUserAsync(userId);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning("Get current user failed: {Message}", ex.Message);
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting current user");
            return StatusCode(500, new { message = "An error occurred while getting user info" });
        }
    }
}
