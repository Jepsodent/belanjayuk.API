namespace belanjayuk.API.Models.DTO
{
    public class LoginRequestDto
    {
        public string PhoneOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
