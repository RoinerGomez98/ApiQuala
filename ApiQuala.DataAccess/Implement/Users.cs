using ApiQuala.DataAccess.Contract;
using ApiQuala.Entities.Class.Dto;
using ApiQuala.Entities.Class.Models;
using ApiQuala.Entities.Class.Utilities;
using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace ApiQuala.DataAccess.Implement
{
    public class Users : IUsers
    {
        SqlConnection cn = new SqlConnection(ApiDBContext.ConfiguracionApiDDB.cadenaConexion);
        SqlCommand cmd = new SqlCommand();
        ClassLog obj = new ClassLog();

        public EUsers Getuser(string username, string pass)
        {
            EUsers us = new EUsers();
            try
            {
                string where = $"WHERE Username ='{username}' and Password ='{pass}'";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"SELECT * FROM [TestQuala].Users {where}");
                cmd = new SqlCommand(sb.ToString(), cn);
                cn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            us.Username = rd["Username"].ToString();
                            us.Email = rd["Email"].ToString();
                            us.UsuarioEncontrado = true;
                        }
                    }
                }
                cn.Close();
                return us;

            }
            catch (Exception ex)
            {
                cn.Close();
                var msg = "Ha ocurrido un error en el proces: " + " | " + (ex.InnerException == null ? string.Empty : ex.InnerException.Message);
                var error = $"{ GetType().Namespace }.{ GetType().Name}.{ MethodBase.GetCurrentMethod().Name} : " + msg;
                obj.Add(error);
                return us;
            }
        }
    }
}
