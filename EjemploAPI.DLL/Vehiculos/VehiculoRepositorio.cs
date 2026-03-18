using VehiculoEntidad = MiPrimeraAPI.DAL.Entidades.Vehiculo.Vehiculo;

namespace MiPrimeraAPI.DAL.Vehiculos
{
    public class VehiculoRepositorio : IVehiculoRepositorio
    {
        private static readonly List<VehiculoEntidad> vehiculos =
        [
            new VehiculoEntidad { Id = 1, Marca = "Toyota", Modelo = "Corolla", Año = 2020, Precio = 12500m },
            new VehiculoEntidad { Id = 2, Marca = "Honda", Modelo = "Civic", Año = 2021, Precio = 14200m },
            new VehiculoEntidad { Id = 3, Marca = "Hyundai", Modelo = "Elantra", Año = 2019, Precio = 10800m }
        ];

        public IEnumerable<VehiculoEntidad> GetVehiculos()
        {
            return vehiculos;
        }

        public VehiculoEntidad GetVehiculo(int id)
        {
            var vehiculo = vehiculos.FirstOrDefault(x => x.Id == id);
            if (vehiculo is null)
            {
                throw new KeyNotFoundException("Vehiculo no encontrado");
            }

            return vehiculo;
        }

        public void AddVehiculo(VehiculoEntidad vehiculo)
        {
            if (vehiculo is null)
            {
                throw new ArgumentNullException(nameof(vehiculo));
            }

            if (vehiculos.Any(x => x.Id == vehiculo.Id))
            {
                throw new InvalidOperationException("Ya existe un vehiculo con ese Id");
            }

            vehiculos.Add(vehiculo);
        }

        public void UpdateVehiculo(int id, VehiculoEntidad vehiculo)
        {
            if (vehiculo is null)
            {
                throw new ArgumentNullException(nameof(vehiculo));
            }

            var indice = vehiculos.FindIndex(x => x.Id == id);
            if (indice < 0)
            {
                throw new KeyNotFoundException("Vehiculo no encontrado");
            }

            if (id != vehiculo.Id && vehiculos.Any(x => x.Id == vehiculo.Id))
            {
                throw new InvalidOperationException("Ya existe un vehiculo con el nuevo Id");
            }

            vehiculos[indice] = vehiculo;
        }

        public void DeleteVehiculo(int id)
        {
            var vehiculo = vehiculos.FirstOrDefault(x => x.Id == id);
            if (vehiculo is null)
            {
                throw new KeyNotFoundException("Vehiculo no encontrado");
            }

            vehiculos.Remove(vehiculo);
        }
    }
}
