package ec.edu.viajecito.controller;

import com.fasterxml.jackson.core.type.TypeReference;
import ec.edu.viajecito.client.VuelosClient;
import ec.edu.viajecito.model.Vuelo;
import jakarta.ws.rs.core.GenericType;
import java.util.List;

public class VuelosController {

    public List<Vuelo> obtenerTodosVuelos() {
        VuelosClient client = new VuelosClient();
        try {
            return client.findAll(new TypeReference<List<Vuelo>>() {});
        } finally {
            client.close();
        }
    }
    
    public Vuelo obtenerVueloPorId(String idVuelo) {
        VuelosClient client = new VuelosClient();
        try {
            Vuelo factura = client.find(Vuelo.class, idVuelo);
            return factura;
        } finally {
            client.close();
        }
    }
    
    public List<Vuelo> obtenerVuelosPorCiudad(String origen, String destino, String horaSalida) {
        VuelosClient client = new VuelosClient();
        try {
            return client.buscarPorCiudadesOrdenadoPorValorDesc(new TypeReference<List<Vuelo>>() {}, origen, destino, horaSalida);
        } finally {
            client.close();
        }
    }
}
