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
 * Jersey REST client generated for REST resource:BoletosFacadeREST
 * [boletos]<br>
 * USAGE:
 * <pre>
 *        BoletosClient client = new BoletosClient();
 *        Object response = client.XXX(...);
 *        // do whatever with response
 *        client.close();
 * </pre>
 *
 * @author Drouet
 */
public class BoletosClient {

    private WebTarget webTarget;
    private Client client;
    private static final String BASE_URI = "http://localhost:61210/ec.edu.monster.controlador/AerolineasController.svc";
    private static final ObjectMapper mapper = new ObjectMapper(); // Jackson mapper

    public BoletosClient() {
        client = jakarta.ws.rs.client.ClientBuilder.newClient();
        webTarget = client.target(BASE_URI).path("boletos");
    }

    // Método usando TypeReference en lugar de GenericType
    public <T> T obtenerBoletosPorUsuario(TypeReference<T> typeReference, String idUsuario) throws ClientErrorException {
        WebTarget resource = webTarget;
        resource = resource.path(java.text.MessageFormat.format("usuario/{0}", new Object[]{idUsuario}));
        
        Response response = resource.request().get();
        String json = response.readEntity(String.class);
        
        try {
            return mapper.readValue(json, typeReference);
        } catch (Exception e) {
            throw new RuntimeException("❌ Error al parsear respuesta JSON: " + e.getMessage(), e);
        }
    }

    public Response comprarBoletos(Object requestEntity) throws ClientErrorException {
        return webTarget.path("comprar").request(jakarta.ws.rs.core.MediaType.APPLICATION_JSON).post(jakarta.ws.rs.client.Entity.entity(requestEntity, jakarta.ws.rs.core.MediaType.APPLICATION_JSON), Response.class);
    }

    public void close() {
        client.close();
    }

}
