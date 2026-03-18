using MiPrimeraAPI.DAL.Vehiculos;
using VehiculoEntidad = MiPrimeraAPI.DAL.Entidades.Vehiculo.Vehiculo;

namespace MiPrimeraAPI.BLL.Vehiculo
{
    public class VehiculoServicio : IVehiculoServicio
    {
        private readonly IVehiculoRepositorio repo;

        public VehiculoServicio(IVehiculoRepositorio repo)
        {
            this.repo = repo;
        }

        public IEnumerable<VehiculoEntidad> GetVehiculos()
        {
            return repo.GetVehiculos();
        }

        public VehiculoEntidad GetVehiculo(int id)
        {
            return repo.GetVehiculo(id);
        }

        public void AddVehiculo(VehiculoEntidad vehiculo)
        {
            repo.AddVehiculo(vehiculo);
        }

        public void UpdateVehiculo(int id, VehiculoEntidad vehiculo)
        {
            repo.UpdateVehiculo(id, vehiculo);
        }

        public void DeleteVehiculo(int id)
        {
            repo.DeleteVehiculo(id);
        }
    }
}
