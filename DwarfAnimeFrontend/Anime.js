// Función para obtener y mostrar la lista de animes
async function fetchAnimes() {
  const apiUrl = 'https://dwarfanimeapiprova-gtbfechmcganb2aw.spaincentral-01.azurewebsites.net/api/Anime'; 
 // Cambia esta URL a la URL de tu API
  const animeListDiv = document.getElementById('anime-list');

  try {
    const response = await fetch(apiUrl);

    if (!response.ok) {
      throw new Error("Network response was not ok");
    }

    const data = await response.json(); // Supón que la API devuelve JSON

    // Limpiar el texto "Loading..."
    animeListDiv.innerHTML = '';

    // Verifica si 'data' contiene la lista dentro de '$values'
    if (data && Array.isArray(data.$values)) {
      // Crear un elemento de lista
      const ul = document.createElement('ul');

      // Recorrer los animes y agregar elementos de lista
      data.$values.forEach(anime => {
        const li = document.createElement('li');
        li.textContent = anime.name; // Asumiendo que cada anime tiene un campo 'name'
        ul.appendChild(li);
      });

      // Añadir la lista al contenedor
      animeListDiv.appendChild(ul);
    } else {
      // Si no es una lista, mostrar un mensaje de error
      animeListDiv.innerHTML = `<p>Error: No se recibió una lista de animes.</p>`;
    }
  } catch (error) {
    animeListDiv.innerHTML = `<p>Error: ${error.message}</p>`;
  }
}

// Función para agregar un nuevo anime
async function addAnime(event) {
  event.preventDefault(); // Evitar que el formulario recargue la página

  const name = document.getElementById('name').value;
  const description = document.getElementById('description').value;

  const anime = { 
      name, 
      description 
      // No enviar AnimeScores aquí
  };

  console.log('Datos enviados:', anime); // Depuración

  const apiUrl = 'https://dwarfanimeapiprova-gtbfechmcganb2aw.spaincentral-01.azurewebsites.net/api/Anime'; 
  // Cambia esta URL a la URL de tu API

  try {
      const response = await fetch(apiUrl, {
          method: 'POST',
          headers: {
              'Content-Type': 'application/json',
          },
          body: JSON.stringify(anime),
      });

      if (!response.ok) {
          console.log(await response.text()); // Verifica la respuesta completa del servidor
          throw new Error('Error adding anime');
      }

      const newAnime = await response.json();
      document.getElementById('response-message').innerHTML = `Anime added: ${newAnime.name}`;
  } catch (error) {
      console.error('Error:', error); // Depuración
      document.getElementById('response-message').innerHTML = `Error: ${error.message}`;
  }
}

// Esperamos a que se cargue el DOM
document.addEventListener("DOMContentLoaded", function() {
  // Llamamos a la función fetchAnimes cuando la página se cargue
  fetchAnimes();

  // Agregamos el event listener al formulario
  const form = document.getElementById('add-anime-form');
  if (form) {
    form.addEventListener('submit', addAnime);
  }
});
