using VehiculoEntidad = MiPrimeraAPI.DAL.Entidades.Vehiculo.Vehiculo;

namespace MiPrimeraAPI.BLL.Vehiculo
{
    public interface IVehiculoServicio
    {
        IEnumerable<VehiculoEntidad> GetVehiculos();
        VehiculoEntidad GetVehiculo(int id);
        void AddVehiculo(VehiculoEntidad vehiculo);
        void UpdateVehiculo(int id, VehiculoEntidad vehiculo);
        void DeleteVehiculo(int id);
    }
}
