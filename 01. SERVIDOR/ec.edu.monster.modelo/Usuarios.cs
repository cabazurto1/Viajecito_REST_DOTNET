using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace ec.edu.monster.modelo
{
    [DataContract(Name = "usuarios")]
    public class Usuarios
    {
        [DataMember(Order = 1)]
        public int? idUsuario { get; set; }

        [DataMember(Order = 2)]
        public string nombre { get; set; }

        [DataMember(Order = 3)]
        public string username { get; set; }

        [DataMember(Order = 4)]
        public string password { get; set; }

        [DataMember(Order = 5)]
        public string telefono { get; set; }

        [DataMember(Order = 6)]
        public string cedula { get; set; }

        [DataMember(Order = 7)]
        public string correo { get; set; }

        public Usuarios() { }

        public Usuarios(int? idUsuario)
        {
            idUsuario = idUsuario;
        }

        public Usuarios(int? idUsuario, string nombre, string username, string password, string cedula, string correo)
        {
            idUsuario = idUsuario;
            nombre = nombre;
            username = username;
            password = password;
            cedula = cedula;
            correo = correo;
        }
    }
}