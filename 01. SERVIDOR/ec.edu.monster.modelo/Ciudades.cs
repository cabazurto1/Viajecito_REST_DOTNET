using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace ec.edu.monster.modelo
{
    [DataContract(Name = "ciudades")]
    public class Ciudades
    {
        [DataMember(Order = 1)]
        public int? idCiudad { get; set; }

        [DataMember(Order = 2)]
        public string codigoCiudad { get; set; }

        [DataMember(Order = 3)]
        public string nombreCiudad { get; set; }

        public Ciudades() { }

        public Ciudades(int? idCiudad)
        {
            idCiudad = idCiudad;
        }

        public Ciudades(int? idCiudad, string codigoCiudad, string nombreCiudad)
        {
            idCiudad = idCiudad;
            codigoCiudad = codigoCiudad;
            nombreCiudad = nombreCiudad;
        }
    }
}
