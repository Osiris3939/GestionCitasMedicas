using System;

namespace GestionCitasMedicas
{
    // Esta clase representa a un paciente de la clinica.
    // Se guardan los datos basicos de contacto para agendar la cita.
    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

        public Paciente(int id, string nombre, string telefono, string correo)
        {
            Id = id;
            Nombre = nombre;
            Telefono = telefono;
            Correo = correo;
        }

        // Muestra los datos del paciente en consola
        public void MostrarInformacion()
        {
            Console.WriteLine($"ID Paciente: {Id}");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Telefono: {Telefono}");
            Console.WriteLine($"Correo: {Correo}");
        }
    }
}
