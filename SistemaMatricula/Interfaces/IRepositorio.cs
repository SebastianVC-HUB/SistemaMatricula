using System.Collections.Generic;

namespace SistemaMatricula.Interfaces
{
    public interface IRepositorio<T>
    {
        void Agregar(T entidad);
        IEnumerable<T> ObtenerTodos();
    }
}