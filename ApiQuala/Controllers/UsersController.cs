using ApiQuala.Business.Contract;
using ApiQuala.DataAccess.Implement;
using ApiQuala.Entities.Class.Dto;
using ApiQuala.Entities.Class.Models;
using ApiQuala.Entities.Class.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace ApiQuala.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IBUsers _User;
        private readonly Encrypt security = new();
        ClassLog obj = new ClassLog();
        public UsersController(IConfiguration config, IBUsers user)
        {
            _User = user;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<ResultApi> Login(Autenticacion autenticacion)
        {
            ResultApi resultLogin = new();
            try
            {
                var user = Authenticate(autenticacion.UserName, security.B64_Encode(autenticacion.Pass));

                if (user.Username != null)
                {
                    resultLogin = GenerateToken(user);
                    if (resultLogin.Token != null)
                    {
                        resultLogin.Message = "Usuario encontrado";
                        resultLogin.Result = user.UsuarioEncontrado;
                    }
                    return resultLogin;
                }

                resultLogin.Message = "Usuario no encontrado";
                resultLogin.Result = false;
                return resultLogin;
            }
            catch (Exception ex)
            {
                var msg = "Ha ocurrido un error en el proces: " + " | " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message);
                var error = $"{ GetType().Namespace }.{ GetType().Name}.{ MethodBase.GetCurrentMethod().Name} : " + msg;
                obj.Add(error);
            }
            return resultLogin;

        }

        private EUsers Authenticate(string userName, string pass)
        {
            EUsers currentUser = _User.Getuser(userName, pass);
            return currentUser;
        }

        private ResultApi GenerateToken(EUsers user)
        {
            if (user.Username == null)
                return null;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            };

            DateTime expira = DateTime.Now.AddMinutes(30);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: expira,
              signingCredentials: credentials);


            ResultApi resultToken = new ResultApi()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = expira
            };

            return resultToken;
        }
    }
}
