namespace ControlBiblioteca.DTOs
{
    public class ApiRequestResultDto<TResult>
    {
        public TResult? Result { get; set; }

        public string? Message { get; set; }

        public bool Success { get; set; } = true;
    }
}