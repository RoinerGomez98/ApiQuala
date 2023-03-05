using ApiQuala.Business.Contract;
using ApiQuala.Entities.Class.Dto;
using ApiQuala.Entities.Class.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiQuala.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MonedasController : ControllerBase
    {
        private readonly IBMonedas _Monedas;
        public MonedasController(IBMonedas monedas)
        {
            _Monedas = monedas;
        }
        [HttpPost]
        [Route("GetMonedas")]
        public IEnumerable<EMonedas> GetMonedas(bool isSucursal)
        {
            EMonedas monedas = new EMonedas();
            return _Monedas.GetMonedas(monedas, isSucursal);
        }
        [HttpPost]
        [Route("MonedasByCodigo")]
        public EMonedas MonedasByCodigo(string codigo)
        {
            EMonedas monedas = new EMonedas
            {
                Codigo = codigo
            };
            return _Monedas.GetMonedas(monedas,false).FirstOrDefault();
        }

        [HttpPost]
        [Route("InsertMonedas")]
        public ResultApi InsertMonedas([FromBody] EMonedas monedas)
        {
            if (_Monedas.InsertMonedas(monedas))
            {
                ResultApi resultInsUp = new ResultApi()
                {
                    Result = true,
                    Message = "Se registro información exitosamente"
                };

                return resultInsUp;
            }
            else
            {
                ResultApi resultInsUp = new ResultApi()
                {
                    Result = false,
                    Message = "No se pudo registrar información"
                };

                return resultInsUp;
            }
        }


    }
}
