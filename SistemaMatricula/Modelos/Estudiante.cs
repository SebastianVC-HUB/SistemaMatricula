using System;

namespace SistemaMatricula.Modelos
{
    public class Estudiante : PersonaBase
    {
        public string Carrera { get; set; }
        public double PromedioPonderado { get; set; }

        // Propiedad pública para acceder al nombre protegido
        public string Nombre => nombreCompleto;

        public Estudiante(string nombreCompleto, int edad, string carrera, double promedio)
            : base(nombreCompleto, edad)
        {
            if (promedio < 0 || promedio > 20)
                throw new ArgumentOutOfRangeException("El promedio ponderado debe estar estrictamente entre 0 y 20.");

            Carrera = carrera;
            PromedioPonderado = promedio;
        }

        public override void MostrarDatos()
        {
            Console.WriteLine($"[{codigoInstitucional}] Estudiante: {nombreCompleto} | Edad: {edad} | Carrera: {Carrera} | Promedio: {PromedioPonderado}");
        }
    }
}