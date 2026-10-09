using System;
using SistemaMatricula.Interfaces;
using SistemaMatricula.Modelos;
using SistemaMatricula.Pagos;
using SistemaMatricula.Servicios;

namespace SistemaMatricula.UI
{
    public class UIManager
    {
        private readonly MatriculaService matriculaService = new MatriculaService();
        private Estudiante estudianteActual = null;

        public void MostrarMenuPrincipal()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE MATRÍCULA UNIVERSITARIA ===");
                Console.WriteLine($"Estudiante Actual: {(estudianteActual != null ? estudianteActual.Nombre + " (" + estudianteActual.Carrera + ")" : "[No registrado]")}");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("1. Registrar / Actualizar Datos del Estudiante");
                Console.WriteLine("2. Ver Cursos Disponibles y Vacantes");
                Console.WriteLine("3. Filtrar Cursos con Vacantes Mínimas (LINQ)");
                Console.WriteLine("4. Realizar Matrícula (Seleccionar Curso y Método de Pago)");
                Console.WriteLine("5. Ver Historial de Matrículas (Alumnos, Cursos y Montos)");
                Console.WriteLine("6. Salir");
                Console.Write("\nSeleccione una opción: ");

                string opcion = Console.ReadLine();

                try
                {
                    switch (opcion)
                    {
                        case "1":
                            RegistrarEstudianteInteractivo();
                            break;
                        case "2":
                            ListarCursos();
                            break;
                        case "3":
                            FiltrarCursosConLinq();
                            break;
                        case "4":
                            RealizarProcesoMatricula();
                            break;
                        case "5":
                            MostrarHistorialMatriculas();
                            break;
                        case "6":
                            salir = true;
                            Console.WriteLine("\nSaliendo del sistema. ¡Hasta luego!");
                            break;
                        default:
                            throw new InvalidOperationException("Opción no válida. Ingrese un número del 1 al 6.");
                    }
                }
                catch (Exception ex)
                {
                    // Manejo robusto mediante try-catch y throw
                    Console.WriteLine($"\n[ERROR]: {ex.Message}");
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        private void RegistrarEstudianteInteractivo()
        {
            Console.Clear();
            Console.WriteLine("--- REGISTRO DE ESTUDIANTE ---");
            Console.Write("Ingrese Nombre Completo: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingrese Edad: ");
            if (!int.TryParse(Console.ReadLine(), out int edad) || edad <= 0)
                throw new ArgumentException("La edad ingresada no es válida.");

            Console.Write("Ingrese Carrera Profesional: ");
            string carrera = Console.ReadLine();

            Console.Write("Ingrese Promedio Ponderado (0 a 20): ");
            if (!double.TryParse(Console.ReadLine(), out double promedio))
                throw new ArgumentException("El promedio debe ser un número válido.");

            // Creación del objeto Estudiante (aplica la clase abstracta y validaciones)
            estudianteActual = new Estudiante(nombre, edad, carrera, promedio);

            Console.WriteLine("\n¡Estudiante registrado con éxito!");
            estudianteActual.MostrarDatos();
            Console.WriteLine("\nPresione cualquier tecla para volver...");
            Console.ReadKey();
        }

        private void ListarCursos()
        {
            Console.Clear();
            Console.WriteLine("--- CATÁLOGO DE CURSOS DISPONIBLES ---");
            foreach (var curso in matriculaService.ObtenerCursos())
            {
                Console.WriteLine($"Código: {curso.Codigo} | Curso: {curso.Nombre} | Créditos: {curso.Creditos} | Vacantes: {curso.Vacantes}");
            }
            Console.WriteLine("\nPresione cualquier tecla para volver...");
            Console.ReadKey();
        }

        private void FiltrarCursosConLinq()
        {
            Console.Clear();
            Console.WriteLine("--- FILTRAR CURSOS POR VACANTES (LINQ) ---");
            Console.Write("Ingrese el número mínimo de vacantes deseadas: ");
            if (!int.TryParse(Console.ReadLine(), out int minVacantes))
                throw new ArgumentException("Debe ingresar un número entero válido.");

            Console.WriteLine($"\nCursos con {minVacantes} o más vacantes:");
            foreach (var curso in matriculaService.FiltrarCursosPorMinimoVacantes(minVacantes))
            {
                Console.WriteLine($"- [{curso.Codigo}] {curso.Nombre} (Disponibles: {curso.Vacantes})");
            }
            Console.WriteLine("\nPresione cualquier tecla para volver...");
            Console.ReadKey();
        }

        private void RealizarProcesoMatricula()
        {
            Console.Clear();
            if (estudianteActual == null)
            {
                throw new InvalidOperationException("Primero debe registrar los datos del estudiante en la opción 1.");
            }

            Console.WriteLine("--- PROCESO DE MATRÍCULA ---");
            Console.WriteLine("Cursos disponibles:");
            foreach (var curso in matriculaService.ObtenerCursos())
            {
                Console.WriteLine($"- [{curso.Codigo}] {curso.Nombre} ({curso.Creditos} créditos) - Vacantes: {curso.Vacantes}");
            }

            Console.Write("\nIngrese el CÓDIGO del curso a matricularse: ");
            string codigoCurso = Console.ReadLine();

            Console.WriteLine("\nSeleccione el Método de Pago:");
            Console.WriteLine("1. Pago en Efectivo (Caja)");
            Console.WriteLine("2. Tarjeta de Crédito / Débito");
            Console.Write("Opción de pago (1 o 2): ");
            string opcionPago = Console.ReadLine();

            IPagoMatricula metodoPago;
            if (opcionPago == "1")
            {
                metodoPago = new PagoEfectivo();
            }
            else if (opcionPago == "2")
            {
                metodoPago = new PagoTarjeta();
            }
            else
            {
                throw new ArgumentException("Opción de pago inválida.");
            }

            // Ejecuta el servicio que procesa el pago, descuenta vacantes y actualiza el diccionario
            matriculaService.RealizarMatricula(estudianteActual, codigoCurso, metodoPago);

            Console.WriteLine("\n========================================");
            Console.WriteLine(" ¡MATRÍCULA REGISTRADA EXITOSAMENTE!");
            Console.WriteLine($" Estudiante: {estudianteActual.Nombre}");
            Console.WriteLine($" Método utilizado: {metodoPago.Metodo}");
            Console.WriteLine("========================================");

            Console.WriteLine("\nPresione cualquier tecla para volver...");
            Console.ReadKey();
        }

        private void MostrarHistorialMatriculas()
        {
            Console.Clear();
            Console.WriteLine("--- HISTORIAL DE MATRÍCULAS REGISTRADAS (DICCIONARIO) ---");
            var historial = matriculaService.ObtenerHistorialMatriculas();

            if (historial.Count == 0)
            {
                Console.WriteLine("\nNo se han registrado matrículas en el sistema todavía.");
            }
            else
            {
                foreach (var carreraEntry in historial)
                {
                    Console.WriteLine($"\nFacultad / Carrera: {carreraEntry.Key}");
                    foreach (var detalle in carreraEntry.Value)
                    {
                        Console.WriteLine($"   * {detalle}");
                    }
                }
            }

            Console.WriteLine("\nPresione cualquier tecla para volver...");
            Console.ReadKey();
        }
    }
}