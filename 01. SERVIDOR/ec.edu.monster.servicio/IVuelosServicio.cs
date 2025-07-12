using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using ec.edu.monster.modelo;

namespace ec.edu.monster.servicio
{
    public interface IVuelosServicio
    {
        void Crear(Vuelos entity);
        void Editar(Vuelos entity);
        void Eliminar(int id);
        Vuelos Buscar(int id);
        List<Vuelos> ListarTodos();
        List<Vuelos> ListarRango(int desde, int hasta);
        List<Vuelos> BuscarPorCiudadesOrdenadoPorValorDesc(string ciudadOrigen, string ciudadDestino, DateTime fecha);
        int Contar();
    }
}