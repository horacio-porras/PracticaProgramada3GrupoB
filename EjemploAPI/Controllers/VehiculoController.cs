using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.BLL.Vehiculo;
using VehiculoEntidad = MiPrimeraAPI.DAL.Entidades.Vehiculo.Vehiculo;

namespace PrimeraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoServicio vehiculoServicio;

        public VehiculoController(IVehiculoServicio vehiculoServicio)
        {
            this.vehiculoServicio = vehiculoServicio;
        }

        /// <summary>
        /// Listado de todos los vehiculos registrados.
        /// </summary>
        [HttpGet(Name = "GetVehiculos")]
        [ProducesResponseType(typeof(IEnumerable<VehiculoEntidad>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VehiculoEntidad>> GetVehiculos()
        {
            return Ok(vehiculoServicio.GetVehiculos());
        }

        /// <summary>
        /// Obtiene el detalle de un vehiculo por Id.
        /// </summary>
        [HttpGet("{id:int}", Name = nameof(GetVehiculo))]
        [ProducesResponseType(typeof(VehiculoEntidad), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VehiculoEntidad> GetVehiculo(int id)
        {
            var vehiculo = vehiculoServicio.GetVehiculo(id);
            return Ok(vehiculo);
        }

        /// <summary>
        /// Registra un nuevo vehiculo.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(VehiculoEntidad), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VehiculoEntidad> PostVehiculo([FromBody] VehiculoEntidad vehiculo)
        {
            vehiculoServicio.AddVehiculo(vehiculo);
            return CreatedAtRoute(nameof(GetVehiculo), new { id = vehiculo.Id }, vehiculo);
        }

        /// <summary>
        /// Modifica la informacion de un vehiculo existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PutVehiculo(int id, [FromBody] VehiculoEntidad vehiculo)
        {
            vehiculoServicio.UpdateVehiculo(id, vehiculo);
            return NoContent();
        }

        /// <summary>
        /// Borra un vehiculo existente.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteVehiculo(int id)
        {
            vehiculoServicio.DeleteVehiculo(id);
            return NoContent();
        }
    }
}
