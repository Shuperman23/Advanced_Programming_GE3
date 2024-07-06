using System.ComponentModel.DataAnnotations;

namespace ExamenCGastos.DTOs
{
    public class UsuarioDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "El IdUsuario no puede ser un número negativo.")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El correo es requerido.")]
        [EmailAddress(ErrorMessage = "El correo no es una dirección de correo electrónico válida.")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "La clave es requerida.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La clave debe tener entre 6 y 100 caracteres.")]
        public string? Clave { get; set; }
    }
}
