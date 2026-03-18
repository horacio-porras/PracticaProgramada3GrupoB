
using MiPrimeraAPI.DAL.Entidades.Persona;
using Swashbuckle.AspNetCore.Filters;

namespace PrimeraAPI.OpenApiExamples
{
    // Provides example(s) for Persona used in Swagger UI
    public class PersonaExample : IExamplesProvider<Persona>
    {
        public Persona GetExamples()
        {
            return new Persona("Ana", "Rojas", 28);
        }
    }
}