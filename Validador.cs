using System;

namespace GestionCitasMedicas
{
    // Clase estatica para centralizar la captura y validacion de datos por consola.
    // Con esta clase aplicamos el principio DRY para no repetir el mismo codigo de validacion.
    public static class Validador
    {
        // Pide un texto en consola y no permite que este vacio
        public static string LeerTextoObligatorio(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(entrada))
                {
                    return entrada;
                }
                Console.WriteLine("[Error] El campo no puede quedar vacio. Intente de nuevo.");
            }
        }

        // Pide un numero entero y valida que no sea negativo
        public static int LeerEnteroPositivo(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine()?.Trim();
                if (int.TryParse(entrada, out int numero))
                {
                    if (numero >= 0)
                    {
                        return numero;
                    }
                    Console.WriteLine("[Error] El numero no puede ser negativo.");
                }
                else
                {
                    Console.WriteLine("[Error] Por favor, ingrese un numero entero valido.");
                }
            }
        }

        // Pide una fecha y hora y valida que tenga el formato correcto
        public static DateTime LeerFecha(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine()?.Trim();
                if (DateTime.TryParse(entrada, out DateTime fecha))
                {
                    return fecha;
                }
                Console.WriteLine("[Error] Formato de fecha invalido. Ejemplo correcto: dd/MM/yyyy hh:mm tt o yyyy-MM-dd HH:mm");
            }
        }
    }
}
