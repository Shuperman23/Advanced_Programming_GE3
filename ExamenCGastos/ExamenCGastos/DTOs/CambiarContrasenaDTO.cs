using System.ComponentModel.DataAnnotations;

namespace ExamenCGastos.DTOs
{
    public class CambiarContrasenaDTO
    {

        [Required(ErrorMessage = "El correo es requerido.")]
        [EmailAddress(ErrorMessage = "El correo no es una dirección de correo electrónico válida.")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "La clave es requerida.")]
        //cual es el tamaño de la clave?
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La clave debe tener entre 6 y 100 caracteres.")]
        public string NuevaClave { get; set; }
    }
}
