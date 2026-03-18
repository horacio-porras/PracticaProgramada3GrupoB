using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.BLL.Persona;
using MiPrimeraAPI.DAL.Entidades.Persona;

namespace PrimeraAPI.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class PersonaController : ControllerBase
    {


        private IPersonaServicio personaServicio;

        public PersonaController(IPersonaServicio personaServicio)
        {
            this.personaServicio = personaServicio;
        }




        //API DEBERIAN TENER METODOS POST, GET, DELETE, REMOVE EN LAS PETICIONES

        //LISTA DE CODIGOS DE STATUS HTTP
        // 200 OK: La solicitud se ha procesado correctamente.
        // 201 Created: La solicitud se ha procesado correctamente y se ha creado un nuevo recurso.
        // 400 Bad Request: La solicitud no se ha podido procesar debido a un error del cliente (por ejemplo, datos inválidos).
        // 404 Not Found: El recurso solicitado no se ha encontrado.
        // 500 Internal Server Error: Se ha producido un error en el servidor al procesar la solicitud.


        /// <summary>
        /// Get all personas.
        /// </summary>
        [HttpGet(Name = "GetPersonas")] //El name no es diferenciador
        [ProducesResponseType(typeof(IEnumerable<Persona>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Persona>> GetPersonas() //Herencia
        {
            var personas = personaServicio.GetPersonas();

            return Ok(personas);

        }
        /*
        [HttpGet("{posicion}", Name = "GetPersona")] //El name no es diferenciador
        public ActionResult GetPersona(int posicion)
        {
            return Ok(personas[posicion]);
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(Persona), typeof(PrimeraAPI.OpenApiExamples.PersonaExample))]
        [ProducesResponseType(typeof(Persona), StatusCodes.Status201Created)]
        public ActionResult PostPersona(Persona persona) //la entidad viaje en el body de la peticion
        {
            personas.Add(persona);
            return Ok(persona);
        }


        [HttpPut("{posicion}")] //Parametro obligatorio 
        public ActionResult PutPersona(int posicion, Persona persona)
        {
            personas[posicion] = persona;
            return Ok(persona);
        }

        [HttpDelete("{posicion}")]
        public ActionResult DeletePersona(int posicion) //usar parametro obligatiorio para eliminar un registro, no es recomendable eliminar por nombre o apellido porque puede haber repetidos
        {
            personas.RemoveAt(posicion);
            return Ok();
        }
        */
    }
}
