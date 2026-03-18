using System.ComponentModel.DataAnnotations;

namespace MiPrimeraAPI.DAL.Entidades.Persona
{
    /// <summary>
    /// Represents a person.
    /// </summary>
    public record Persona
    {
        /// <summary>
        /// First name.
        /// </summary>
        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { get; init; }

        /// <summary>
        /// Last name.
        /// </summary>
        [Required(ErrorMessage = "Apellido requerido")]
        public string Apellido { get; init; }

        /// <summary>
        /// Age (must be greater than 0).
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Edad debe ser mayor que 0")]
        public int Edad { get; init; }

        public Persona(string nombre, string apellido, int edad)
        {
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
        }
    }
}
