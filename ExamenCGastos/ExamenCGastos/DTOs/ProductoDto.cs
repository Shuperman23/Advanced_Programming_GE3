using System.ComponentModel.DataAnnotations;

namespace ExamenCGastos.DTOs
{
    public class ProductoDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "El Id no puede ser un número negativo.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Range(0, int.MaxValue, ErrorMessage = "El ProveedorId no puede ser un número negativo.")]
        public int? ProveedorId { get; set; }
    }
}