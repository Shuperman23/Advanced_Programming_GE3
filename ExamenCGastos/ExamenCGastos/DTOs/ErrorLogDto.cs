namespace ExamenCGastos.DTOs
{
    public class ErrorLogDto
    {
        public int ErrorId { get; set; }
        public string? Controller { get; set; }
        public string? Endpoint { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ErrorStackTrace { get; set; }
        public DateTime ErrorTimestamp { get; set; }
    }
}
