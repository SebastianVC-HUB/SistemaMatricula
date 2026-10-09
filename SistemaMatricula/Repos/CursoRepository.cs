using System.Collections.Generic;
using System.Linq;

namespace SistemaMatricula.Repos
{
    public class CursoRepository
    {
        // Uso de Listas y Diccionarios requeridos
        private List<string> listaCursosDisponibles = new List<string> { "Matemática I", "Algoritmos", "Física I", "Base de Datos" };
        private Dictionary<string, int> diccionarioCupos = new Dictionary<string, int>()
        {
            { "Matemática I", 30 },
            { "Algoritmos", 25 },
            { "Física I", 20 },
            { "Base de Datos", 35 }
        };

        public List<string> ObtenerTodosLosCursos()
        {
            return listaCursosDisponibles;
        }

        // Uso de LINQ para buscar cursos con cupos disponibles
        public IEnumerable<string> BuscarCursosConCupos()
        {
            return diccionarioCupos.Where(c => c.Value > 10).Select(c => c.Key);
        }

        public void ActualizarCupo(string curso)
        {
            if (!diccionarioCupos.ContainsKey(curso))
            {
                throw new KeyNotFoundException($"El curso '{curso}' no existe en el sistema.");
            }

            if (diccionarioCupos[curso] <= 0)
            {
                throw new System.Exception($"El curso '{curso}' ya no cuenta con vacantes disponibles.");
            }

            diccionarioCupos[curso]--;
        }
    }
}