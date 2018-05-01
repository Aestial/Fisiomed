# Fisiomed

### Instrucciones
#### Taller para creación de UI personalizado con Unity. (para Fisiomed, 05/05/2018).

1.	Descargar e instalar Unity: https://store.unity.com/download?ref=personal
1.1 Elegir los siguientes componentes en el instalador:
- Unity 2017.4.2
- MonoDevelop / Unity Debugger
- Standard Assets
- Example Project (Omitir en caso de poca memoria en HDD).
- Android Build Support
- WebGL build Support
![Componentes a Descargar](https://www.dropbox.com/s/2lr143g50p7n2ou/Screenshot%202018-05-01%2018.39.34.png)
En la imagen se muestran seleccionados los componentes mínimos deseables.
1.2 (Opcional) Elegir alguno(s) de los siguientes componentes dependiendo los casos planteados.
- Documentation: se planea trabajar con poco o nulo acceso a internet. Poder consultar la documentación en todo momento es necesario para cualquier usuario de Unity.
- iOS Build Support: En caso de los usarios de Mac OS que cuenten con un dispositivo iOS (iPhone/iPad).
2.  Descargar e instalar Github Desktop: https://desktop.github.com/
3.	Clonar proyecto de Github: https://github.com/Aestial/Fisiomed, para ello seleccionar la opción 'Clone or download' -> 'Open in Desktop'.
4.	Descargar Assets (videos): Google Drive de cuenta especial. Faltan en repositorio
5.	Reconocimiento rápido de la interfaz o Editor de Unity, relacionarla con algunos programas de creación de contenido como Photoshop, Blender, Maya, etc.
6.	Agregar assets en proyecto (siguiendo una buena estructura, jerarquía y nomenclatura)
6.	Checar opciones de importación de assets, crear sprites/UI a partir de imágenes PNG, crear sprites a partir del multi sprite editor (textura con tilling, atlas).
7.	Creación de escena, comprensión de los elementos de la escena, creación de objetos.
8.	Creación de Canvas, para interfaz 2D (UI). Configuración del canvas, conocimiento de los diferentes componentes y comportamiento. Descripción de eventos de entrada de usuario, input.
9. 	Creación de Video Player, Render Texture y un Raw Image para asignar el video en Canvas.
10.	Creación de elementos reconocidos en algunas de las pantallas (de las más sencillas), personalizar el fondo, la fuente, colores, etc.
11.	Terminar la pantalla seleccionada (layout estático) desplegando algún tipo de mensaje para cada evento aunque no se tenga la lógica programada.
12. Animaciones de propiedades de elementos de UI (botones, imágenes, etc.)
13.	Programación básica de lógica de los eventos. Disparar un evento (reproducir sonido, cambiar texto, etc.) Introducción a la programación.

### Consideraciones:
- La aplicación es para diferentes dispositivos móviles con diferentes resoluciones, por lo que debe ser un diseño responsivo en todo momento. Responsive-based design.
- Manejar los recursos de manera óptima y correcta, i.e. mínima resolución en imágenes, texto creado en Unity, no abuso de gradientes (no se pueden comprimir), creación de atlas, etc.
- Se debe tener una pantalla (imagen o video) como referencia para el acomodo o layout de los elementos de la UI.
