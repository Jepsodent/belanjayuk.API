namespace belanjayuk.API.Models.DTO
{
    public class RegisterRequestDto
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!; 
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DOB { get; set; }
        // Dari LtGender
        public string? IdGender { get; set; }
        public HomeAddressDto? PrimaryAddress { get; set;}
    }
}
