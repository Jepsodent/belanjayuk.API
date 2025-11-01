using BCrypt.Net;
using belanjayuk.API.Data;
using belanjayuk.API.Models.DTO;
using belanjayuk.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace belanjayuk.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly BelanjaYukDbContext _context;
        private readonly IJwtService _jwtService;
        public AuthService(BelanjaYukDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }
        public async Task<APIResponseDto<UserResponseDto>> RegisterUser(RegisterRequestDto request)
        {
            var userExist = await _context.MsUsers.AnyAsync(u => u.Email == request.Email || u.PhoneNumber== request.PhoneNumber);

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

            var lastUserId =  await _context.MsUsers.OrderByDescending(u => u.IdUser).Select(u => u.IdUser).FirstOrDefaultAsync();
            int nextNumber = 1;
            if (!string.IsNullOrEmpty(lastUserId) && lastUserId.StartsWith("USR"))
            {
                var number = lastUserId.Substring(3);
                if(int.TryParse(number, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }

            }


            var newUser = new Models.Entities.MsUser
            {
                IdUser = "USR" + nextNumber.ToString("D3"),
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




            var lastPasswordId = await _context.MsUserPasswords.OrderByDescending(u => u.IdUserPassword).Select(u => u.IdUserPassword).FirstOrDefaultAsync();
            nextNumber = 1;
            if (!string.IsNullOrEmpty(lastPasswordId) && lastPasswordId.StartsWith("PASS"))
            {
                var number = lastPasswordId.Substring(4);
                if (int.TryParse(number, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }

            }

            var newPassword = new Models.Entities.MsUserPassword
            {
                IdUserPassword = "PASS" + nextNumber.ToString("D3"),
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

                var lastAddressId = await _context.TrHomeAddresses.OrderByDescending(u => u.IdHomeAddress).Select(u => u.IdHomeAddress).FirstOrDefaultAsync();
                nextNumber = 1;
                if (!string.IsNullOrEmpty(lastAddressId) && lastAddressId.StartsWith("HADDR"))
                {
                    var number = lastAddressId.Substring(5);
                    if (int.TryParse(number, out int lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }

                }
                var newAddress = new Models.Entities.TrHomeAddress
                {
                    IdHomeAddress = "HADDR" + nextNumber.ToString("D3"),
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
                Token = null
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
            
            var token = _jwtService.GenerateJwtToken(userExist, new List<string> { "Buyer" });

            var responseData = new UserResponseDto
            {
                IdUser = userExist.IdUser,
                UserName = userExist.UserName,
                Email = userExist.Email,
                Token =  token 
            };

                return new APIResponseDto<UserResponseDto>
            {
                IsSuccess = true,
                Message = "Successfully login",
                Data = responseData
            };
        }
        public async Task<APIResponseDto<UserResponseDto>> RegisterSeller(string userId, RegisterSellerRequestDto request)
        {
            var sellerExist = await _context.MsUserSellers.AnyAsync(s => s.IdUser == userId);
            if (sellerExist)
            {
                return new APIResponseDto<UserResponseDto>
                { IsSuccess = false, Message = "Anda sudah terdaftar sebagai penjual.", Data = null };
            }

            var storeNameExist = await _context.MsUserSellers.AnyAsync(s => s.StoreName == request.StoreName);
            if (storeNameExist)
            {
                return new APIResponseDto<UserResponseDto>
                { IsSuccess = false, Message = "Nama toko sudah digunakan, silakan pilih nama lain.", Data = null };
            }

            var user = await _context.MsUsers.FirstOrDefaultAsync(u => u.IdUser == userId);
            if (user == null)
            {
                return new APIResponseDto<UserResponseDto>
                { IsSuccess = false, Message = "User tidak ditemukan.", Data = null };
            }

            var lastSellerId = await _context.MsUserSellers
                .OrderByDescending(s => s.IdUserSeller)
                .Select(s => s.IdUserSeller)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (!string.IsNullOrEmpty(lastSellerId) && lastSellerId.StartsWith("SEL"))
            {

                if (int.TryParse(lastSellerId.Substring(3), out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            var newSeller = new MsUserSeller
            {
                IdUserSeller = "SEL" + nextNumber.ToString("D3"),
                IdUser = userId,
                StoreName = request.StoreName,
                SellerDesc = request.SellerDesc,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Email = user.Email,
                DateIn = DateTime.Now,
                IsActive = true
            };
            _context.MsUserSellers.Add(newSeller);
            await _context.SaveChangesAsync();
            var responseData = new UserResponseDto
            {
                IdUser = user.IdUser,
                UserName = user.UserName,
                Email = user.Email,
                Token = null // LOGIN ULANG SETELAH MENJADI PENJUAL BUAT DAPET TOKEN
            };

            return new APIResponseDto<UserResponseDto> { IsSuccess = true, Message = $"Toko {request.StoreName} berhasil didaftarkan. Silakan Login ulang.", Data = responseData };
        }
        public async Task<APIResponseDto<UserResponseDto>> LoginSeller(LoginRequestDto request)
        {
            var userExist = await _context.MsUsers.FirstOrDefaultAsync(u => u.Email == request.PhoneOrEmail || u.PhoneNumber == request.PhoneOrEmail);
            if (userExist == null || userExist.IsActive == false)
            {
                return new APIResponseDto<UserResponseDto>
                {
                    IsSuccess = false,
                    Message = "User not found or inactive",
                    Data = null
                };
            }
            var sellerExist = await _context.MsUserSellers.AnyAsync(s => s.IdUser == userExist.IdUser);
            if (!sellerExist)
            {
                return new APIResponseDto<UserResponseDto>
                {
                    IsSuccess = false,
                    Message = "User is not registered as a seller",
                    Data = null
                };
            }

            var userPassword = await _context.MsUserPasswords.FirstOrDefaultAsync(p => p.IdUser == userExist.IdUser && p.IsActive == true);
            if (userPassword == null)
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
            var token = _jwtService.GenerateJwtToken(userExist, new List<string> { "Buyer" , "Seller" });
            var responseData = new UserResponseDto
            {
                IdUser = userExist.IdUser,
                UserName = userExist.UserName,
                Email = userExist.Email,
                Token = token
            };
            return new APIResponseDto<UserResponseDto>
            {
                IsSuccess = true,
                Message = "Successfully login as seller",
                Data = responseData
            };
        }

    }
}
