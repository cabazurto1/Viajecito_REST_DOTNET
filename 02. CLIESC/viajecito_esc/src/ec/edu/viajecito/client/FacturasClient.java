/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
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
 *
 * @author Drouet
 */
public class FacturasClient {

    private WebTarget webTarget;
    private Client client;
    private static final String BASE_URI = "http://localhost:61210/ec.edu.monster.controlador/AerolineasController.svc";
    private static final ObjectMapper mapper = new ObjectMapper(); // Jackson mapper

    public FacturasClient() {
        client = jakarta.ws.rs.client.ClientBuilder.newClient();
        webTarget = client.target(BASE_URI).path("facturas");
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

    public <T> T findByUsuario(TypeReference<T> responseType, String idUsuario) throws ClientErrorException {
        WebTarget resource = webTarget;
        resource = resource.path(java.text.MessageFormat.format("usuario/{0}", new Object[]{idUsuario}));
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
