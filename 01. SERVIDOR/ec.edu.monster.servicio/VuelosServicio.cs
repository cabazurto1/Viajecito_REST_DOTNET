using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// VuelosServicio.cs
using System.Configuration;
using System.Data.SqlClient;
using ec.edu.monster.modelo;

namespace ec.edu.monster.servicio
{
    public class VuelosServicio : IVuelosServicio
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["AerolineasDB"].ConnectionString;

        public void Crear(Vuelos entity)
        {
            string sql = @"INSERT INTO vuelos 
                         (codigo_vuelo, id_ciudad_origen, id_ciudad_destino, valor, hora_salida, capacidad, disponibles) 
                         VALUES (@codigoVuelo, @idCiudadOrigen, @idCiudadDestino, @valor, @horaSalida, @capacidad, @disponibles)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@codigoVuelo", entity.codigoVuelo);
                cmd.Parameters.AddWithValue("@idCiudadOrigen", entity.idCiudadOrigen.idCiudad);
                cmd.Parameters.AddWithValue("@idCiudadDestino", entity.idCiudadDestino.idCiudad);
                cmd.Parameters.AddWithValue("@valor", entity.valor);
                cmd.Parameters.AddWithValue("@horaSalida", entity.horaSalida);
                cmd.Parameters.AddWithValue("@capacidad", entity.capacidad);
                cmd.Parameters.AddWithValue("@disponibles", entity.disponibles);
                cmd.ExecuteNonQuery();
            }
        }

        public void Editar(Vuelos entity)
        {
            string sql = @"UPDATE vuelos 
                         SET codigo_vuelo = @codigoVuelo, id_ciudad_origen = @idCiudadOrigen, 
                             id_ciudad_destino = @idCiudadDestino, valor = @valor, 
                             hora_salida = @horaSalida, capacidad = @capacidad, disponibles = @disponibles 
                         WHERE id_vuelo = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", entity.idVuelo);
                cmd.Parameters.AddWithValue("@codigoVuelo", entity.codigoVuelo);
                cmd.Parameters.AddWithValue("@idCiudadOrigen", entity.idCiudadOrigen.idCiudad);
                cmd.Parameters.AddWithValue("@idCiudadDestino", entity.idCiudadDestino.idCiudad);
                cmd.Parameters.AddWithValue("@valor", entity.valor);
                cmd.Parameters.AddWithValue("@horaSalida", entity.horaSalida);
                cmd.Parameters.AddWithValue("@capacidad", entity.capacidad);
                cmd.Parameters.AddWithValue("@disponibles", entity.disponibles);
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            string sql = "DELETE FROM vuelos WHERE id_vuelo = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Vuelos Buscar(int id)
        {
            string sql = @"SELECT v.*, 
                         co.id_ciudad AS coid, co.codigo_ciudad AS co_codigo, co.nombre_ciudad AS co_nombre,
                         cd.id_ciudad AS cdid, cd.codigo_ciudad AS cd_codigo, cd.nombre_ciudad AS cd_nombre
                         FROM vuelos v
                         INNER JOIN ciudades co ON v.id_ciudad_origen = co.id_ciudad
                         INNER JOIN ciudades cd ON v.id_ciudad_destino = cd.id_ciudad
                         WHERE v.id_vuelo = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return MapearVueloCompleto(dr);
                }
                return null;
            }
        }

        public List<Vuelos> ListarTodos()
        {
            var lista = new List<Vuelos>();
            string sql = @"SELECT v.*, 
                         co.id_ciudad AS coid, co.codigo_ciudad AS co_codigo, co.nombre_ciudad AS co_nombre,
                         cd.id_ciudad AS cdid, cd.codigo_ciudad AS cd_codigo, cd.nombre_ciudad AS cd_nombre
                         FROM vuelos v
                         INNER JOIN ciudades co ON v.id_ciudad_origen = co.id_ciudad
                         INNER JOIN ciudades cd ON v.id_ciudad_destino = cd.id_ciudad";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(MapearVueloCompleto(dr));
                }
            }
            return lista;
        }

        public List<Vuelos> ListarRango(int desde, int hasta)
        {
            var lista = new List<Vuelos>();
            string sql = @"SELECT v.*, 
                         co.id_ciudad AS coid, co.codigo_ciudad AS co_codigo, co.nombre_ciudad AS co_nombre,
                         cd.id_ciudad AS cdid, cd.codigo_ciudad AS cd_codigo, cd.nombre_ciudad AS cd_nombre
                         FROM vuelos v
                         INNER JOIN ciudades co ON v.id_ciudad_origen = co.id_ciudad
                         INNER JOIN ciudades cd ON v.id_ciudad_destino = cd.id_ciudad
                         ORDER BY v.id_vuelo 
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
                    lista.Add(MapearVueloCompleto(dr));
                }
            }
            return lista;
        }

        public List<Vuelos> BuscarPorCiudadesOrdenadoPorValorDesc(string ciudadOrigen, string ciudadDestino, DateTime fecha)
        {
            var lista = new List<Vuelos>();
            DateTime inicio = fecha.Date;
            DateTime fin = fecha.Date.AddDays(1).AddSeconds(-1);

            string sql = @"SELECT v.*, 
                         co.id_ciudad AS coid, co.codigo_ciudad AS co_codigo, co.nombre_ciudad AS co_nombre,
                         cd.id_ciudad AS cdid, cd.codigo_ciudad AS cd_codigo, cd.nombre_ciudad AS cd_nombre
                         FROM vuelos v
                         INNER JOIN ciudades co ON v.id_ciudad_origen = co.id_ciudad
                         INNER JOIN ciudades cd ON v.id_ciudad_destino = cd.id_ciudad
                         WHERE co.codigo_ciudad = @origen AND cd.codigo_ciudad = @destino 
                         AND v.hora_salida BETWEEN @inicio AND @fin
                         ORDER BY v.valor DESC";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@origen", ciudadOrigen);
                cmd.Parameters.AddWithValue("@destino", ciudadDestino);
                cmd.Parameters.AddWithValue("@inicio", inicio);
                cmd.Parameters.AddWithValue("@fin", fin);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(MapearVueloCompleto(dr));
                }
            }
            return lista;
        }

        public int Contar()
        {
            string sql = "SELECT COUNT(*) FROM vuelos";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private Vuelos MapearVueloCompleto(SqlDataReader dr)
        {
            return new Vuelos
            {
                idVuelo = Convert.ToInt32(dr["id_vuelo"]),
                codigoVuelo = dr["codigo_vuelo"].ToString(),
                valor = Convert.ToDecimal(dr["valor"]),
                horaSalida = Convert.ToDateTime(dr["hora_salida"]),
                capacidad = Convert.ToInt32(dr["capacidad"]),
                disponibles = Convert.ToInt32(dr["disponibles"]),
                idCiudadOrigen = new Ciudades
                {
                    idCiudad = Convert.ToInt32(dr["coid"]),
                    codigoCiudad = dr["co_codigo"].ToString(),
                    nombreCiudad = dr["co_nombre"].ToString()
                },
                idCiudadDestino = new Ciudades
                {
                    idCiudad = Convert.ToInt32(dr["cdid"]),
                    codigoCiudad = dr["cd_codigo"].ToString(),
                    nombreCiudad = dr["cd_nombre"].ToString()
                }
            };
        }
    }
}