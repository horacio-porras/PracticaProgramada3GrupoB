using MiPrimeraAPI.DAL.Entidades.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MiPrimeraAPI.DAL.Personas
{
    public class PersonaRepositorio : IPersonaRepositorio
    {

        private static List<Persona> personas = new List<Persona>()
        {
            new Persona("Juan", "Perez", 30),
            new Persona("Maria", "Gomez", 25),
            new Persona("Carlos", "Lopez", 40)
        };

        public void AddPersona(Persona persona)
        {
            if (persona is null)
                throw new ArgumentNullException(nameof(persona));

            personas.Add(persona);
        }

        public void DeletePersona(int id)
        {
            if (id < 0 || id >= personas.Count)
                throw new ArgumentOutOfRangeException(nameof(id), "Persona no encontrada");

            personas.RemoveAt(id);
        }

        public Persona GetPersona(int id)
        {
            if (id < 0 || id >= personas.Count)
                throw new ArgumentOutOfRangeException(nameof(id), "Persona no encontrada");

            return personas[id];
        }

        public IEnumerable<Persona> GetPersonas()
        {
            HttpClient httpClient = new HttpClient();

            var prueba = httpClient.GetFromJsonAsync<List<Persona>>("https://localhost:7143/personas"); // Constantes. Configuracion

            return prueba.Result;
        }

        public void UpdatePersona(int id, Persona persona)
        {
            if (persona is null)
                throw new ArgumentNullException(nameof(persona));

            if (id < 0 || id >= personas.Count)
                throw new ArgumentOutOfRangeException(nameof(id), "Persona no encontrada");

            personas[id] = persona;
        }
    }
}
