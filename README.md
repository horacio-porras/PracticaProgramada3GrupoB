**1. Grupo B:**
* Adrian Morales Robles
* Cheryl Robles Quesada
* Horacio Porras Marin
* Marypaz Vargas Arce


<br>**2. Repositorio:**
https://github.com/horacio-porras/PracticaProgramada3GrupoB

<br>**3. Especificación básica del proyecto:**

**a) Arquitectura**
* El proyecto utiliza una arquitectura por capas con separación de responsabilidades:
  * **EjemploAPI (tipo Web API):** capa de presentación/exposición HTTP. Contiene controladores, configuración (Program.cs) y middleware.
  * **EjemploAPI.BLL (tipo Class Library):** capa de lógica de negocio. Define servicios e interfaces para orquestar operaciones del dominio.
  * **EjemploAPI.DLL (tipo Class Library):** capa de acceso a datos y entidades. Contiene modelos de dominio y repositorios.
  * **EjemploMinimalAPI (tipo Web API):** proyecto adicional de ejemplo con enfoque Minimal API.

**b) Libraries / paquetes NuGet utilizados**
* En la solución se usan principalmente:
  * Swashbuckle.AspNetCore (documentación Swagger/OpenAPI).
  * Swashbuckle.AspNetCore.Filters (ejemplos y filtros para Swagger).
  * Microsoft.AspNetCore.OpenApi (en proyecto Minimal API).
  * Microsoft.AspNetCore.Authentication.JwtBearer (en proyecto Minimal API).

**c) Principios SOLID + Patrones utilizados**
* **Principios SOLID:**
  * **S (Single Responsibility):** cada capa/clase tiene responsabilidad clara (AutomovilController, AutomovilServicio, AutomovilRepositorio).
  * **O (Open/Closed):** se puede extender lógica (nuevos servicios/repositorios) sin modificar la estructura global.
  * **L (Liskov):** implementaciones respetan contratos de interfaces.
  * **I (Interface Segregation):** contratos específicos (IAutomovilServicio, IAutomovilRepositorio) evitan dependencias innecesarias.
  * **D (Dependency Inversion):** el controlador depende de abstracciones (interfaces) y se resuelve por inyección de dependencias.
* **Patrones:**
  * **Repository Pattern:** encapsula acceso a datos en AutomovilRepositorio.
  * **Service Layer Pattern:** centraliza reglas de negocio en AutomovilServicio.
  * **Dependency Injection:** configuración en Program.cs para desacoplar componentes.
  * **Middleware Pattern:** manejo transversal de excepciones con ExceptionHandlingMiddleware.
