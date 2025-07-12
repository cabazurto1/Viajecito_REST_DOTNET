/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/WebServices/JerseyClient.java to edit this template
 */
package ec.edu.viajecito.client;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import jakarta.ws.rs.ClientErrorException;
import jakarta.ws.rs.client.Client;
import jakarta.ws.rs.client.WebTarget;
import jakarta.ws.rs.core.GenericType;
import jakarta.ws.rs.core.Response;

/**
 * Jersey REST client generated for REST resource:VuelosFacadeREST [vuelos]<br>
 * USAGE:
 * <pre>
 *        VuelosClient client = new VuelosClient();
 *        Object response = client.XXX(...);
 *        // do whatever with response
 *        client.close();
 * </pre>
 *
 * @author Drouet
 */
public class VuelosClient {

    private WebTarget webTarget;
    private Client client;
    private static final String BASE_URI = "http://localhost:61210/ec.edu.monster.controlador/AerolineasController.svc";
    private static final ObjectMapper mapper = new ObjectMapper(); // Jackson mapper

    public VuelosClient() {
        client = jakarta.ws.rs.client.ClientBuilder.newClient();
        webTarget = client.target(BASE_URI).path("vuelos");
    }

    public <T> T buscarPorCiudadesOrdenadoPorValorDesc(TypeReference<T> responseType, String origen, String destino, String horaSalida) throws ClientErrorException {
        WebTarget resource = webTarget;
        if (origen != null) {
            resource = resource.queryParam("origen", origen);
        }
        if (destino != null) {
            resource = resource.queryParam("destino", destino);
        }

        if (horaSalida != null) {
            resource = resource.queryParam("horaSalida", horaSalida);
        }

        resource = resource.path("buscar");
        Response response = resource.request().get();
        String json = response.readEntity(String.class);

        try {
            return mapper.readValue(json, responseType);
        } catch (Exception e) {
            throw new RuntimeException("❌ Error al parsear respuesta JSON: " + e.getMessage(), e);
        }
    }

    public <T> T find(Class<T> responseType, String id) throws ClientErrorException {
        WebTarget resource = webTarget;
        resource = resource.path(java.text.MessageFormat.format("{0}", new Object[]{id}));
        // Forzar lectura como texto sin importar Content-Type
        Response response = resource.request().get();
        String json = response.readEntity(String.class);

        try {
            return mapper.readValue(json, responseType);
        } catch (Exception e) {
            throw new RuntimeException("❌ Error al parsear respuesta JSON: " + e.getMessage(), e);
        }
    }

    public <T> T findAll(TypeReference<T> responseType) throws ClientErrorException {
        WebTarget resource = webTarget;
        Response response = resource.request().get();
        String json = response.readEntity(String.class);

        try {
            return mapper.readValue(json, responseType);
        } catch (Exception e) {
            throw new RuntimeException("❌ Error al parsear respuesta JSON: " + e.getMessage(), e);
        }
    }

    public void close() {
        client.close();
    }

}
