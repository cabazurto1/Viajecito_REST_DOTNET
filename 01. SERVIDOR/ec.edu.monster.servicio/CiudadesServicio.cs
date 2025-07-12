using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// CiudadesServicio.cs

using System.Configuration;

using System.Data.SqlClient;
using ec.edu.monster.modelo;

namespace ec.edu.monster.servicio
{
    public class CiudadesServicio : ICiudadesServicio
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["AerolineasDB"].ConnectionString;

        public void Crear(Ciudades entity)
        {
            string sql = @"INSERT INTO ciudades (codigo_ciudad, nombre_ciudad) 
                         VALUES (@codigoCiudad, @nombreCiudad)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@codigoCiudad", entity.codigoCiudad);
                cmd.Parameters.AddWithValue("@nombreCiudad", entity.nombreCiudad);
                cmd.ExecuteNonQuery();
            }
        }

        public void Editar(Ciudades entity)
        {
            string sql = @"UPDATE ciudades 
                         SET codigo_ciudad = @codigoCiudad, nombre_ciudad = @nombreCiudad 
                         WHERE id_ciudad = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", entity.idCiudad);
                cmd.Parameters.AddWithValue("@codigoCiudad", entity.codigoCiudad);
                cmd.Parameters.AddWithValue("@nombreCiudad", entity.nombreCiudad);
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            string sql = "DELETE FROM ciudades WHERE id_ciudad = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Ciudades Buscar(int id)
        {
            string sql = "SELECT * FROM ciudades WHERE id_ciudad = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return MapearCiudad(dr);
                }
                return null;
            }
        }

        public List<Ciudades> ListarTodos()
        {
            var lista = new List<Ciudades>();
            string sql = "SELECT * FROM ciudades";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(MapearCiudad(dr));
                }
            }
            return lista;
        }

        public List<Ciudades> ListarRango(int desde, int hasta)
        {
            var lista = new List<Ciudades>();
            string sql = @"SELECT * FROM ciudades 
                         ORDER BY id_ciudad 
                         OFFSET @desde ROWS FETCH NEXT @cantidad ROWS ONLY";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@desde", desde);
                cmd.Parameters.AddWithValue("@cantidad", hasta - desde + 1);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(MapearCiudad(dr));
                }
            }
            return lista;
        }

        public int Contar()
        {
            string sql = "SELECT COUNT(*) FROM ciudades";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private Ciudades MapearCiudad(SqlDataReader dr)
        {
            return new Ciudades
            {
                idCiudad = Convert.ToInt32(dr["id_ciudad"]),
                codigoCiudad = dr["codigo_ciudad"].ToString(),
                nombreCiudad = dr["nombre_ciudad"].ToString()
            };
        }
    }
}
