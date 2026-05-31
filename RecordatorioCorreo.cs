using System;

namespace GestionCitasMedicas
{
    // Clase que implementa la interfaz IRecordatorio.
    // Simula el envio de un correo electronico imprimiendo la informacion en consola.
    public class RecordatorioCorreo : IRecordatorio
    {
        public void Enviar(CitaMedica cita)
        {
            Console.WriteLine("\n--- SIMULACION DE ENVIO DE CORREO ---");
            Console.WriteLine($"Para: {cita.Paciente.Correo}");
            Console.WriteLine($"Asunto: Recordatorio de Cita Medica - ID {cita.Id}");
            Console.WriteLine($"Hola {cita.Paciente.Nombre}, te recordamos tu cita con el Dr. {cita.Medico.Nombre}");
            Console.WriteLine($"Fecha y Hora: {cita.FechaHora:dd/MM/yyyy hh:mm tt}");
            Console.WriteLine("--------------------------------------\n");
        }
    }
}
