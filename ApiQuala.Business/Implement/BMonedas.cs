using ApiQuala.Business.Contract;
using ApiQuala.DataAccess.Contract;
using ApiQuala.Entities.Class.Models;

namespace ApiQuala.Business.Implement
{
    public class BMonedas : IBMonedas
    {
        private readonly IMonedas _Monedas;
        public BMonedas(IMonedas monedas)
        {
            _Monedas = monedas;
        }
        public List<EMonedas> GetMonedas(EMonedas eMonedas,bool isSucursal)
        {
            return _Monedas.GetMonedas(eMonedas, isSucursal);
        }
       public bool InsertMonedas(EMonedas eMonedas)
        {
            return _Monedas.InsertMonedas(eMonedas);
        }
    }
}
