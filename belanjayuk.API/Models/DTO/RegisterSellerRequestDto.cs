namespace belanjayuk.API.Models.DTO
{
    public class RegisterSellerRequestDto
    {
        public string StoreName { get; set; } = null!;
        public string SellerDesc { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;


    }
}
