using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections.Generic;
using ec.edu.monster.modelo;

namespace ec.edu.monster.servicio
{
    public interface IAmortizacionServicio
    {
        void Crear(Amortizacion entity);
        void Editar(Amortizacion entity);
        void Eliminar(int id);
        Amortizacion Buscar(int id);
        List<Amortizacion> ListarTodos();
        List<Amortizacion> ListarRango(int desde, int hasta);
        List<Amortizacion> ObtenerPorFactura(int idFactura);
        int Contar();
    }
}

