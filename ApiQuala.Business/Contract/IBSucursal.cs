using ApiQuala.Entities.Class.Models;

namespace ApiQuala.Business.Contract
{
    public interface IBSucursal
    {
        List<ESucursales> GetSucursales(ESucursales eSucursales);
        bool InsertSucursales(ESucursales eSucursales);
        bool DeleteSucursales(ESucursales eSucursales);
    }
}
