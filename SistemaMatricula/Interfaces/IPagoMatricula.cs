namespace SistemaMatricula.Interfaces
{
    public interface IPagoMatricula
    {
        string Metodo { get; }
        bool ProcesarPago(decimal monto);
    }
}