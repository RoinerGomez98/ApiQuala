using ApiQuala.Entities.Class.Models;

namespace ApiQuala.DataAccess.Contract
{
    public interface ISucursales
    {
        List<ESucursales> GetSucursales(ESucursales eSucursales);
        bool InsertSucursales(ESucursales eSucursales);
        bool DeleteSucursales(ESucursales eSucursales);


    }
}
