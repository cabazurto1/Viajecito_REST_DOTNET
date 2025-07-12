using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using ec.edu.monster.modelo;

namespace ec.edu.monster.servicio
{
    public class FacturasServicio : IFacturasServicio
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["AerolineasDB"].ConnectionString;

        public void Crear(Facturas entity)
        {
            string sql = @"INSERT INTO facturas 
                         (numero_factura, id_usuario, precio_sin_iva, precio_con_iva, fecha_factura) 
                         VALUES (@numeroFactura, @idUsuario, @precioSinIva, @precioConIva, @fechaFactura)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@numeroFactura", entity.numeroFactura);
                cmd.Parameters.AddWithValue("@idUsuario", entity.idUsuario.idUsuario);
                cmd.Parameters.AddWithValue("@precioSinIva", entity.precioSinIva);
                cmd.Parameters.AddWithValue("@precioConIva", entity.precioConIva);
                cmd.Parameters.AddWithValue("@fechaFactura", entity.fechaFactura ?? DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }

        public void Editar(Facturas entity)
        {
            string sql = @"UPDATE facturas 
                         SET numero_factura = @numeroFactura, id_usuario = @idUsuario, 
                             precio_sin_iva = @precioSinIva, precio_con_iva = @precioConIva, 
                             fecha_factura = @fechaFactura 
                         WHERE id_factura = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", entity.idFactura);
                cmd.Parameters.AddWithValue("@numeroFactura", entity.numeroFactura);
                cmd.Parameters.AddWithValue("@idUsuario", entity.idUsuario.idUsuario);
                cmd.Parameters.AddWithValue("@precioSinIva", entity.precioSinIva);
                cmd.Parameters.AddWithValue("@precioConIva", entity.precioConIva);
                cmd.Parameters.AddWithValue("@fechaFactura", entity.fechaFactura ?? DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            string sql = "DELETE FROM facturas WHERE id_factura = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Facturas Buscar(int id)
        {
            string sql = @"SELECT f.*, 
                         u.id_usuario AS uid, u.nombre, u.username, u.password, u.telefono, u.cedula, u.correo
                         FROM facturas f
                         INNER JOIN usuarios u ON f.id_usuario = u.id_usuario
                         WHERE f.id_factura = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    var factura = MapearFacturaCompleta(dr);
                    dr.Close();

                    // Cargar boletos relacionados
                    factura.boletosCollection = CargarBoletosDeFactura(cn, id);

                    return factura;
                }
                return null;
            }
        }

        public List<Facturas> ListarTodos()
        {
            var lista = new List<Facturas>();
            string sql = @"SELECT f.*, 
                         u.id_usuario AS uid, u.nombre, u.username, u.password, u.telefono, u.cedula, u.correo
                         FROM facturas f
                         INNER JOIN usuarios u ON f.id_usuario = u.id_usuario";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(MapearFacturaCompleta(dr));
                }
            }
            return lista;
        }

        public List<Facturas> ListarRango(int desde, int hasta)
        {
            var lista = new List<Facturas>();
            string sql = @"SELECT f.*, 
                         u.id_usuario AS uid, u.nombre, u.username, u.password, u.telefono, u.cedula, u.correo
                         FROM facturas f
                         INNER JOIN usuarios u ON f.id_usuario = u.id_usuario
                         ORDER BY f.id_factura 
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
                    lista.Add(MapearFacturaCompleta(dr));
                }
            }
            return lista;
        }

        public List<Facturas> BuscarPorUsuario(int idUsuario)
        {
            var lista = new List<Facturas>();
            string sql = @"SELECT f.*, 
                         u.id_usuario AS uid, u.nombre, u.username, u.password, u.telefono, u.cedula, u.correo
                         FROM facturas f
                         INNER JOIN usuarios u ON f.id_usuario = u.id_usuario
                         WHERE f.id_usuario = @idUsuario
                         ORDER BY f.fecha_factura DESC";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(MapearFacturaCompleta(dr));
                }
            }
            return lista;
        }

        public int Contar()
        {
            string sql = "SELECT COUNT(*) FROM facturas";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private List<Boletos> CargarBoletosDeFactura(SqlConnection cn, int idFactura)
        {
            var boletos = new List<Boletos>();

            string sql = @"SELECT b.*, 
                         u.id_usuario AS uid, u.nombre, u.username, u.password, u.telefono, u.cedula, u.correo,
                         v.id_vuelo AS vid, v.codigo_vuelo, v.valor, v.hora_salida, v.capacidad, v.disponibles,
                         co.id_ciudad AS coid, co.codigo_ciudad AS co_codigo, co.nombre_ciudad AS co_nombre,
                         cd.id_ciudad AS cdid, cd.codigo_ciudad AS cd_codigo, cd.nombre_ciudad AS cd_nombre
                         FROM boletos b
                         INNER JOIN usuarios u ON b.id_usuario = u.id_usuario
                         INNER JOIN vuelos v ON b.id_vuelo = v.id_vuelo
                         INNER JOIN ciudades co ON v.id_ciudad_origen = co.id_ciudad
                         INNER JOIN ciudades cd ON v.id_ciudad_destino = cd.id_ciudad
                         WHERE b.id_factura = @idFactura";

            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@idFactura", idFactura);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var boleto = new Boletos
                {
                    idBoleto = Convert.ToInt32(dr["id_boleto"]),
                    numeroBoleto = dr["numero_boleto"].ToString(),
                    fechaCompra = dr["fecha_compra"] != DBNull.Value ? Convert.ToDateTime(dr["fecha_compra"]) : (DateTime?)null,
                    precioCompra = Convert.ToDecimal(dr["precio_compra"]),
                    idUsuario = new Usuarios
                    {
                        idUsuario = Convert.ToInt32(dr["uid"]),
                        nombre = dr["nombre"].ToString(),
                        username = dr["username"].ToString(),
                        password = dr["password"].ToString(),
                        telefono = dr["telefono"]?.ToString(),
                        cedula = dr["cedula"].ToString(),
                        correo = dr["correo"].ToString()
                    },
                    idVuelo = new Vuelos
                    {
                        idVuelo = Convert.ToInt32(dr["vid"]),
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
                    }
                };

                boletos.Add(boleto);
            }
            dr.Close();

            return boletos;
        }

        private Facturas MapearFacturaCompleta(SqlDataReader dr)
        {
            return new Facturas
            {
                idFactura = Convert.ToInt32(dr["id_factura"]),
                numeroFactura = dr["numero_factura"].ToString(),
                precioSinIva = Convert.ToDecimal(dr["precio_sin_iva"]),
                precioConIva = Convert.ToDecimal(dr["precio_con_iva"]),
                fechaFactura = dr["fecha_factura"] != DBNull.Value ? Convert.ToDateTime(dr["fecha_factura"]) : (DateTime?)null,
                idUsuario = new Usuarios
                {
                    idUsuario = Convert.ToInt32(dr["uid"]),
                    nombre = dr["nombre"].ToString(),
                    username = dr["username"].ToString(),
                    password = dr["password"].ToString(),
                    telefono = dr["telefono"]?.ToString(),
                    cedula = dr["cedula"].ToString(),
                    correo = dr["correo"].ToString()
                }
            };
        }
    }
}