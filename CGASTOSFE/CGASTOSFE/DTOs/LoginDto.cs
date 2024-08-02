namespace CGASTOSFE.DTOs
{
    public class LoginDto
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public class LoginResponseDto
    {
        public string Token { get; set; } = default!;
    }
}
