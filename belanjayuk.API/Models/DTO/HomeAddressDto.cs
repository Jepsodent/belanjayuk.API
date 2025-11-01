namespace belanjayuk.API.Models.DTO
{
    public class HomeAddressDto
    {
        public string Provinsi { get; set; } = null!; 
        public string KotaKabupaten { get; set; } = null!; 
        public string Kecamatan { get; set; } = null!; 
        public string KodePos { get; set; } = null!; 
        public string HomeAddressDesc { get; set; } = null!; 
    }
}
