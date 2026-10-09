using System;
using SistemaMatricula.Interfaces;

namespace SistemaMatricula.Pagos
{
    public class PagoEfectivo : IPagoMatricula
    {
        public string Metodo => "Efectivo (Caja Universitaria)";

        public bool ProcesarPago(decimal monto)
        {
            Console.WriteLine($"Procesando pago en efectivo por S/ {monto:F2}... [Comprobante Registrado]");
            return true;
        }
    }
}