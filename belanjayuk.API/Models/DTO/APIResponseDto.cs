namespace belanjayuk.API.Models.DTO
{
    public class APIResponseDto<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
