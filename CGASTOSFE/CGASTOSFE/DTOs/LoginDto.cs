using System.ComponentModel.DataAnnotations;

namespace CGASTOSFE.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El correo es requerido.")]
        [EmailAddress(ErrorMessage = "El correo no es una dirección de correo electrónico válida.")]
        public string Correo { get; set; } = default!;

        [Required(ErrorMessage = "La clave es requerida.")]
        //cual es el tamaño de la clave?
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La clave debe tener entre 6 y 100 caracteres.")]
        public string Clave { get; set; } = default!;
    }

    public class LoginResponseDto
    {
        public string Token { get; set; } = default!;
    }
}
