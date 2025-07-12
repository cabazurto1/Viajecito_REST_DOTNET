using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using ec.edu.monster.modelo;

namespace ec.edu.monster.servicio
{
    public interface IFacturasServicio
    {
        void Crear(Facturas entity);
        void Editar(Facturas entity);
        void Eliminar(int id);
        Facturas Buscar(int id);
        List<Facturas> ListarTodos();
        List<Facturas> ListarRango(int desde, int hasta);
        List<Facturas> BuscarPorUsuario(int idUsuario);
        int Contar();
    }
}