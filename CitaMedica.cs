using System;

namespace GestionCitasMedicas
{
    // Define los estados posibles para una cita medica
    public enum EstadoCita
    {
        Agendada,
        Cancelada
    }

    // Esta clase maneja una cita medica individual entre un paciente y un medico.
    // Permite cambiar su estado o modificar la fecha y hora asignadas.
    public class CitaMedica
    {
        public int Id { get; set; }
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public DateTime FechaHora { get; set; }
        public EstadoCita Estado { get; set; }

        public CitaMedica(int id, Paciente paciente, Medico medico, DateTime fechaHora)
        {
            Id = id;
            Paciente = paciente;
            Medico = medico;
            FechaHora = fechaHora;
            Estado = EstadoCita.Agendada; // Por defecto inicia como Agendada
        }

        // Cambia el estado de la cita a Cancelada
        public void Cancelar()
        {
            Estado = EstadoCita.Cancelada;
        }

        // Cambia la fecha de la cita y se asegura de que vuelva a estar Agendada
        public void Reprogramar(DateTime nuevaFecha)
        {
            FechaHora = nuevaFecha;
            Estado = EstadoCita.Agendada;
        }

        // Muestra la informacion detallada de la cita
        public void MostrarInformacion()
        {
            Console.WriteLine($"ID Cita: {Id}");
            Console.WriteLine($"Fecha y Hora: {FechaHora:dd/MM/yyyy hh:mm tt}");
            Console.WriteLine($"Estado: {Estado}");
            Console.WriteLine($"Paciente: {Paciente.Nombre} (ID: {Paciente.Id})");
            Console.WriteLine($"Medico: {Medico.Nombre} (ID: {Medico.Id})");
        }
    }
}
