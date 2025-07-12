using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// BoletosServicio.cs

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ec.edu.monster.modelo;

namespace ec.edu.monster.servicio
{
    public class BoletosServicio : IBoletosServicio
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["AerolineasDB"].ConnectionString;

        public void Crear(Boletos entity)
        {
            string sql = @"INSERT INTO boletos 
                         (numero_boleto, id_vuelo, id_usuario, fecha_compra, precio_compra, id_factura) 
                         VALUES (@numeroBoleto, @idVuelo, @idUsuario, @fechaCompra, @precioCompra, @idFactura)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@numeroBoleto", entity.numeroBoleto);
                cmd.Parameters.AddWithValue("@idVuelo", entity.idVuelo.idVuelo);
                cmd.Parameters.AddWithValue("@idUsuario", entity.idUsuario.idUsuario);
                cmd.Parameters.AddWithValue("@fechaCompra", entity.fechaCompra ?? DateTime.Now);
                cmd.Parameters.AddWithValue("@precioCompra", entity.precioCompra);
                cmd.Parameters.AddWithValue("@idFactura", entity.idFactura?.idFactura ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void Editar(Boletos entity)
        {
            string sql = @"UPDATE boletos 
                         SET numero_boleto = @numeroBoleto, id_vuelo = @idVuelo, 
                             id_usuario = @idUsuario, fecha_compra = @fechaCompra, 
                             precio_compra = @precioCompra, id_factura = @idFactura 
                         WHERE id_boleto = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", entity.idBoleto);
                cmd.Parameters.AddWithValue("@numeroBoleto", entity.numeroBoleto);
                cmd.Parameters.AddWithValue("@idVuelo", entity.idVuelo.idVuelo);
                cmd.Parameters.AddWithValue("@idUsuario", entity.idUsuario.idUsuario);
                cmd.Parameters.AddWithValue("@fechaCompra", entity.fechaCompra ?? DateTime.Now);
                cmd.Parameters.AddWithValue("@precioCompra", entity.precioCompra);
                cmd.Parameters.AddWithValue("@idFactura", entity.idFactura?.idFactura ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            string sql = "DELETE FROM boletos WHERE id_boleto = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Boletos Buscar(int id)
        {
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
                         WHERE b.id_boleto = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return MapearBoletoCompleto(dr);
                }
                return null;
            }
        }

        public List<Boletos> ListarTodos()
        {
            var lista = new List<Boletos>();
            string sql = @"SELECT b.*, 
                         u.id_usuario AS uid, u.nombre, u.username, u.password, u.telefono, u.cedula, u.correo,
                         v.id_vuelo AS vid, v.codigo_vuelo, v.valor, v.hora_salida, v.capacidad, v.disponibles,
                         co.id_ciudad AS coid, co.codigo_ciudad AS co_codigo, co.nombre_ciudad AS co_nombre,
                         cd.id_ciudad AS cdid, cd.codigo_ciudad AS cd_codigo, cd.nombre_ciudad AS cd_nombre
                         FROM boletos b
                         INNER JOIN usuarios u ON b.id_usuario = u.id_usuario
                         INNER JOIN vuelos v ON b.id_vuelo = v.id_vuelo
                         INNER JOIN ciudades co ON v.id_ciudad_origen = co.id_ciudad
                         INNER JOIN ciudades cd ON v.id_ciudad_destino = cd.id_ciudad";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(MapearBoletoCompleto(dr));
                }
            }
            return lista;
        }

        public List<Boletos> ListarRango(int desde, int hasta)
        {
            var lista = new List<Boletos>();
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
                         ORDER BY b.id_boleto 
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
                    lista.Add(MapearBoletoCompleto(dr));
                }
            }
            return lista;
        }

        public List<Boletos> ObtenerBoletosPorUsuario(int idUsuario)
        {
            var lista = new List<Boletos>();
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
                         WHERE b.id_usuario = @idUsuario";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(MapearBoletoCompleto(dr));
                }
            }
            return lista;
        }
        public string ComprarBoletos(CompraBoletoRequest request)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlTransaction tx = cn.BeginTransaction();
                try
                {
                    double totalSinIVA = 0.0;

                    // Validar vuelos y calcular total
                    foreach (var vc in request.vuelos)
                    {
                        SqlCommand cmdVuelo = new SqlCommand(
                            "SELECT valor, disponibles FROM vuelos WHERE id_vuelo = @id", cn, tx);
                        cmdVuelo.Parameters.AddWithValue("@id", vc.idVuelo);
                        SqlDataReader drVuelo = cmdVuelo.ExecuteReader();

                        if (!drVuelo.Read())
                        {
                            drVuelo.Close();
                            throw new Exception($"Vuelo {vc.idVuelo} no encontrado");
                        }

                        decimal valor = Convert.ToDecimal(drVuelo["valor"]);
                        int disponibles = Convert.ToInt32(drVuelo["disponibles"]);
                        drVuelo.Close();

                        if (disponibles < vc.cantidad)
                        {
                            throw new Exception($"Vuelo {vc.idVuelo} sin cupos suficientes");
                        }

                        totalSinIVA += (double)valor * vc.cantidad;
                    }

                    double totalConIVA = totalSinIVA * 1.15;

                    // Validar usuario
                    SqlCommand cmdUsuario = new SqlCommand(
                        "SELECT COUNT(*) FROM usuarios WHERE id_usuario = @id", cn, tx);
                    cmdUsuario.Parameters.AddWithValue("@id", request.idUsuario);
                    if (Convert.ToInt32(cmdUsuario.ExecuteScalar()) == 0)
                    {
                        throw new Exception("Usuario no encontrado");
                    }

                    // Generar número de factura
                    SqlCommand cmdMaxFactura = new SqlCommand(
                        "SELECT COALESCE(MAX(id_factura), 0) FROM facturas", cn, tx);
                    int maxFacturaId = Convert.ToInt32(cmdMaxFactura.ExecuteScalar());
                    string numeroFactura = $"FAC-{(maxFacturaId + 1):D9}";

                    // Crear factura
                    SqlCommand cmdFactura = new SqlCommand(@"
                INSERT INTO facturas (numero_factura, id_usuario, precio_sin_iva, precio_con_iva, fecha_factura)
                VALUES (@numeroFactura, @idUsuario, @precioSinIva, @precioConIva, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() as int)", cn, tx);
                    cmdFactura.Parameters.AddWithValue("@numeroFactura", numeroFactura);
                    cmdFactura.Parameters.AddWithValue("@idUsuario", request.idUsuario);
                    cmdFactura.Parameters.AddWithValue("@precioSinIva", totalSinIVA);
                    cmdFactura.Parameters.AddWithValue("@precioConIva", totalConIVA);
                    int idFactura = (int)cmdFactura.ExecuteScalar();

                    // Si es a crédito, generar tabla amortización
                    if (request.esCredito)
                    {
                        var tablaAmortizacion = GenerarTablaAmortizacion(
                            totalConIVA, request.tasaInteresAnual, request.numeroCuotas);

                        foreach (var amort in tablaAmortizacion)
                        {
                            SqlCommand cmdAmort = new SqlCommand(@"
                        INSERT INTO amortizacion_boletos 
                        (id_factura, numero_cuota, valor_cuota, interes_pagado, capital_pagado, saldo)
                        VALUES (@idFactura, @numeroCuota, @valorCuota, @interesPagado, @capitalPagado, @saldo)", cn, tx);
                            cmdAmort.Parameters.AddWithValue("@idFactura", idFactura);
                            cmdAmort.Parameters.AddWithValue("@numeroCuota", amort.numeroCuota);
                            cmdAmort.Parameters.AddWithValue("@valorCuota", amort.valorCuota);
                            cmdAmort.Parameters.AddWithValue("@interesPagado", amort.interesPagado);
                            cmdAmort.Parameters.AddWithValue("@capitalPagado", amort.capitalPagado);
                            cmdAmort.Parameters.AddWithValue("@saldo", amort.saldo);
                            cmdAmort.ExecuteNonQuery();
                        }
                    }

                    // Insertar boletos y actualizar vuelos
                    foreach (var vc in request.vuelos)
                    {
                        // Obtener información del vuelo
                        SqlCommand cmdVuelo = new SqlCommand(
                            "SELECT valor FROM vuelos WHERE id_vuelo = @id", cn, tx);
                        cmdVuelo.Parameters.AddWithValue("@id", vc.idVuelo);
                        decimal precioVuelo = Convert.ToDecimal(cmdVuelo.ExecuteScalar());

                        // Insertar boletos
                        for (int i = 0; i < vc.cantidad; i++)
                        {
                            string numeroBoleto = Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
                            SqlCommand cmdBoleto = new SqlCommand(@"
                        INSERT INTO boletos 
                        (numero_boleto, id_vuelo, id_usuario, fecha_compra, precio_compra, id_factura)
                        VALUES (@numeroBoleto, @idVuelo, @idUsuario, GETDATE(), @precioCompra, @idFactura)", cn, tx);
                            cmdBoleto.Parameters.AddWithValue("@numeroBoleto", numeroBoleto);
                            cmdBoleto.Parameters.AddWithValue("@idVuelo", vc.idVuelo);
                            cmdBoleto.Parameters.AddWithValue("@idUsuario", request.idUsuario);
                            cmdBoleto.Parameters.AddWithValue("@precioCompra", precioVuelo);
                            cmdBoleto.Parameters.AddWithValue("@idFactura", idFactura);
                            cmdBoleto.ExecuteNonQuery();
                        }

                        // Actualizar disponibles
                        SqlCommand cmdUpdate = new SqlCommand(
                            "UPDATE vuelos SET disponibles = disponibles - @cantidad WHERE id_vuelo = @id", cn, tx);
                        cmdUpdate.Parameters.AddWithValue("@cantidad", vc.cantidad);
                        cmdUpdate.Parameters.AddWithValue("@id", vc.idVuelo);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    tx.Commit();
                    return "Compra realizada con éxito";
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw new Exception($"Error en la compra: {ex.Message}");
                }
            }
        }
        public int Contar()
        {
            string sql = "SELECT COUNT(*) FROM boletos";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private List<Amortizacion> GenerarTablaAmortizacion(double monto, double tasaAnual, int cuotas)
        {
            var lista = new List<Amortizacion>();
            decimal saldo = (decimal)monto;
            double tasaMensual = tasaAnual / 12 / 100;

            decimal cuota = saldo * (decimal)tasaMensual /
                           (decimal)(1 - Math.Pow(1 + tasaMensual, -cuotas));

            for (int i = 1; i <= cuotas; i++)
            {
                decimal interes = saldo * (decimal)tasaMensual;
                decimal capital = cuota - interes;
                saldo = saldo - capital;

                lista.Add(new Amortizacion
                {
                    numeroCuota = i,
                    valorCuota = Math.Round(cuota, 2),
                    interesPagado = Math.Round(interes, 2),
                    capitalPagado = Math.Round(capital, 2),
                    saldo = Math.Max(0, Math.Round(saldo, 2))
                });
            }

            return lista;
        }

        private Boletos MapearBoletoCompleto(SqlDataReader dr)
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

            // Si tiene factura
            if (dr["id_factura"] != DBNull.Value)
            {
                // Aquí podrías cargar la factura si es necesario
                // Por ahora dejamos null para evitar referencias circulares
            }

            return boleto;
        }
    }
}
