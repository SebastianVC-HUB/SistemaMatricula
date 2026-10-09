using System.Collections.Generic;
using SistemaMatricula.Interfaces;

namespace SistemaMatricula.Repos
{
    public class BaseRepository<T> : IRepositorio<T> where T : class
    {
        protected readonly List<T> elementos = new List<T>();

        public virtual void Agregar(T entidad)
        {
            elementos.Add(entidad);
        }

        public virtual IEnumerable<T> ObtenerTodos()
        {
            return elementos;
        }
    }
}