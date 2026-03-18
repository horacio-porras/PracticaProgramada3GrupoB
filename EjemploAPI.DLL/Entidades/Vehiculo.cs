using System.ComponentModel.DataAnnotations;

namespace MiPrimeraAPI.DAL.Entidades.Vehiculo
{
    public class Vehiculo
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id debe ser mayor que 0")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Marca requerida")]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "Modelo requerido")]
        public string Modelo { get; set; } = string.Empty;

        [Range(1886, 2100, ErrorMessage = "Año fuera de rango")]
        public int Año { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Precio debe ser mayor o igual a 0")]
        public decimal Precio { get; set; }
    }
}
