using System;

namespace GestionCitasMedicas
{
    // Esta clase representa a un medico de la clinica.
    // Un medico puede ser registrado sin especialidad y asignarsele una mas adelante.
    public class Medico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Especialidad Especialidad { get; set; }

        public Medico(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            Especialidad = null; // Inicialmente no tiene especialidad asignada
        }

        // Asigna una especialidad medica al medico
        public void AsignarEspecialidad(Especialidad especialidad)
        {
            Especialidad = especialidad;
        }

        // Muestra la informacion del medico
        public void MostrarInformacion()
        {
            Console.WriteLine($"ID Medico: {Id}");
            Console.WriteLine($"Nombre: {Nombre}");
            if (Especialidad != null)
            {
                Console.WriteLine($"Especialidad: {Especialidad.Nombre}");
            }
            else
            {
                Console.WriteLine("Especialidad: Ninguna asignada");
            }
        }
    }
}
