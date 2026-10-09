# 🎓 Sistema de Matrícula Universitaria en C#

Aplicación de consola desarrollada en C# orientada a objetos (POO), aplicando principios SOLID, patrones de diseño (Repository Pattern), manejo de excepciones robusto (`try-catch` y `throw`), y estructuras avanzadas como Diccionarios y consultas LINQ.

## 🚀 Características Principales
* **Gestión de Estudiantes:** Registro validado mediante clases abstractas (`PersonaBase`).
* **Catálogo de Cursos:** Listado interactivo de materias con control de vacantes en tiempo real.
* **Consultas Avanzadas (LINQ):** Filtrado dinámico de cursos según disponibilidad mínima.
* **Polimorfismo en Pagos:** Procesamiento de pagos mediante interfaces (`IPagoMatricula`) para Efectivo y Tarjeta.
* **Control de Historial:** Registro detallado de matrículas organizadas por carrera utilizando Diccionarios (`Dictionary<string, List<string>>`).

## 🛠️ Tecnologías Utilizadas
* Lenguaje: C# (.NET)
* Paradigma: Programación Orientada a Objetos (POO)
* Arquitectura: Modular por capas (`Interfaces`, `Modelos`, `Pagos`, `Repos`, `Servicios`, `UI`)
