using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ec.edu.monster.modelo
{
    [DataContract(Name = "movimiento")]
    public class Movimiento
    {
        [DataMember(Order = 1)]
        public string cuenta { get; set; }

        [DataMember(Order = 2)]
        public int nroMov { get; set; }

        [DataMember(Order = 3)]
        public DateTime fecha { get; set; }

        [DataMember(Order = 4)]
        public string tipo { get; set; }

        [DataMember(Order = 5)]
        public string accion { get; set; }

        [DataMember(Order = 6)]
        public double importe { get; set; }

        public Movimiento() { }

        public Movimiento(string cuenta, int nromov, DateTime fecha, string tipo, string accion, double importe)
        {
            cuenta = cuenta;
            nroMov = nromov;
            fecha = fecha;
            tipo = tipo;
            accion = accion;
            importe = importe;
        }
    }
}