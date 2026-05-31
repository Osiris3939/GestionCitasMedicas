# Sistema de Gestion de Citas Medicas

Este proyecto es una practica academica para la asignatura de Programacion 2. Implementa un sistema de consola en C# sobre .NET 8 que permite gestionar pacientes, medicos, especialidades y agendar citas medicas en memoria de forma simple y modular.

## Informacion del Estudiante
- **Nombre:** Franklyn Enmanuel Santana Rodriguez
- **Matricula:** 2025 2089
- **Practica:** Programacion 2

## Caracteristicas del Proyecto
- Administracion en memoria (usando listas) de Pacientes, Medicos, Especialidades y Citas.
- Control de conflictos en la agenda de medicos (no se permiten citas en el mismo horario para el mismo medico).
- Uso del principio DIP (Inversion de Dependencias) para desacoplar el envio de recordatorios de la logica central de citas.
- Modulos de validacion robusta por consola para asegurar que las entradas de texto, numeros y fechas sean correctas.
- Proyecto precargado con datos iniciales (pacientes, medicos, especialidades y una cita agendada) para facilitar su evaluacion.

## Estructura de Archivos

- **Paciente.cs**: Clase modelo para la entidad Paciente.
- **Medico.cs**: Clase modelo para la entidad Medico.
- **Especialidad.cs**: Clase modelo para la entidad Especialidad.
- **CitaMedica.cs**: Clase modelo para la cita, manejando fecha, hora y estado (Agendada/Cancelada).
- **IRecordatorio.cs**: Interfaz para el envio de recordatorios.
- **RecordatorioCorreo.cs**: Implementacion concreta que simula el envio de un correo electronico por consola.
- **Validador.cs**: Funciones auxiliares estaticas de validacion de datos de usuario.
- **GestionPacientes.cs**: Gestor de la coleccion de pacientes en memoria.
- **GestionMedicos.cs**: Gestor de la coleccion de medicos y la asignacion de especialidades.
- **GestionEspecialidades.cs**: Gestor de la coleccion de especialidades.
- **GestionCitas.cs**: Gestor de la logica de agendamiento, conflicto de horarios y cancelacion de citas.
- **Program.cs**: Punto de entrada del programa y menu de opciones de consola.
- **RESPUESTAS.md**: Documento de analisis y explicacion tecnica del uso de principios de diseño (SOLID, KISS, SoC, DRY, YAGNI).

## Como Compilar y Ejecutar

### Requisitos Previos
- Tener instalado el SDK de .NET 8.0 en el equipo.

### Compilar el Proyecto
Abra una terminal o consola de comandos en la carpeta raiz del proyecto (`GestionCitasMedicas/`) y ejecute:
```bash
dotnet build
```

### Ejecutar el Proyecto
Para correr la aplicacion de consola interactiva, ejecute:
```bash
dotnet run
```
El sistema se iniciara con datos pre-cargados de prueba que permiten agendar, cancelar, reprogramar o consultar citas inmediatamente.
