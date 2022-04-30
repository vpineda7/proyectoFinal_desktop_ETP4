using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Permiso
    {

        public List<Permiso> Listar(int idusuario)
        {
            List<Permiso> lista = new List<Permiso>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {

                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.id_rol,p.NombreMenu from PERMISO p");
                    query.AppendLine("inner join ROL r on r.id_rol = p.id_rol");
                    query.AppendLine("inner join USUARIO u on u.id_rol = r.id_rol");
                    query.AppendLine("where u.IdUsuario = @idusuario");

                   
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idusuario", idusuario);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            lista.Add(new Permiso()
                            {
                                oRol = new Rol() { id_rol = Convert.ToInt32(dr["id_rol"]) } ,
                                NombreMenu = dr["NombreMenu"].ToString(),
                            });

                        }

                    }


                }
                catch (Exception )
                {

                    lista = new List<Permiso>();
                }
            }

            return lista;

        }

    }
}
