
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// IAerolineasController.cs
using System.ServiceModel;
using System.ServiceModel.Web;
using ec.edu.monster.modelo;
using Newtonsoft.Json;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace ec.edu.monster.controlador
{
    [ServiceContract]
    public interface IAerolineasController
    {
        // ========== AMORTIZACION ENDPOINTS ==========
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/amortizacion",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void CrearAmortizacion(Amortizacion entity);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/amortizacion/{id}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void EditarAmortizacion(string id, Amortizacion entity);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/amortizacion/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        void EliminarAmortizacion(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/amortizacion/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        Amortizacion BuscarAmortizacion(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/amortizacion",
            ResponseFormat = WebMessageFormat.Json)]
        List<Amortizacion> ListarAmortizaciones();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/amortizacion/{from}/{to}",
            ResponseFormat = WebMessageFormat.Json)]
        List<Amortizacion> ListarAmortizacionesRango(string from, string to);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/amortizacion/factura/{idFactura}",
            ResponseFormat = WebMessageFormat.Json)]
        List<Amortizacion> ObtenerAmortizacionesPorFactura(string idFactura);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/amortizacion/count",
            ResponseFormat = WebMessageFormat.Json)]
        string ContarAmortizaciones();

        // ========== BOLETOS ENDPOINTS ==========
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/boletos",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void CrearBoleto(Boletos entity);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/boletos/{id}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void EditarBoleto(string id, Boletos entity);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/boletos/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        void EliminarBoleto(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/boletos/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        Boletos BuscarBoleto(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/boletos",
            ResponseFormat = WebMessageFormat.Json)]
        List<Boletos> ListarBoletos();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/boletos/{from}/{to}",
            ResponseFormat = WebMessageFormat.Json)]
        List<Boletos> ListarBoletosRango(string from, string to);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/boletos/comprar",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string ComprarBoletos(CompraBoletoRequest request);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/boletos/usuario/{idUsuario}",
            ResponseFormat = WebMessageFormat.Json)]
        List<Boletos> ObtenerBoletosPorUsuario(string idUsuario);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/boletos/count",
            ResponseFormat = WebMessageFormat.Json)]
        string ContarBoletos();

        // ========== CIUDADES ENDPOINTS ==========
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/ciudades",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void CrearCiudad(Ciudades entity);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/ciudades/{id}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void EditarCiudad(string id, Ciudades entity);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/ciudades/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        void EliminarCiudad(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ciudades/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        Ciudades BuscarCiudad(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ciudades",
            ResponseFormat = WebMessageFormat.Json)]
        List<Ciudades> ListarCiudades();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ciudades/{from}/{to}",
            ResponseFormat = WebMessageFormat.Json)]
        List<Ciudades> ListarCiudadesRango(string from, string to);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ciudades/count",
            ResponseFormat = WebMessageFormat.Json)]
        string ContarCiudades();

        // ========== FACTURAS ENDPOINTS ==========
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/facturas",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void CrearFactura(Facturas entity);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/facturas/{id}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void EditarFactura(string id, Facturas entity);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/facturas/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        void EliminarFactura(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/facturas/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        Facturas BuscarFactura(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/facturas",
            ResponseFormat = WebMessageFormat.Json)]
        List<Facturas> ListarFacturas();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/facturas/{from}/{to}",
            ResponseFormat = WebMessageFormat.Json)]
        List<Facturas> ListarFacturasRango(string from, string to);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/facturas/usuario/{idUsuario}",
            ResponseFormat = WebMessageFormat.Json)]
        List<Facturas> BuscarFacturasPorUsuario(string idUsuario);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/facturas/count",
            ResponseFormat = WebMessageFormat.Json)]
        string ContarFacturas();

        // ========== USUARIOS ENDPOINTS ==========
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/usuarios",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void CrearUsuario(Usuarios entity);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/usuarios/{id}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void EditarUsuario(string id, Usuarios entity);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/usuarios/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        void EliminarUsuario(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/usuarios/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        Usuarios BuscarUsuario(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/usuarios",
            ResponseFormat = WebMessageFormat.Json)]
        List<Usuarios> ListarUsuarios();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/usuarios/{from}/{to}",
            ResponseFormat = WebMessageFormat.Json)]
        List<Usuarios> ListarUsuariosRango(string from, string to);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/usuarios/login?username={username}&password={password}",
            ResponseFormat = WebMessageFormat.Json)]
        Usuarios LoginUsuario(string username, string password);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/usuarios/count",
            ResponseFormat = WebMessageFormat.Json)]
        string ContarUsuarios();

        // ========== VUELOS ENDPOINTS ==========
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/vuelos",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void CrearVuelo(Vuelos entity);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/vuelos/{id}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void EditarVuelo(string id, Vuelos entity);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/vuelos/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        void EliminarVuelo(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/vuelos/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        Vuelos BuscarVuelo(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/vuelos",
            ResponseFormat = WebMessageFormat.Json)]
        List<Vuelos> ListarVuelos();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/vuelos/{from}/{to}",
            ResponseFormat = WebMessageFormat.Json)]
        List<Vuelos> ListarVuelosRango(string from, string to);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/vuelos/buscar?origen={origen}&destino={destino}&horaSalida={horaSalida}",
            ResponseFormat = WebMessageFormat.Json)]
        List<Vuelos> BuscarVuelosPorCiudades(string origen, string destino, string horaSalida);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/vuelos/count",
            ResponseFormat = WebMessageFormat.Json)]
        string ContarVuelos();
    }
}

