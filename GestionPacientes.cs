using System;
using System.Collections.Generic;

namespace GestionCitasMedicas
{
    // Esta clase se encarga unicamente de la gestion de los pacientes en memoria.
    // Cumple con el principio de responsabilidad unica (SRP).
    public class GestionPacientes
    {
        private readonly List<Paciente> listaPacientes = new List<Paciente>();

        // Registra un nuevo paciente validando que el ID no se repita
        public bool RegistrarPaciente(Paciente paciente)
        {
            if (paciente == null) return false;

            Paciente existente = BuscarPacientePorId(paciente.Id);
            if (existente != null)
            {
                Console.WriteLine("\n[Error] Ya existe un paciente con ese ID.");
                return false;
            }

            listaPacientes.Add(paciente);
            return true;
        }

        // Busca un paciente en la lista por su ID
        public Paciente BuscarPacientePorId(int id)
        {
            foreach (Paciente p in listaPacientes)
            {
                if (p.Id == id)
                {
                    return p;
                }
            }
            return null;
        }

        // Muestra todos los pacientes registrados
        public void ListarPacientes()
        {
            if (listaPacientes.Count == 0)
            {
                Console.WriteLine("\nNo hay pacientes registrados en el sistema.");
                return;
            }

            Console.WriteLine("\n=== Lista de Pacientes ===");
            foreach (Paciente p in listaPacientes)
            {
                p.MostrarInformacion();
                Console.WriteLine("-----------------------");
            }
        }
    }
}
