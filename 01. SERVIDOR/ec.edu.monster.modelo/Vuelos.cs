using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ec.edu.monster.modelo
{
    [DataContract(Name = "vuelos")]
    public class Vuelos
    {
        [DataMember(Order = 1)]
        public int? idVuelo { get; set; }

        [DataMember(Order = 2)]
        public string codigoVuelo { get; set; }

        [DataMember(Order = 3)]
        public decimal valor { get; set; }

        // Propiedad interna para la fecha
        private DateTime _horaSalida;

        [DataMember(Order = 4)]
        public DateTime horaSalida
        {
            get { return _horaSalida; }
            set { _horaSalida = value; }
        }

        // Propiedad adicional para serialización JSON con formato ISO
        [DataMember(Order = 9, Name = "horaSalidaISO")]
        public string HoraSalidaISO
        {
            get { return _horaSalida.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"); }
            set
            {
                if (DateTime.TryParse(value, out DateTime parsed))
                    _horaSalida = parsed;
            }
        }

        [DataMember(Order = 5)]
        public int capacidad { get; set; }

        [DataMember(Order = 6)]
        public int disponibles { get; set; }

        [DataMember(Order = 7)]
        public Ciudades idCiudadOrigen { get; set; }

        [DataMember(Order = 8)]
        public Ciudades idCiudadDestino { get; set; }

        public Vuelos() { }

        public Vuelos(int? idVuelo)
        {
            this.idVuelo = idVuelo;
        }

        public Vuelos(int? idVuelo, string codigoVuelo, decimal valor, DateTime horaSalida, int capacidad, int disponibles)
        {
            this.idVuelo = idVuelo;
            this.codigoVuelo = codigoVuelo;
            this.valor = valor;
            this.horaSalida = horaSalida;
            this.capacidad = capacidad;
            this.disponibles = disponibles;
        }
    }
}