using belanjayuk.API.Models.DTO;

namespace belanjayuk.API.Services

{
    public interface IAuthService
    {
        Task<APIResponseDto<UserResponseDto>> RegisterUser(RegisterRequestDto request);
        Task<APIResponseDto<UserResponseDto>> LoginUser(LoginRequestDto request);
        Task<APIResponseDto<UserResponseDto>> LoginSeller(LoginRequestDto request);
        Task<APIResponseDto<UserResponseDto>> RegisterSeller(string userId, RegisterSellerRequestDto register);
    }
}
