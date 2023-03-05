using ApiQuala.Entities.Class.Models;

namespace ApiQuala.Business.Contract
{
    public interface IBMonedas
    {
        List<EMonedas> GetMonedas(EMonedas eMonedas, bool isSucursal);
        bool InsertMonedas(EMonedas eMonedas);
    }
}
