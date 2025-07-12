using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ec.edu.monster.modelo
{
    [DataContract]
    public class Facturas
    {
        [DataMember]
        public int idFactura { get; set; }

        [DataMember]
        public string numeroFactura { get; set; }

        [DataMember]
        public decimal precioSinIva { get; set; }

        [DataMember]
        public decimal precioConIva { get; set; }

        [DataMember]
        public DateTime? fechaFactura { get; set; }

        [DataMember]
        public Usuarios idUsuario { get; set; }

        [DataMember]
        public List<Boletos> boletosCollection { get; set; }

        public Facturas()
        {
            boletosCollection = new List<Boletos>();
        }
    }
}