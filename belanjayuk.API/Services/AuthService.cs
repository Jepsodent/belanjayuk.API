using belanjayuk.API.Data;
using belanjayuk.API.Models.DTO;
using belanjayuk.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

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

            if (!string.IsNullOrEmpty(request.IdGender))
            {
                var genderExist = await _context.LtGenders.AnyAsync(g => g.IdGender == request.IdGender);

                if (!genderExist)
                {
                    return new APIResponseDto<UserResponseDto>
                    {
                        IsSuccess = false,
                        Message = "Jenis kelamin tidak valid",
                        Data = null
                    };
                }
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

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newPassword = new Models.Entities.MsUserPassword
            {
                IdUserPassword = Guid.NewGuid().ToString(),
                IdUser = newUser.IdUser,
                PasswordHashed = hashedPassword,
                DateIn = DateTime.Now,
                IsActive = true,                
            };

            _context.MsUsers.Add(newUser);
            _context.MsUserPasswords.Add(newPassword);
            await _context.SaveChangesAsync();

            if(request.PrimaryAddress != null)
            {
                // TODO: Tambahin validasi kalau mau disisi frontend (panjang char dll)
                var newAddress = new Models.Entities.TrHomeAddress
                {
                    IdHomeAddress = Guid.NewGuid().ToString(),
                    IdUser = newUser.IdUser,
                    Provinsi = request.PrimaryAddress.Provinsi,
                    KotaKabupaten = request.PrimaryAddress.KotaKabupaten,
                    Kecamatan = request.PrimaryAddress.Kecamatan,
                    KodePos = request.PrimaryAddress.KodePos,
                    HomeAddressDesc = request.PrimaryAddress.HomeAddressDesc,
                    IsPrimaryAddress = true,
                    DateIn = DateTime.Now,
                    IsActive = true
                };
                _context.TrHomeAddresses.Add(newAddress);
                await _context.SaveChangesAsync();
            }

            var responseData = new UserResponseDto
            {
                Email = newUser.Email,
                UserName = newUser.UserName,
                IdUser = newUser.IdUser,
                Token = null //TODO: generate jwt token ya
            };

            return new APIResponseDto<UserResponseDto>
            {
                IsSuccess = true,
                Message = "User registered successfully",
                Data = responseData
            };
        }


        public async Task<APIResponseDto<UserResponseDto>> LoginUser(LoginRequestDto request)
        {
            var userExist = await _context.MsUsers.FirstOrDefaultAsync(u => u.Email == request.PhoneOrEmail || u.PhoneNumber == request.PhoneOrEmail);

            if(userExist == null || userExist.IsActive == false)
            {
                return new APIResponseDto<UserResponseDto>
                {
                    IsSuccess = false,
                    Message = "User not found or inactive",
                    Data = null
                };
            }
            var userPassword = await _context.MsUserPasswords.FirstOrDefaultAsync(p => p.IdUser == userExist.IdUser && p.IsActive == true);
            if(userPassword == null)
            {
                return new APIResponseDto<UserResponseDto>
                {
                    IsSuccess = false,
                    Message = "Password is not found", 
                    Data = null
                };
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, userPassword.PasswordHashed);

            if (!isPasswordValid)
            {
                return new APIResponseDto<UserResponseDto>
                {
                    IsSuccess = false,
                    Message = "Invalid password",
                    Data = null
                };
            }

            var responseData = new UserResponseDto
            {
                IdUser = userExist.IdUser,
                UserName = userExist.UserName,
                Email = userExist.Email,
                Token = "DUMMY DULU BOS" // TODO : Implement JWT TOKEN nanti
            };

                return new APIResponseDto<UserResponseDto>
            {
                IsSuccess = true,
                Message = "Successfully login",
                Data = responseData
            };
        }

    }
}
