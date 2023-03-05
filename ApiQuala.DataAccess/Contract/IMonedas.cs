using ApiQuala.Entities.Class.Models;

namespace ApiQuala.DataAccess.Contract
{
    public interface IMonedas
    {
        List<EMonedas> GetMonedas(EMonedas eMonedas,bool isSucursal);
        bool InsertMonedas(EMonedas eMonedas);
    }
}
