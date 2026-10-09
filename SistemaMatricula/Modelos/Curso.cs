namespace SistemaMatricula.Modelos
{
    public class Curso
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Creditos { get; set; }
        public int Vacantes { get; set; }

        public Curso(string codigo, string nombre, int creditos, int vacantes)
        {
            Codigo = codigo;
            Nombre = nombre;
            Creditos = creditos;
            Vacantes = vacantes;
        }
    }
}