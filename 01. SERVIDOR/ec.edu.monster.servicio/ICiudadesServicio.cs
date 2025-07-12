using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ec.edu.monster.modelo;

namespace ec.edu.monster.servicio
{
    public interface ICiudadesServicio
    {
        void Crear(Ciudades entity);
        void Editar(Ciudades entity);
        void Eliminar(int id);
        Ciudades Buscar(int id);
        List<Ciudades> ListarTodos();
        List<Ciudades> ListarRango(int desde, int hasta);
        int Contar();
    }
}
