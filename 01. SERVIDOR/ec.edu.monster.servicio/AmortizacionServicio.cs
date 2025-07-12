using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// AmortizacionServicio.cs

using System.Configuration;
using System.Data.SqlClient;
using ec.edu.monster.modelo;

namespace ec.edu.monster.servicio
{
    public class AmortizacionServicio : IAmortizacionServicio
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["AerolineasDB"].ConnectionString;

        public void Crear(Amortizacion entity)
        {
            string sql = @"INSERT INTO amortizacion_boletos 
                         (id_factura, numero_cuota, valor_cuota, interes_pagado, capital_pagado, saldo) 
                         VALUES (@idFactura, @numeroCuota, @valorCuota, @interesPagado, @capitalPagado, @saldo)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@idFactura", entity.idFactura?.idFactura ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@numeroCuota", entity.numeroCuota);
                cmd.Parameters.AddWithValue("@valorCuota", entity.valorCuota ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@interesPagado", entity.interesPagado ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@capitalPagado", entity.capitalPagado ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@saldo", entity.saldo ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void Editar(Amortizacion entity)
        {
            string sql = @"UPDATE amortizacion_boletos 
                         SET id_factura = @idFactura, numero_cuota = @numeroCuota, 
                             valor_cuota = @valorCuota, interes_pagado = @interesPagado, 
                             capital_pagado = @capitalPagado, saldo = @saldo 
                         WHERE id_amortizacion = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", entity.idAmortizacion);
                cmd.Parameters.AddWithValue("@idFactura", entity.idFactura?.idFactura ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@numeroCuota", entity.numeroCuota);
                cmd.Parameters.AddWithValue("@valorCuota", entity.valorCuota ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@interesPagado", entity.interesPagado ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@capitalPagado", entity.capitalPagado ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@saldo", entity.saldo ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            string sql = "DELETE FROM amortizacion_boletos WHERE id_amortizacion = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Amortizacion Buscar(int id)
        {
            string sql = "SELECT * FROM amortizacion_boletos WHERE id_amortizacion = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return MapearAmortizacion(dr);
                }
                return null;
            }
        }

        public List<Amortizacion> ListarTodos()
        {
            var lista = new List<Amortizacion>();
            string sql = "SELECT * FROM amortizacion_boletos";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(MapearAmortizacion(dr));
                }
            }
            return lista;
        }

        public List<Amortizacion> ListarRango(int desde, int hasta)
        {
            var lista = new List<Amortizacion>();
            string sql = @"SELECT * FROM amortizacion_boletos 
                         ORDER BY id_amortizacion 
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
                    lista.Add(MapearAmortizacion(dr));
                }
            }
            return lista;
        }

        public List<Amortizacion> ObtenerPorFactura(int idFactura)
        {
            var lista = new List<Amortizacion>();
            string sql = "SELECT * FROM amortizacion_boletos WHERE id_factura = @idFactura ORDER BY numero_cuota";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@idFactura", idFactura);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(MapearAmortizacion(dr));
                }
            }
            return lista;
        }

        public int Contar()
        {
            string sql = "SELECT COUNT(*) FROM amortizacion_boletos";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private Amortizacion MapearAmortizacion(SqlDataReader dr)
        {
            return new Amortizacion
            {
                idAmortizacion = Convert.ToInt32(dr["id_amortizacion"]),
                numeroCuota = Convert.ToInt32(dr["numero_cuota"]),
                valorCuota = dr["valor_cuota"] != DBNull.Value ? Convert.ToDecimal(dr["valor_cuota"]) : (decimal?)null,
                interesPagado = dr["interes_pagado"] != DBNull.Value ? Convert.ToDecimal(dr["interes_pagado"]) : (decimal?)null,
                capitalPagado = dr["capital_pagado"] != DBNull.Value ? Convert.ToDecimal(dr["capital_pagado"]) : (decimal?)null,
                saldo = dr["saldo"] != DBNull.Value ? Convert.ToDecimal(dr["saldo"]) : (decimal?)null
            };
        }
    }
}
