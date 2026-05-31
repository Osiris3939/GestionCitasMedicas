using System;
using System.Collections.Generic;

namespace GestionCitasMedicas
{
    // Esta clase gestiona la logica de negocio de las citas medicas.
    // Recibe la interfaz IRecordatorio por constructor para cumplir con el principio DIP (Inversion de Dependencias).
    // Cumple con el principio de responsabilidad unica (SRP).
    public class GestionCitas
    {
        private readonly List<CitaMedica> listaCitas = new List<CitaMedica>();
        private readonly IRecordatorio recordatorio;

        public GestionCitas(IRecordatorio recordatorio)
        {
            this.recordatorio = recordatorio;
        }

        // Verifica si el medico tiene alguna cita activa (Agendada) a la misma hora
        private bool MedicoTieneConflicto(int medicoId, DateTime fechaHora, int? citaIdIgnorar = null)
        {
            foreach (CitaMedica c in listaCitas)
            {
                if (c.Estado == EstadoCita.Agendada && c.Medico.Id == medicoId && c.FechaHora == fechaHora)
                {
                    if (citaIdIgnorar.HasValue && c.Id == citaIdIgnorar.Value)
                    {
                        continue;
                    }
                    return true;
                }
            }
            return false;
        }

        // Busca una cita por su ID
        public CitaMedica BuscarCitaPorId(int id)
        {
            foreach (CitaMedica c in listaCitas)
            {
                if (c.Id == id)
                {
                    return c;
                }
            }
            return null;
        }

        // Agenda una nueva cita medica tras realizar las validaciones correspondientes
        public bool AgendarCita(CitaMedica cita)
        {
            if (cita == null) return false;

            // Validar que el ID de cita no este duplicado
            if (BuscarCitaPorId(cita.Id) != null)
            {
                Console.WriteLine("\n[Error] Ya existe una cita registrada con ese ID.");
                return false;
            }

            // Validar que el medico no tenga conflicto de horario
            if (MedicoTieneConflicto(cita.Medico.Id, cita.FechaHora))
            {
                Console.WriteLine($"\n[Error] El medico {cita.Medico.Nombre} ya tiene una cita agendada para el {cita.FechaHora:dd/MM/yyyy hh:mm tt}.");
                return false;
            }

            listaCitas.Add(cita);
            Console.WriteLine("\nCita agendada exitosamente.");
            
            // Envío del recordatorio simulado
            recordatorio?.Enviar(cita);
            return true;
        }

        // Cancela una cita registrada por su ID
        public bool CancelarCita(int id)
        {
            CitaMedica cita = BuscarCitaPorId(id);
            if (cita == null)
            {
                Console.WriteLine("\n[Error] No se encontro la cita especificada.");
                return false;
            }

            if (cita.Estado == EstadoCita.Cancelada)
            {
                Console.WriteLine("\n[Error] La cita ya se encuentra cancelada.");
                return false;
            }

            cita.Cancelar();
            Console.WriteLine("\nCita cancelada exitosamente.");
            return true;
        }

        // Reprograma la fecha y hora de una cita activa validando la disponibilidad del medico
        public bool ReprogramarCita(int id, DateTime nuevaFecha)
        {
            CitaMedica cita = BuscarCitaPorId(id);
            if (cita == null)
            {
                Console.WriteLine("\n[Error] No se encontro la cita especificada.");
                return false;
            }

            // Validar disponibilidad del medico en el nuevo horario (ignorando la cita actual en la busqueda de conflicto)
            if (MedicoTieneConflicto(cita.Medico.Id, nuevaFecha, cita.Id))
            {
                Console.WriteLine($"\n[Error] El medico {cita.Medico.Nombre} ya tiene otra cita agendada para el {nuevaFecha:dd/MM/yyyy hh:mm tt}.");
                return false;
            }

            cita.Reprogramar(nuevaFecha);
            Console.WriteLine("\nCita reprogramada exitosamente.");

            // Envío del recordatorio tras la reprogramacion
            recordatorio?.Enviar(cita);
            return true;
        }

        // Listar todas las citas registradas en el sistema
        public void ListarCitas()
        {
            if (listaCitas.Count == 0)
            {
                Console.WriteLine("\nNo hay citas registradas en el sistema.");
                return;
            }

            Console.WriteLine("\n=== Lista General de Citas ===");
            foreach (CitaMedica c in listaCitas)
            {
                c.MostrarInformacion();
                Console.WriteLine("-----------------------");
            }
        }

        // Filtra y muestra las citas de un paciente especifico
        public void ListarCitasPorPaciente(int pacienteId)
        {
            bool encontro = false;
            Console.WriteLine($"\n=== Citas del Paciente (ID: {pacienteId}) ===");
            foreach (CitaMedica c in listaCitas)
            {
                if (c.Paciente.Id == pacienteId)
                {
                    c.MostrarInformacion();
                    Console.WriteLine("-----------------------");
                    encontro = true;
                }
            }

            if (!encontro)
            {
                Console.WriteLine("No se encontraron citas para este paciente.");
            }
        }

        // Filtra y muestra las citas de un medico especifico
        public void ListarCitasPorMedico(int medicoId)
        {
            bool encontro = false;
            Console.WriteLine($"\n=== Citas del Medico (ID: {medicoId}) ===");
            foreach (CitaMedica c in listaCitas)
            {
                if (c.Medico.Id == medicoId)
                {
                    c.MostrarInformacion();
                    Console.WriteLine("-----------------------");
                    encontro = true;
                }
            }

            if (!encontro)
            {
                Console.WriteLine("No se encontraron citas para este medico.");
            }
        }
    }
}
