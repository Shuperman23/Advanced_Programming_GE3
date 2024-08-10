using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenCGastos.DTOs
{
    public class ProductoDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 200 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Range(0, int.MaxValue, ErrorMessage = "El ProveedorId no puede ser un número negativo.")]
        public int? ProveedorId { get; set; }
    }
}