using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiPrimeraAPI.BLL.Persona
{
    public interface IPersonaServicio
    {
        //DEBERIAMOS UTILIZAR DTOS PARA EVITAR EXPONER NUESTRA ENTIDAD DIRECTAMENTE, PERO PARA SIMPLICIDAD EN ESTE EJEMPLO SE UTILIZARÁ LA ENTIDAD DIRECTAMENTE
        IEnumerable<DAL.Entidades.Persona.Persona> GetPersonas();
        MiPrimeraAPI.DAL.Entidades.Persona.Persona GetPersona(int id);
        void AddPersona(MiPrimeraAPI.DAL.Entidades.Persona.Persona persona);
        void UpdatePersona(int id, MiPrimeraAPI.DAL.Entidades.Persona.Persona persona);
        void DeletePersona(int id);
    }
}
