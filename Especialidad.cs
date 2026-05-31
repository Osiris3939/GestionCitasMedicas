using System;

namespace GestionCitasMedicas
{
    // Esta clase representa las especialidades medicas disponibles en la clinica.
    public class Especialidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Especialidad(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        // Muestra la informacion de la especialidad
        public void MostrarInformacion()
        {
            Console.WriteLine($"ID Especialidad: {Id} - Nombre: {Nombre}");
        }
    }
}
