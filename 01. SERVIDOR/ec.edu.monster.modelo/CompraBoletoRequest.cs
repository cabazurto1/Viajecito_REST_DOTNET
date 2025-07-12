using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ec.edu.monster.modelo
{
    [DataContract(Name = "compraBoletoRequest")]
    public class CompraBoletoRequest
    {
        [DataMember(Order = 1)]
        public int idUsuario { get; set; }

        [DataMember(Order = 2)]
        public List<VueloCompra> vuelos { get; set; }

        [DataMember(Order = 3)]
        public bool esCredito { get; set; }

        [DataMember(Order = 4)]
        public int numeroCuotas { get; set; }

        [DataMember(Order = 5)]
        public double tasaInteresAnual { get; set; }
    }
}
