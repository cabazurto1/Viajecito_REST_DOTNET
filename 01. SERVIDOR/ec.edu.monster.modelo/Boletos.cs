using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Runtime.Serialization;
namespace ec.edu.monster.modelo
{
    [DataContract(Name = "boletos")]
    public class Boletos
    {
        [DataMember(Order = 1)]
        public int? idBoleto { get; set; }

        [DataMember(Order = 2)]
        public string numeroBoleto { get; set; }

        [DataMember(Order = 3)]
        public DateTime? fechaCompra { get; set; }

        [DataMember(Order = 4)]
        public decimal precioCompra { get; set; }

        [DataMember(Order = 5)]
        public Usuarios idUsuario { get; set; }

        [DataMember(Order = 6)]
        public Vuelos idVuelo { get; set; }

        [DataMember(Order = 7)]
        public Facturas idFactura { get; set; }

        public Boletos() { }

        public Boletos(int? idBoleto)
        {
            idBoleto = idBoleto;
        }

        public Boletos(int? idBoleto, string numeroBoleto, decimal precioCompra)
        {
            idBoleto = idBoleto;
            numeroBoleto = numeroBoleto;
            precioCompra = precioCompra;
        }
    }
}
