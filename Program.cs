// Franklyn Enmanuel Santana Rodriguez
// Matrícula: 2025 2089
// Práctica de Programación 2 - Sistema de Gestión de Citas Médicas

using System;

namespace GestionCitasMedicas
{
    class Program
    {
        static void Main(string[] args)
        {
            // Inicializacion de manejadores de datos
            GestionPacientes gestorPacientes = new GestionPacientes();
            GestionMedicos gestorMedicos = new GestionMedicos();
            GestionEspecialidades gestorEspecialidades = new GestionEspecialidades();
            
            // Demostracion del principio de inversion de dependencias (DIP)
            IRecordatorio recordatorio = new RecordatorioCorreo();
            GestionCitas gestorCitas = new GestionCitas(recordatorio);

            // Preguntar si se desean cargar datos de prueba iniciales para evaluar la practica
            Console.Write("¿Desea cargar los datos de prueba iniciales para la evaluacion? (S/N): ");
            string respuesta = Console.ReadLine()?.Trim().ToUpper();
            if (respuesta == "S" || respuesta == "SI")
            {
                CargarDatosIniciales(gestorPacientes, gestorMedicos, gestorEspecialidades, gestorCitas);
                Console.WriteLine("\nDatos de prueba cargados exitosamente.");
            }
            else
            {
                Console.WriteLine("\nEl sistema iniciara con las listas vacias.");
            }

            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\n==================================================");
                Console.WriteLine("       SISTEMA DE GESTION DE CITAS MEDICAS        ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Registrar Paciente");
                Console.WriteLine("2. Registrar Medico");
                Console.WriteLine("3. Registrar Especialidad");
                Console.WriteLine("4. Asignar Especialidad a Medico");
                Console.WriteLine("5. Agendar Cita Medica");
                Console.WriteLine("6. Cancelar Cita Medica");
                Console.WriteLine("7. Reprogramar Cita Medica");
                Console.WriteLine("8. Listar Pacientes");
                Console.WriteLine("9. Listar Medicos");
                Console.WriteLine("10. Listar Especialidades");
                Console.WriteLine("11. Consultar Citas Medicas");
                Console.WriteLine("12. Salir del Sistema");
                Console.WriteLine("==================================================");
                
                int opcion = Validador.LeerEnteroPositivo("Seleccione una opcion (1-12): ");

                switch (opcion)
                {
                    case 1:
                        RegistrarPaciente(gestorPacientes);
                        break;
                    case 2:
                        RegistrarMedico(gestorMedicos);
                        break;
                    case 3:
                        RegistrarEspecialidad(gestorEspecialidades);
                        break;
                    case 4:
                        AsignarEspecialidad(gestorMedicos, gestorEspecialidades);
                        break;
                    case 5:
                        AgendarCita(gestorCitas, gestorPacientes, gestorMedicos);
                        break;
                    case 6:
                        CancelarCita(gestorCitas);
                        break;
                    case 7:
                        ReprogramarCita(gestorCitas);
                        break;
                    case 8:
                        gestorPacientes.ListarPacientes();
                        break;
                    case 9:
                        gestorMedicos.ListarMedicos();
                        break;
                    case 10:
                        gestorEspecialidades.ListarEspecialidades();
                        break;
                    case 11:
                        ConsultarCitas(gestorCitas);
                        break;
                    case 12:
                        Console.WriteLine("\nSaliendo del sistema. Gracias por usar la aplicacion.");
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("\n[Error] Opcion invalida. Intente entre 1 y 12.");
                        break;
                }
            }
        }

        // Metodo para cargar datos iniciales en memoria
        private static void CargarDatosIniciales(
            GestionPacientes gp, 
            GestionMedicos gm, 
            GestionEspecialidades ge, 
            GestionCitas gc)
        {
            // Especialidades
            Especialidad pediatria = new Especialidad(1, "Pediatria");
            Especialidad cardiologia = new Especialidad(2, "Cardiologia");
            ge.RegistrarEspecialidad(pediatria);
            ge.RegistrarEspecialidad(cardiologia);

            // Medicos
            Medico medico1 = new Medico(1, "Dr. Franklyn Santana");
            medico1.AsignarEspecialidad(pediatria);
            Medico medico2 = new Medico(2, "Dra. Maria Gomez");
            medico2.AsignarEspecialidad(cardiologia);
            gm.RegistrarMedico(medico1);
            gm.RegistrarMedico(medico2);

            // Pacientes
            Paciente paciente1 = new Paciente(1, "Eva Santana", "809-555-0199", "eva.santana@email.com");
            Paciente paciente2 = new Paciente(2, "Ana Ruiz", "829-555-0244", "ana.ruiz@email.com");
            gp.RegistrarPaciente(paciente1);
            gp.RegistrarPaciente(paciente2);

            // Cita inicial agendada (ejemplo de control)
            DateTime fechaCita = DateTime.Today.AddDays(1).AddHours(10); // Mañana a las 10:00 AM
            CitaMedica cita1 = new CitaMedica(1, paciente1, medico1, fechaCita);
            gc.AgendarCita(cita1);
        }

        private static void RegistrarPaciente(GestionPacientes gp)
        {
            Console.WriteLine("\n--- Registro de Nuevo Paciente ---");
            int id = Validador.LeerEnteroPositivo("Ingrese el ID del paciente: ");
            string nombre = Validador.LeerTextoObligatorio("Ingrese el nombre completo: ");
            string telefono = Validador.LeerTextoObligatorio("Ingrese el telefono de contacto: ");
            string correo = Validador.LeerTextoObligatorio("Ingrese el correo electronico: ");

            Paciente p = new Paciente(id, nombre, telefono, correo);
            if (gp.RegistrarPaciente(p))
            {
                Console.WriteLine("\nPaciente registrado exitosamente.");
            }
        }

        private static void RegistrarMedico(GestionMedicos gm)
        {
            Console.WriteLine("\n--- Registro de Nuevo Medico ---");
            int id = Validador.LeerEnteroPositivo("Ingrese el ID del medico: ");
            string nombre = Validador.LeerTextoObligatorio("Ingrese el nombre completo: ");

            Medico m = new Medico(id, nombre);
            if (gm.RegistrarMedico(m))
            {
                Console.WriteLine("\nMedico registrado exitosamente sin especialidad.");
            }
        }

        private static void RegistrarEspecialidad(GestionEspecialidades ge)
        {
            Console.WriteLine("\n--- Registro de Especialidad Medica ---");
            int id = Validador.LeerEnteroPositivo("Ingrese el ID de la especialidad: ");
            string nombre = Validador.LeerTextoObligatorio("Ingrese el nombre de la especialidad: ");

            Especialidad e = new Especialidad(id, nombre);
            if (ge.RegistrarEspecialidad(e))
            {
                Console.WriteLine("\nEspecialidad registrada exitosamente.");
            }
        }

        private static void AsignarEspecialidad(GestionMedicos gm, GestionEspecialidades ge)
        {
            Console.WriteLine("\n--- Asignar Especialidad a Medico ---");
            int medicoId = Validador.LeerEnteroPositivo("Ingrese el ID del medico: ");
            Medico medico = gm.BuscarMedicoPorId(medicoId);
            if (medico == null)
            {
                Console.WriteLine("\n[Error] Medico no encontrado.");
                return;
            }

            int especialidadId = Validador.LeerEnteroPositivo("Ingrese el ID de la especialidad: ");
            Especialidad esp = ge.BuscarEspecialidadPorId(especialidadId);
            if (esp == null)
            {
                Console.WriteLine("\n[Error] Especialidad no encontrada.");
                return;
            }

            if (gm.AsignarEspecialidad(medicoId, esp))
            {
                Console.WriteLine($"\nEspecialidad '{esp.Nombre}' asignada exitosamente al Dr. {medico.Nombre}.");
            }
        }

        private static void AgendarCita(GestionCitas gc, GestionPacientes gp, GestionMedicos gm)
        {
            Console.WriteLine("\n--- Agendar Cita Medica ---");
            int citaId = Validador.LeerEnteroPositivo("Ingrese el ID unico de la cita: ");
            
            int pacienteId = Validador.LeerEnteroPositivo("Ingrese el ID del paciente: ");
            Paciente paciente = gp.BuscarPacientePorId(pacienteId);
            if (paciente == null)
            {
                Console.WriteLine("\n[Error] Paciente no encontrado en el sistema.");
                return;
            }

            int medicoId = Validador.LeerEnteroPositivo("Ingrese el ID del medico: ");
            Medico medico = gm.BuscarMedicoPorId(medicoId);
            if (medico == null)
            {
                Console.WriteLine("\n[Error] Medico no encontrado en el sistema.");
                return;
            }

            DateTime fechaHora = Validador.LeerFecha("Ingrese la fecha y hora de la cita (Ej: 31/05/2026 10:00 AM): ");

            CitaMedica cita = new CitaMedica(citaId, paciente, medico, fechaHora);
            gc.AgendarCita(cita);
        }

        private static void CancelarCita(GestionCitas gc)
        {
            Console.WriteLine("\n--- Cancelar Cita Medica ---");
            int citaId = Validador.LeerEnteroPositivo("Ingrese el ID de la cita a cancelar: ");
            gc.CancelarCita(citaId);
        }

        private static void ReprogramarCita(GestionCitas gc)
        {
            Console.WriteLine("\n--- Reprogramar Cita Medica ---");
            int citaId = Validador.LeerEnteroPositivo("Ingrese el ID de la cita a reprogramar: ");
            CitaMedica cita = gc.BuscarCitaPorId(citaId);
            if (cita == null)
            {
                Console.WriteLine("\n[Error] Cita no encontrada.");
                return;
            }

            DateTime nuevaFecha = Validador.LeerFecha("Ingrese la nueva fecha y hora (Ej: 31/05/2026 11:30 AM): ");
            gc.ReprogramarCita(citaId, nuevaFecha);
        }

        private static void ConsultarCitas(GestionCitas gc)
        {
            Console.WriteLine("\n--- Consulta de Citas ---");
            Console.WriteLine("1. Mostrar todas las citas (General)");
            Console.WriteLine("2. Filtrar por Paciente");
            Console.WriteLine("3. Filtrar por Medico");
            int filtro = Validador.LeerEnteroPositivo("Seleccione opcion de consulta: ");

            if (filtro == 1)
            {
                gc.ListarCitas();
            }
            else if (filtro == 2)
            {
                int pacienteId = Validador.LeerEnteroPositivo("Ingrese el ID del paciente: ");
                gc.ListarCitasPorPaciente(pacienteId);
            }
            else if (filtro == 3)
            {
                int medicoId = Validador.LeerEnteroPositivo("Ingrese el ID del medico: ");
                gc.ListarCitasPorMedico(medicoId);
            }
            else
            {
                Console.WriteLine("\n[Error] Opcion de busqueda no valida.");
            }
        }
    }
}
