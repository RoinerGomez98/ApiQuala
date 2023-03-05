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
    public class Monedas : IMonedas
    {
        SqlConnection cn = new SqlConnection(ApiDBContext.ConfiguracionApiDDB.cadenaConexion);
        SqlCommand cmd = new SqlCommand();
        ClassLog obj = new ClassLog();
        public List<EMonedas> GetMonedas(EMonedas eMonedas, bool isSucursal)
        {
            List<EMonedas> mList = new List<EMonedas>();
            EMonedas mcl = new EMonedas();
            try
            {
                string where = string.IsNullOrEmpty(eMonedas.Codigo) ? "" : $" and Codigo = @Codigo";
                string estado = isSucursal ? "Estado in(1)" : $" Estado in(0,1)";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"SELECT * FROM [TestQuala].Monedas where {estado} {where}");
                cmd = new SqlCommand(sb.ToString(), cn);
                if (!string.IsNullOrEmpty(where))
                {
                    cmd.Parameters.AddWithValue("@Codigo", eMonedas.Codigo);
                }
                cn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            mcl = new EMonedas
                            {
                                Codigo = rd["Codigo"].ToString(),
                                Descripcion = rd["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(rd["Estado"])
                            };
                            mList.Add(mcl);
                        }
                    }
                    else
                    {
                        cn.Close();
                        return mList;

                    }
                }
                cn.Close();
                return mList;

            }
            catch (Exception ex)
            {
                cn.Close();
                var msg = "Ha ocurrido un error en el proceso: " + " | " + (ex.InnerException == null ? string.Empty : ex.InnerException.Message);
                var error = $"{ GetType().Namespace }.{ GetType().Name}.{ MethodBase.GetCurrentMethod().Name} : " + msg;
                obj.Add(error);
                return mList;
            }
        }

        public bool InsertMonedas(EMonedas eMonedas)
        {
            try
            {
                cn.Open();
                cmd = new SqlCommand("[TestQuala].SP_INSERT_UPDATE_DATA_MONEDAS", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Codigo", eMonedas.Codigo);
                cmd.Parameters.AddWithValue("@Descripcion", eMonedas.Descripcion);
                cmd.Parameters.AddWithValue("@Estado", eMonedas.Estado);
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
    }
}
