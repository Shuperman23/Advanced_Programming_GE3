using System.ComponentModel.DataAnnotations;

namespace CGASTOSFE.DTOs
{
    public class VerificarUsuarioDTO
    {
        [Required(ErrorMessage = "El correo es requerido.")]
        [EmailAddress(ErrorMessage = "El correo no es una dirección de correo electrónico válida.")]
        public string Correo { get; set; }
    }
}
