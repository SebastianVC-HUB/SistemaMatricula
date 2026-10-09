namespace SistemaMatricula.Modelos
{
    public abstract class PersonaBase
    {
        // Variables globales requeridas: const, static, readonly
        public const string Institucion = "Universidad Nacional";
        public static int ContadorPersonas = 0;
        protected readonly string codigoInstitucional;

        protected string nombreCompleto;
        protected int edad;

        public PersonaBase(string nombreCompleto, int edad)
        {
            this.nombreCompleto = nombreCompleto;
            this.edad = edad;
            ContadorPersonas++;
            this.codigoInstitucional = $"UNI-{ContadorPersonas:D3}";
        }

        public abstract void MostrarDatos();
    }
}