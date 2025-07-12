using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// AerolineasController.svc.cs
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using ec.edu.monster.modelo;
using ec.edu.monster.servicio;

namespace ec.edu.monster.controlador
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AerolineasController : IAerolineasController
    {
        // Instancias de los servicios
        private readonly IAmortizacionServicio amortizacionServicio = new AmortizacionServicio();
        private readonly IBoletosServicio boletosServicio = new BoletosServicio();
        private readonly ICiudadesServicio ciudadesServicio = new CiudadesServicio();
        private readonly IFacturasServicio facturasServicio = new FacturasServicio();
        private readonly IUsuariosServicio usuariosServicio = new UsuariosServicio();
        private readonly IVuelosServicio vuelosServicio = new VuelosServicio();

        // ========== IMPLEMENTACIÓN AMORTIZACION ==========
        [JsonDateFormat]
        public void CrearAmortizacion(Amortizacion entity)
        {
            try
            {
                amortizacionServicio.Crear(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public void EditarAmortizacion(string id, Amortizacion entity)
        {
            try
            {
                entity.idAmortizacion = Convert.ToInt32(id);
                amortizacionServicio.Editar(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public void EliminarAmortizacion(string id)
        {
            try
            {
                amortizacionServicio.Eliminar(Convert.ToInt32(id));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public Amortizacion BuscarAmortizacion(string id)
        {
            try
            {
                return amortizacionServicio.Buscar(Convert.ToInt32(id));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public List<Amortizacion> ListarAmortizaciones()
        {
            try
            {
                return amortizacionServicio.ListarTodos();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public List<Amortizacion> ListarAmortizacionesRango(string from, string to)
        {
            try
            {
                return amortizacionServicio.ListarRango(Convert.ToInt32(from), Convert.ToInt32(to));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public List<Amortizacion> ObtenerAmortizacionesPorFactura(string idFactura)
        {
            try
            {
                return amortizacionServicio.ObtenerPorFactura(Convert.ToInt32(idFactura));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public string ContarAmortizaciones()
        {
            try
            {
                return amortizacionServicio.Contar().ToString();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        // ========== IMPLEMENTACIÓN BOLETOS ==========
        [JsonDateFormat]
        public void CrearBoleto(Boletos entity)
        {
            try
            {
                boletosServicio.Crear(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public void EditarBoleto(string id, Boletos entity)
        {
            try
            {
                entity.idBoleto = Convert.ToInt32(id);
                boletosServicio.Editar(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public void EliminarBoleto(string id)
        {
            try
            {
                boletosServicio.Eliminar(Convert.ToInt32(id));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public Boletos BuscarBoleto(string id)
        {
            try
            {
                return boletosServicio.Buscar(Convert.ToInt32(id));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public List<Boletos> ListarBoletos()
        {
            try
            {
                return boletosServicio.ListarTodos();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public List<Boletos> ListarBoletosRango(string from, string to)
        {
            try
            {
                return boletosServicio.ListarRango(Convert.ToInt32(from), Convert.ToInt32(to));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public string ComprarBoletos(CompraBoletoRequest request)
        {
            try
            {
                return boletosServicio.ComprarBoletos(request);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public List<Boletos> ObtenerBoletosPorUsuario(string idUsuario)
        {
            try
            {
                return boletosServicio.ObtenerBoletosPorUsuario(Convert.ToInt32(idUsuario));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public string ContarBoletos()
        {
            try
            {
                return boletosServicio.Contar().ToString();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        // ========== IMPLEMENTACIÓN CIUDADES ==========
        [JsonDateFormat]
        public void CrearCiudad(Ciudades entity)
        {
            try
            {
                ciudadesServicio.Crear(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public void EditarCiudad(string id, Ciudades entity)
        {
            try
            {
                entity.idCiudad = Convert.ToInt32(id);
                ciudadesServicio.Editar(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public void EliminarCiudad(string id)
        {
            try
            {
                ciudadesServicio.Eliminar(Convert.ToInt32(id));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public Ciudades BuscarCiudad(string id)
        {
            try
            {
                return ciudadesServicio.Buscar(Convert.ToInt32(id));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public List<Ciudades> ListarCiudades()
        {
            try
            {
                return ciudadesServicio.ListarTodos();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]

        public List<Ciudades> ListarCiudadesRango(string from, string to)
        {
            try
            {
                return ciudadesServicio.ListarRango(Convert.ToInt32(from), Convert.ToInt32(to));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public string ContarCiudades()
        {
            try
            {
                return ciudadesServicio.Contar().ToString();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        // ========== IMPLEMENTACIÓN FACTURAS ==========
        [JsonDateFormat]
        public void CrearFactura(Facturas entity)
        {
            try
            {
                facturasServicio.Crear(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]

        public void EditarFactura(string id, Facturas entity)
        {
            try
            {
                entity.idFactura = Convert.ToInt32(id);
                facturasServicio.Editar(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public void EliminarFactura(string id)
        {
            try
            {
                facturasServicio.Eliminar(Convert.ToInt32(id));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public Facturas BuscarFactura(string id)
        {
            try
            {
                return facturasServicio.Buscar(Convert.ToInt32(id));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public List<Facturas> ListarFacturas()
        {
            try
            {
                return facturasServicio.ListarTodos();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public List<Facturas> ListarFacturasRango(string from, string to)
        {
            try
            {
                return facturasServicio.ListarRango(Convert.ToInt32(from), Convert.ToInt32(to));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public List<Facturas> BuscarFacturasPorUsuario(string idUsuario)
        {
            try
            {
                return facturasServicio.BuscarPorUsuario(Convert.ToInt32(idUsuario));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public string ContarFacturas()
        {
            try
            {
                return facturasServicio.Contar().ToString();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        // ========== IMPLEMENTACIÓN USUARIOS ==========
        public void CrearUsuario(Usuarios entity)
        {
            try
            {
                usuariosServicio.Crear(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public void EditarUsuario(string id, Usuarios entity)
        {
            try
            {
                entity.idUsuario = Convert.ToInt32(id);
                usuariosServicio.Editar(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public void EliminarUsuario(string id)
        {
            try
            {
                usuariosServicio.Eliminar(Convert.ToInt32(id));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public Usuarios BuscarUsuario(string id)
        {
            try
            {
                return usuariosServicio.Buscar(Convert.ToInt32(id));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public List<Usuarios> ListarUsuarios()
        {
            try
            {
                return usuariosServicio.ListarTodos();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public List<Usuarios> ListarUsuariosRango(string from, string to)
        {
            try
            {
                return usuariosServicio.ListarRango(Convert.ToInt32(from), Convert.ToInt32(to));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public Usuarios LoginUsuario(string username, string password)
        {
            try
            {
                return usuariosServicio.Login(username, password);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]
        public string ContarUsuarios()
        {
            try
            {
                return usuariosServicio.Contar().ToString();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]


        // ========== IMPLEMENTACIÓN VUELOS ==========
        public void CrearVuelo(Vuelos entity)
        {
            try
            {
                vuelosServicio.Crear(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]

        public void EditarVuelo(string id, Vuelos entity)
        {
            try
            {
                entity.idVuelo = Convert.ToInt32(id);
                vuelosServicio.Editar(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]

        public void EliminarVuelo(string id)
        {
            try
            {
                vuelosServicio.Eliminar(Convert.ToInt32(id));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]

        public Vuelos BuscarVuelo(string id)
        {
            try
            {
                return vuelosServicio.Buscar(Convert.ToInt32(id));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]

        public List<Vuelos> ListarVuelos()
        {
            try
            {
                return vuelosServicio.ListarTodos();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]

        public List<Vuelos> ListarVuelosRango(string from, string to)
        {
            try
            {
                return vuelosServicio.ListarRango(Convert.ToInt32(from), Convert.ToInt32(to));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [JsonDateFormat]

        public List<Vuelos> BuscarVuelosPorCiudades(string origen, string destino, string horaSalida)
        {
            try
            {
                DateTime fecha = DateTime.ParseExact(horaSalida, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                return vuelosServicio.BuscarPorCiudadesOrdenadoPorValorDesc(origen, destino, fecha);
            }
            catch (FormatException)
            {
                throw new FaultException("Formato de fecha inválido. Use yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [JsonDateFormat]

        public string ContarVuelos()
        {
            try
            {
                return vuelosServicio.Contar().ToString();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
