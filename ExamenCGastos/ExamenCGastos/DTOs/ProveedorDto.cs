using System.ComponentModel.DataAnnotations;

namespace ExamenCGastos.DTOs
{
    public class ProveedorDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "El Id no puede ser un número negativo.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La dirección es requerida.")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "La dirección debe tener entre 10 y 250 caracteres.")]
        public string Direccion { get; set; } = null!;

        [Required(ErrorMessage = "El teléfono de contacto es requerido.")]
        [Phone(ErrorMessage = "El teléfono de contacto no es un número de teléfono válido.")]
        public string TelefonoContacto { get; set; } = null!;

        [Required(ErrorMessage = "El correo electrónico de contacto es requerido.")]
        [EmailAddress(ErrorMessage = "El correo electrónico de contacto no es una dirección de correo electrónico válida.")]
        public string EmailContacto { get; set; } = null!;
    }
}