using belanjayuk.API.Models.DTO;

namespace belanjayuk.API.Services

{
    public interface IAuthService
    {
        Task<APIResponseDto<UserResponseDto>> RegisterUser(RegisterRequestDto request);
        Task<APIResponseDto<UserResponseDto>> LoginUser(LoginRequestDto request);
    }
}
