using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// IBoletosServicio.cs
using ec.edu.monster.modelo;

namespace ec.edu.monster.servicio
{
    public interface IBoletosServicio
    {
        void Crear(Boletos entity);
        void Editar(Boletos entity);
        void Eliminar(int id);
        Boletos Buscar(int id);
        List<Boletos> ListarTodos();
        List<Boletos> ListarRango(int desde, int hasta);
        List<Boletos> ObtenerBoletosPorUsuario(int idUsuario);
        string ComprarBoletos(CompraBoletoRequest request);
        int Contar();
    }
}
