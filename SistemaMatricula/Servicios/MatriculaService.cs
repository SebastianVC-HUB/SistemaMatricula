using System;
using System.Collections.Generic;
using System.Linq;
using SistemaMatricula.Interfaces;
using SistemaMatricula.Modelos;

namespace SistemaMatricula.Servicios
{
    public class MatriculaService
    {
        private readonly List<Curso> cursosDisponibles = new List<Curso>
        {
            new Curso("MAT101", "Matemática I", 4, 30),
            new Curso("SIS102", "Algoritmos y Estructuras de Datos", 5, 25),
            new Curso("FIS103", "Física I", 4, 20),
            new Curso("BD201", "Bases de Datos Avanzadas", 4, 10)
        };

        // Uso de Diccionarios para llevar control de matrículas por facultad/carrera
        private readonly Dictionary<string, List<string>> historialMatriculasPorCarrera = new Dictionary<string, List<string>>();

        public IEnumerable<Curso> ObtenerCursos()
        {
            return cursosDisponibles;
        }

        // Uso de consultas LINQ avanzadas
        public IEnumerable<Curso> FiltrarCursosPorMinimoVacantes(int vacantesMinimas)
        {
            return cursosDisponibles.Where(c => c.Vacantes >= vacantesMinimas);
        }

        public Dictionary<string, List<string>> ObtenerHistorialMatriculas()
        {
            return historialMatriculasPorCarrera;
        }

        public void RealizarMatricula(Estudiante estudiante, string codigoCurso, IPagoMatricula metodoPago)
        {
            Curso cursoSeleccionado = cursosDisponibles.FirstOrDefault(c => c.Codigo.Equals(codigoCurso, StringComparison.OrdinalIgnoreCase));

            if (cursoSeleccionado == null)
            {
                throw new KeyNotFoundException($"El código de curso '{codigoCurso}' no existe en el sistema.");
            }

            if (cursoSeleccionado.Vacantes <= 0)
            {
                throw new InvalidOperationException($"El curso '{cursoSeleccionado.Nombre}' ya no cuenta con vacantes disponibles.");
            }

            // Cálculo y procesamiento del pago
            decimal costoCredito = 180.0m;
            decimal totalPagar = cursoSeleccionado.Creditos * costoCredito;
            metodoPago.ProcesarPago(totalPagar);

            // Actualización de estado (Descuenta la vacante)
            cursoSeleccionado.Vacantes--;

            // Registro en diccionario incluyendo alumno, curso y monto pagado
            if (!historialMatriculasPorCarrera.ContainsKey(estudiante.Carrera))
            {
                historialMatriculasPorCarrera[estudiante.Carrera] = new List<string>();
            }

            string detalle = $"Alumno: {estudiante.Nombre} | Curso: {cursoSeleccionado.Nombre} | Monto Pagado: S/ {totalPagar:F2} | Método: {metodoPago.Metodo}";
            historialMatriculasPorCarrera[estudiante.Carrera].Add(detalle);
        }
    }
}