using System.ComponentModel.DataAnnotations;

namespace CGASTOSFE.DTOs
{
    public class ProductoDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El Id no puede ser un numero negativo")]
        public int? Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 200 caracteres.")]
        public string Nombre { get; set; } = null!;
        
        [Range(0, int.MaxValue, ErrorMessage ="El ProveedorId no puede ser un numero negativo.")]
        public int? ProveedorId { get; set; }

    }
}
