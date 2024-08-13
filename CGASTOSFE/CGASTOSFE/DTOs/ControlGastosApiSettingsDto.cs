namespace CGASTOSFE.DTOs
{
    public class ControlGastosApiSettingsDto
    {
        public string ApiBaseUrl { get; set; } = default!;
        public string AuthUser { get; set; } = default!;
        public string AuthPass { get; set; } = default!;
    }
}
