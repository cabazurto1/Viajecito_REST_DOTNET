using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace ec.edu.monster.modelo
{
    [DataContract(Name = "vueloCompra")]
    public class VueloCompra
    {
        [DataMember(Order = 1)]
        public int idVuelo { get; set; }

        [DataMember(Order = 2)]
        public int cantidad { get; set; }
    }
}