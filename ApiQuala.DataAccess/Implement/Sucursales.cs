using ApiQuala.DataAccess.Contract;
using ApiQuala.Entities.Class.Dto;
using ApiQuala.Entities.Class.Models;
using ApiQuala.Entities.Class.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Text;

namespace ApiQuala.DataAccess.Implement
{
    public class Sucursales : ISucursales
    {
        SqlConnection cn = new SqlConnection(ApiDBContext.ConfiguracionApiDDB.cadenaConexion);
        SqlCommand cmd = new SqlCommand();
        ClassLog obj = new ClassLog();
        public List<ESucursales> GetSucursales(ESucursales eSucursales)
        {
            List<ESucursales> clList = new List<ESucursales>();
            ESucursales cl = new ESucursales();
            try
            {
                string where = eSucursales.Codigo == 0 ? "" : $"WHERE Codigo = @Codigo";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"SELECT * FROM [TestQuala].Sucursales {where}");
                cmd = new SqlCommand(sb.ToString(), cn);
                if (!string.IsNullOrEmpty(where))
                {
                    cmd.Parameters.AddWithValue("@Codigo", eSucursales.Codigo);
                }
                cn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            cl = new ESucursales
                            {
                                Codigo = Convert.ToInt32(rd["Codigo"].ToString()),
                                Descripcion = rd["Descripcion"].ToString(),
                                Direccion = rd["Direccion"].ToString(),
                                Identificacion = rd["Identificacion"].ToString(),
                                FechaCreacion = Convert.ToDateTime(rd["FechaCreacion"].ToString()),
                                Moneda = rd["Moneda"].ToString(),
                                Estado = Convert.ToBoolean(rd["Estado"])
                            };
                            clList.Add(cl);
                        }
                    }
                    else
                    {
                        cn.Close();
                        return clList;

                    }
                }
                cn.Close();
                return clList;

            }
            catch (Exception ex)
            {
                cn.Close();
                var msg = "Ha ocurrido un error en el proceso: " + " | " + (ex.InnerException == null ? string.Empty : ex.InnerException.Message);
                var error = $"{ GetType().Namespace }.{ GetType().Name}.{ MethodBase.GetCurrentMethod().Name} : " + msg;
                obj.Add(error);
                return clList;
            }
        }

        public bool InsertSucursales(ESucursales sucursales)
        {
            try
            {
                cn.Open();
                cmd = new SqlCommand("[TestQuala].SP_INSERT_UPDATE_DATA_SUCURSAL", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Codigo", sucursales.Codigo);
                cmd.Parameters.AddWithValue("@Descripcion", sucursales.Descripcion);
                cmd.Parameters.AddWithValue("@Direccion", sucursales.Direccion);
                cmd.Parameters.AddWithValue("@Identificacion", sucursales.Identificacion);
                cmd.Parameters.AddWithValue("@Moneda", sucursales.Moneda);
                cmd.Parameters.AddWithValue("@Estado", sucursales.Estado);
                cmd.ExecuteNonQuery();
                cn.Close();
                return true;
            }
            catch (Exception ex)
            {
                cn.Close();
                var msg = "Ha ocurrido un error en el proces: " + " | " + (ex.InnerException == null ? string.Empty : ex.InnerException.Message);
                var error = $"{ GetType().Namespace }.{ GetType().Name}.{ MethodBase.GetCurrentMethod().Name} : " + msg;
                obj.Add(error);
                return false;
            }

        }

        public bool DeleteSucursales(ESucursales sucursales)
        {
            try
            {
                cn.Open();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"DELETE FROM [TestQuala].Sucursales where Codigo = @Codigo");
                cmd = new SqlCommand(sb.ToString(), cn);
                cmd.Parameters.AddWithValue("@Codigo", sucursales.Codigo);
                SqlDataReader rd = cmd.ExecuteReader();

                cn.Close();
                return true;
            }
            catch (Exception ex)
            {
                cn.Close();
                var msg = "Ha ocurrido un error en el proces: " + " | " + (ex.InnerException == null ? string.Empty : ex.InnerException.Message);
                var error = $"{ GetType().Namespace }.{ GetType().Name}.{ MethodBase.GetCurrentMethod().Name} : " + msg;
                obj.Add(error);
                return false;
            }

        }
    }
}
