using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Amortizacion.cs
using System.Runtime.Serialization;
namespace ec.edu.monster.modelo
{
    [DataContract(Name = "amortizacion")]
    public class Amortizacion
    {
        [DataMember(Order = 1)]
        public int? idAmortizacion { get; set; }

        [DataMember(Order = 2)]
        public int numeroCuota { get; set; }

        [DataMember(Order = 3)]
        public decimal? valorCuota { get; set; }

        [DataMember(Order = 4)]
        public decimal? interesPagado { get; set; }

        [DataMember(Order = 5)]
        public decimal? capitalPagado { get; set; }

        [DataMember(Order = 6)]
        public decimal? saldo { get; set; }

        [DataMember(Order = 7)]
        public Facturas idFactura { get; set; }

        public Amortizacion() { }

        public Amortizacion(int? idAmortizacion)
        {
            idAmortizacion = idAmortizacion;
        }

        public Amortizacion(int? idAmortizacion, int numeroCuota)
        {
            idAmortizacion = idAmortizacion;
            numeroCuota = numeroCuota;
        }
    }
}
