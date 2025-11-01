namespace belanjayuk.API.Models.DTO
{
    public class UserResponseDto
    {
        public string IdUser { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Token { get; set; }
    }
}
