using System;
using SistemaMatricula.Interfaces;

namespace SistemaMatricula.Pagos
{
    public class PagoTarjeta : IPagoMatricula
    {
        public string Metodo => "Tarjeta de Crédito / Débito";

        public bool ProcesarPago(decimal monto)
        {
            Console.WriteLine($"Conectando con pasarela segura... Cobro exitoso de S/ {monto:F2} con Tarjeta. [Aprobado]");
            return true;
        }
    }
}