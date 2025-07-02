using LegalSaaS.Api.Dal.Entities;
using LegalSaaS.Api.Domain.Interfaces;
using LegalSaaS.Api.Models.Requests;
using LegalSaaS.Api.Models.Responses;

using CryptoProvider = BCrypt.Net.BCrypt;

namespace LegalSaaS.Api.Domain.Handlers;

public class AuthHandler : IAuthHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthHandler(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<AuthTokenResponsePayload> SignupAsync(SignupRequestPayload request)
    {
        // Check if user already exists
        if (await _userRepository.EmailExistsAsync(request.Email))
        {
            throw new InvalidOperationException("User with this email already exists");
        }

        // Hash password
        var passwordHash = CryptoProvider.HashPassword(request.Password);

        // Create user
        var user = new User
        {
            Email = request.Email.ToLower(),
            PasswordHash = passwordHash,
            FirmName = request.FirmName
        };

        var createdUser = await _userRepository.CreateAsync(user);

        // Generate JWT token
        var token = _jwtService.GenerateToken(createdUser.Id, createdUser.Email);

        return new AuthTokenResponsePayload
        {
            Token = token,
            ExpiresAt = DateTimeOffset.UtcNow.AddHours(1), // Match token expiration
            User = new UserResponsePayload
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirmName = createdUser.FirmName,
                CreatedAt = createdUser.CreatedAt
            }
        };
    }

    public async Task<AuthTokenResponsePayload> LoginAsync(LoginRequestPayload request)
    {
        // Find user by email
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        // Verify password
        if (!CryptoProvider.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        // Generate JWT token
        var token = _jwtService.GenerateToken(user.Id, user.Email);

        return new AuthTokenResponsePayload
        {
            Token = token,
            ExpiresAt = DateTimeOffset.UtcNow.AddHours(1), // Match token expiration
            User = new UserResponsePayload
            {
                Id = user.Id,
                Email = user.Email,
                FirmName = user.FirmName,
                CreatedAt = user.CreatedAt
            }
        };
    }

    public async Task<UserResponsePayload> GetCurrentUserAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found");
        }

        return new UserResponsePayload
        {
            Id = user.Id,
            Email = user.Email,
            FirmName = user.FirmName,
            CreatedAt = user.CreatedAt
        };
    }
}
