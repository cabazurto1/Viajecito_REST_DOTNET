using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ec.edu.monster.modelo;

namespace ec.edu.monster.servicio
{
    public interface IUsuariosServicio
    {
        void Crear(Usuarios entity);
        void Editar(Usuarios entity);
        void Eliminar(int id);
        Usuarios Buscar(int id);
        List<Usuarios> ListarTodos();
        List<Usuarios> ListarRango(int desde, int hasta);
        Usuarios Login(string username, string password);
        int Contar();
    }
}