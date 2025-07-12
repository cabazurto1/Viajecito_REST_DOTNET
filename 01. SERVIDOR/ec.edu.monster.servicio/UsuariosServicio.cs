using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// UsuariosServicio.cs

using System.Configuration;

using System.Data.SqlClient;
using ec.edu.monster.modelo;

namespace ec.edu.monster.servicio
{
    public class UsuariosServicio : IUsuariosServicio
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["AerolineasDB"].ConnectionString;

        public void Crear(Usuarios entity)
        {
            string sql = @"INSERT INTO usuarios 
                         (nombre, username, password, telefono, cedula, correo) 
                         VALUES (@nombre, @username, @password, @telefono, @cedula, @correo)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@nombre", entity.nombre);
                cmd.Parameters.AddWithValue("@username", entity.username);
                cmd.Parameters.AddWithValue("@password", entity.password);
                cmd.Parameters.AddWithValue("@telefono", entity.telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@cedula", entity.cedula);
                cmd.Parameters.AddWithValue("@correo", entity.correo);
                cmd.ExecuteNonQuery();
            }
        }

        public void Editar(Usuarios entity)
        {
            string sql = @"UPDATE usuarios 
                         SET nombre = @nombre, username = @username, password = @password, 
                             telefono = @telefono, cedula = @cedula, correo = @correo 
                         WHERE id_usuario = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", entity.idUsuario);
                cmd.Parameters.AddWithValue("@nombre", entity.nombre);
                cmd.Parameters.AddWithValue("@username", entity.username);
                cmd.Parameters.AddWithValue("@password", entity.password);
                cmd.Parameters.AddWithValue("@telefono", entity.telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@cedula", entity.cedula);
                cmd.Parameters.AddWithValue("@correo", entity.correo);
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            string sql = "DELETE FROM usuarios WHERE id_usuario = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Usuarios Buscar(int id)
        {
            string sql = "SELECT * FROM usuarios WHERE id_usuario = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return MapearUsuario(dr);
                }
                return null;
            }
        }

        public List<Usuarios> ListarTodos()
        {
            var lista = new List<Usuarios>();
            string sql = "SELECT * FROM usuarios";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(MapearUsuario(dr));
                }
            }
            return lista;
        }

        public List<Usuarios> ListarRango(int desde, int hasta)
        {
            var lista = new List<Usuarios>();
            string sql = @"SELECT * FROM usuarios 
                         ORDER BY id_usuario 
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
                    lista.Add(MapearUsuario(dr));
                }
            }
            return lista;
        }

        public Usuarios Login(string username, string password)
        {
            string sql = "SELECT * FROM usuarios WHERE username = @username AND password = @password";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return MapearUsuario(dr);
                }
                return null;
            }
        }

        public int Contar()
        {
            string sql = "SELECT COUNT(*) FROM usuarios";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private Usuarios MapearUsuario(SqlDataReader dr)
        {
            return new Usuarios
            {
                idUsuario = Convert.ToInt32(dr["id_usuario"]),
                nombre = dr["nombre"].ToString(),
                username = dr["username"].ToString(),
                password = dr["password"].ToString(),
                telefono = dr["telefono"]?.ToString(),
                cedula = dr["cedula"].ToString(),
                correo = dr["correo"].ToString()
            };
        }
    }
}