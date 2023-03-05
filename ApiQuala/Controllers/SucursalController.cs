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
    public class SucursalController : ControllerBase
    {
        private readonly IBSucursal _Sucursal;
        public SucursalController(IBSucursal sucursal)
        {
            _Sucursal = sucursal;
        }

        [HttpGet]
        [Route("GetSucursal")]
        public IEnumerable<ESucursales> GetSucursal()
        {
            ESucursales sucursales = new ESucursales();
            return _Sucursal.GetSucursales(sucursales);
        }

        [HttpPost]
        [Route("GetSucursalByCodigo")]
        public ESucursales GetSucursalByCodigo(int codigo)
        {
            ESucursales sucursales = new ESucursales
            {
                Codigo = codigo
            };
            return _Sucursal.GetSucursales(sucursales).FirstOrDefault();
        }

        [HttpPost]
        [Route("InsertSucursal")]
        public ResultApi InsertSucursal([FromBody] ESucursales sucursales)
        {
            if (_Sucursal.InsertSucursales(sucursales))
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

        [HttpDelete]
        [Route("DeleteSucursal")]
        public ResultApi DeleteSucursal(ESucursales sucursales)
        {
            if (_Sucursal.DeleteSucursales(sucursales))
            {
                ResultApi resultInsUp = new ResultApi()
                {
                    Result = true,
                    Message = "Se eliminó información exitosamente"
                };

                return resultInsUp;
            }
            else
            {
                ResultApi resultInsUp = new ResultApi()
                {
                    Result = false,
                    Message = "No se pudo eliminar información"
                };

                return resultInsUp;
            }
        }
    }
}
