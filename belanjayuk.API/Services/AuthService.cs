using belanjayuk.API.Data;
using belanjayuk.API.Models.DTO;
using belanjayuk.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace belanjayuk.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly BelanjaYukDbContext _context;
        public AuthService(BelanjaYukDbContext context)
        {
            _context = context;
        }
        public async Task<APIResponseDto<UserResponseDto>> RegisterUser(RegisterRequestDto request)
        {
            var userExist = await _context.MsUsers.AnyAsync(u => u.UserName == request.UserName || u.Email == request.Email);

            if (userExist)
            {
                return new APIResponseDto<UserResponseDto>
                {
                    IsSuccess = false,
                    Message = "Username or Email already exists",
                    Data = null
                };
            }

            var newUser = new Models.Entities.MsUser
            {
                IdUser = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Dob = request.DOB,
                IdGender = request.IdGender,
                DateIn = DateTime.Now,
                UserIn = request.UserName,
                IsActive = true
            };

            var newPassword = new Models.Entities.MsUserPassword
            {
                IdUserPassword = Guid.NewGuid().ToString(),
                IdUser = newUser.IdUser,
                PasswordHashed = request.Password, //TODO: Nanti ganti sama bcrypt ya!
                DateIn = DateTime.Now,
                IsActive = true,                
            };

            _context.MsUsers.Add(newUser);
            _context.MsUserPasswords.Add(newPassword);
            await _context.SaveChangesAsync();



        }

        public async Task<APIResponseDto<UserResponseDto>> LoginUser(LoginRequestDto request)
        {
            return new APIResponseDto<UserResponseDto>
            {
                IsSuccess = true,
                Message = "Successfully login",
                Data = null
            };
        }

    }
}
