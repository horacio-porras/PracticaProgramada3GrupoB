
using MiPrimeraAPI.DAL.Entidades.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiPrimeraAPI.DAL.Personas
{
    public interface IPersonaRepositorio
    {
            IEnumerable<Persona> GetPersonas();
            Persona GetPersona(int id);
            void AddPersona(Persona persona);
            void UpdatePersona(int id, Persona persona);
            void DeletePersona(int id);
    }
}
