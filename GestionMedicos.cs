using System;
using System.Collections.Generic;

namespace GestionCitasMedicas
{
    // Esta clase maneja unicamente la coleccion de medicos en memoria y sus asignaciones.
    // Cumple con el principio de responsabilidad unica (SRP).
    public class GestionMedicos
    {
        private readonly List<Medico> listaMedicos = new List<Medico>();

        // Registra un nuevo medico validando que el ID no este duplicado
        public bool RegistrarMedico(Medico medico)
        {
            if (medico == null) return false;

            Medico existente = BuscarMedicoPorId(medico.Id);
            if (existente != null)
            {
                Console.WriteLine("\n[Error] Ya existe un medico registrado con ese ID.");
                return false;
            }

            listaMedicos.Add(medico);
            return true;
        }

        // Busca un medico en la lista usando su ID
        public Medico BuscarMedicoPorId(int id)
        {
            foreach (Medico m in listaMedicos)
            {
                if (m.Id == id)
                {
                    return m;
                }
            }
            return null;
        }

        // Asigna una especialidad a un medico
        public bool AsignarEspecialidad(int medicoId, Especialidad especialidad)
        {
            Medico medico = BuscarMedicoPorId(medicoId);
            if (medico == null)
            {
                Console.WriteLine("\n[Error] No se encontro el medico.");
                return false;
            }

            if (especialidad == null)
            {
                Console.WriteLine("\n[Error] La especialidad no es valida.");
                return false;
            }

            medico.AsignarEspecialidad(especialidad);
            return true;
        }

        // Muestra todos los medicos registrados en consola
        public void ListarMedicos()
        {
            if (listaMedicos.Count == 0)
            {
                Console.WriteLine("\nNo hay medicos registrados en el sistema.");
                return;
            }

            Console.WriteLine("\n=== Lista de Medicos ===");
            foreach (Medico m in listaMedicos)
            {
                m.MostrarInformacion();
                Console.WriteLine("---------------------");
            }
        }
    }
}
