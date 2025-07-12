const BASE_URL = 'http://192.168.18.158:8093/ec.edu.monster.controlador/AerolineasController.svc';
// 🔹 Obtener todas las ciudades
export const obtenerCiudades = async () => {
  try {
    const response = await fetch(`${BASE_URL}/ciudades`);
    if (!response.ok) throw new Error('Error al obtener ciudades');
    
    const ciudades = await response.json();

    return ciudades.map(c => ({
      id: c.idCiudad,
      codigo: c.codigoCiudad,
      nombre: c.nombreCiudad
    }));
  } catch (error) {
    console.error('❌ Error al obtener ciudades (REST):', error);
    return [];
  }
};

// 🔹 Obtener ciudad por ID
export const obtenerCiudadPorId = async (idCiudad) => {
  try {
    const response = await fetch(`${BASE_URL}/ciudades/${idCiudad}`);
    if (!response.ok) throw new Error('Ciudad no encontrada');

    const c = await response.json();

    return {
      id: c.idCiudad,
      codigo: c.codigoCiudad,
      nombre: c.nombreCiudad
    };
  } catch (error) {
    console.error('❌ Error al obtener ciudad por ID (REST):', error);
    return null;
  }
};
