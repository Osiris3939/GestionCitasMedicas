# Practica de Programacion 2 - Respuestas Academicas

**Estudiante:** Franklyn Enmanuel Santana Rodriguez  
**Matricula:** 2025 2089  
**Asignatura:** Programacion 2  
**Proyecto:** Sistema de Gestion de Citas Medicas  

---

## 1. Analisis del Problema y Enfoque de Desarrollo

El objetivo principal de esta practica era desarrollar un sistema de gestion de citas medicas de consola de forma simple y modular utilizando C# y .NET 8. El alcance exigia la administracion en memoria de pacientes, medicos, especialidades y citas, con reglas de negocio basicas como evitar la duplicidad de IDs y prevenir conflictos de agenda para un mismo medico.

Para cumplir con las directrices de la practica, el desarrollo se enfoco estrictamente en los requerimientos explicitos, descartando modulos adicionales como facturacion, recetas medicas o historial clinico (respetando YAGNI y KISS).

---

## 2. Principios de Diseño de Software Aplicados

### Separation of Concerns (SoC) - Separacion de Responsabilidades
El codigo no se estructuro en un unico archivo monolito. En su lugar, se dividio en componentes aislados de acuerdo con su proposito:
- **Modelos de datos (`Paciente`, `Medico`, `Especialidad`, `CitaMedica`)**: Representan las entidades del negocio y sus atributos basicos.
- **Manejadores o Gestores (`GestionPacientes`, `GestionMedicos`, `GestionEspecialidades`, `GestionCitas`)**: Implementan la logica de almacenamiento y manipulacion de datos en memoria para cada entidad.
- **Validacion (`Validador`)**: Centraliza la lectura y verificacion de las entradas desde la consola.
- **Interfaz de Usuario (`Program`)**: Controla el ciclo del menu y coordina las llamadas a los diferentes gestores.

### DRY (Don't Repeat Yourself) - No te repitas
La captura de datos por consola suele generar duplicacion de codigo debido a los ciclos de reintento cuando el usuario introduce un valor incorrecto. Para evitar esto, se creo la clase estatica `Validador`. Los metodos para leer texto obligatorio, enteros positivos o fechas validas se escribieron una sola vez y se invocan en todo el programa.

### KISS (Keep It Simple, Stupid) y YAGNI (You Aren't Gonna Need It)
- Se evito el uso de bases de datos, APIs o archivos locales (como JSON o CSV), gestionando toda la informacion en listas genericas en memoria.
- No se agregaron bibliotecas ni marcos de trabajo externos; se utilizo puramente .NET 8 estandar.
- Se omitio cualquier funcionalidad de facturacion, recetas, historial clinico o seguros, manteniendose estrictamente apegado a la especificacion inicial.

### Principios SOLID Aplicados

#### Single Responsibility Principle (SRP)
Cada clase tiene una unica razon para cambiar:
- `Paciente` solo sabe acerca de los datos de un paciente.
- `GestionPacientes` solo administra la lista de pacientes y sus busquedas.
- `Validador` solo se encarga de convertir y validar entradas.
- `Program` solo maneja la presentacion del menu.

#### Dependency Inversion Principle (DIP)
La clase `GestionCitas` no depende directamente de la implementacion concreta de notificaciones por correo (`RecordatorioCorreo`). En su lugar, depende de la abstraccion `IRecordatorio`. 
El constructor de `GestionCitas` recibe un `IRecordatorio` (Inyeccion de Dependencia):
```csharp
public GestionCitas(IRecordatorio recordatorio)
{
    this.recordatorio = recordatorio;
}
```
Esto desacopla la logica de citas del mecanismo de envio. Si en el futuro se requiere enviar avisos por SMS o WhatsApp, solo se crea una nueva clase que implemente `IRecordatorio` y se inyecta en el constructor, sin alterar una sola linea de codigo de `GestionCitas`.

#### Interface Segregation Principle (ISP)
La interfaz `IRecordatorio` es sumamente especifica. Define un unico metodo orientado a su funcion: `void Enviar(CitaMedica cita)`. De esta forma, ninguna clase que implemente la interfaz se ve obligada a desarrollar metodos que no necesita.

---

## 3. Resolucion del Conflicto de Horarios

Uno de los requerimientos criticos es evitar el cruce de citas. Esto se resolvio en el metodo `MedicoTieneConflicto` dentro de `GestionCitas.cs`:
1. Se recorren las citas existentes en la lista.
2. Se filtran aquellas que esten en estado `Agendada` (se ignoran las canceladas).
3. Se verifica si el ID del medico coincide y si la fecha/hora coincide exactamente.
4. Para la reprogramacion, se añadio un parametro opcional `citaIdIgnorar` para evitar que la propia cita que se esta reprogramando bloquee el cambio al mismo horario.

---

## 4. Reflexion Final

Esta practica ilustra como el diseño de software ordenado no requiere una infraestructura compleja o bases de datos pesadas. La correcta aplicacion de interfaces y la separacion de responsabilidades facilitan el mantenimiento del codigo y garantizan que el sistema sea facilmente escalable si los requerimientos cambian en el futuro.
