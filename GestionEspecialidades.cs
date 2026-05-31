using System;
using System.Collections.Generic;

namespace GestionCitasMedicas
{
    // Esta clase se encarga unicamente de la gestion de especialidades medicas en memoria.
    // Cumple con el principio de responsabilidad unica (SRP).
    public class GestionEspecialidades
    {
        private readonly List<Especialidad> listaEspecialidades = new List<Especialidad>();

        // Registra una nueva especialidad validando que el ID no se repita
        public bool RegistrarEspecialidad(Especialidad especialidad)
        {
            if (especialidad == null) return false;

            Especialidad existente = BuscarEspecialidadPorId(especialidad.Id);
            if (existente != null)
            {
                Console.WriteLine("\n[Error] Ya existe una especialidad con ese ID.");
                return false;
            }

            listaEspecialidades.Add(especialidad);
            return true;
        }

        // Busca una especialidad por su ID
        public Especialidad BuscarEspecialidadPorId(int id)
        {
            foreach (Especialidad e in listaEspecialidades)
            {
                if (e.Id == id)
                {
                    return e;
                }
            }
            return null;
        }

        // Muestra todas las especialidades registradas
        public void ListarEspecialidades()
        {
            if (listaEspecialidades.Count == 0)
            {
                Console.WriteLine("\nNo hay especialidades registradas en el sistema.");
                return;
            }

            Console.WriteLine("\n=== Lista de Especialidades ===");
            foreach (Especialidad e in listaEspecialidades)
            {
                e.MostrarInformacion();
            }
            Console.WriteLine("-----------------------");
        }
    }
}
