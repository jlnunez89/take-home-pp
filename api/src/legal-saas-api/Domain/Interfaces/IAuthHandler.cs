using LegalSaaS.Api.Models.Requests;
using LegalSaaS.Api.Models.Responses;

namespace LegalSaaS.Api.Domain.Interfaces;

public interface IAuthHandler
{
    Task<AuthTokenResponsePayload> SignupAsync(SignupRequestPayload request);
    Task<AuthTokenResponsePayload> LoginAsync(LoginRequestPayload request);
    Task<UserResponsePayload> GetCurrentUserAsync(int userId);
}
