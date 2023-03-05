using ApiQuala.Business.Contract;
using ApiQuala.DataAccess.Contract;
using ApiQuala.Entities.Class.Models;

namespace ApiQuala.Business.Implement
{
    public class BSucursal : IBSucursal
    {
        private readonly ISucursales _Sucursales;
        public BSucursal(ISucursales sucursales)
        {
            _Sucursales = sucursales;
        }
        public List<ESucursales> GetSucursales(ESucursales eSucursales)
        {
            return _Sucursales.GetSucursales(eSucursales);
        }
        public bool InsertSucursales(ESucursales eSucursales)
        {
            return _Sucursales.InsertSucursales(eSucursales);
        }
        public bool DeleteSucursales(ESucursales eSucursales)
        {
            return _Sucursales.DeleteSucursales(eSucursales);
        }
    }
}
