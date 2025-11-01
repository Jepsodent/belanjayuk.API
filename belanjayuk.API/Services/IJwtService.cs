using belanjayuk.API.Models.Entities;

namespace belanjayuk.API.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(MsUser user);


    }
}
