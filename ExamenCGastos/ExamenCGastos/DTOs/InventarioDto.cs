using System.ComponentModel.DataAnnotations;

namespace ExamenCGastos.DTOs
{
    public class InventarioDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El Id no puede ser un número negativo.")]
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El ProductoId no puede ser un número negativo.")]
        public int? ProductoId { get; set; }

        [Required(ErrorMessage = "El TipoMovimiento es requerido.")]
        [RegularExpression("^(Salida|Ingreso)$", ErrorMessage = "El TipoMovimiento solo permite los valores 'Salida' o 'Ingreso'.")]
        public string? TipoMovimiento { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La Cantidad no puede ser un número negativo.")]
        public int Cantidad { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El Precio no puede ser un número negativo.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La fecha de movimiento es requerida.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(InventarioDto), nameof(ValidateFechaMovimiento))]
        public DateTime FechaMovimiento { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(InventarioDto), nameof(ValidateFechaCaducidad))]
        public DateTime? FechaCaducidad { get; set; }

        public static ValidationResult? ValidateFechaMovimiento(DateTime fechaMovimiento, ValidationContext context)
        {
            if (fechaMovimiento <= DateTime.Now)
            {
                return new ValidationResult("La fecha de movimiento debe ser mayor a la fecha actual.");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult? ValidateFechaCaducidad(DateTime? fechaCaducidad, ValidationContext context)
        {
            if (fechaCaducidad.HasValue && fechaCaducidad.Value <= DateTime.Now)
            {
                return new ValidationResult("La fecha de caducidad debe ser mayor a la fecha actual.");
            }
            return ValidationResult.Success;
        }
    }
}