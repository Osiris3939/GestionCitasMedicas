namespace GestionCitasMedicas
{
    // Interfaz para desacoplar el envio de recordatorios.
    // Se usa para cumplir con el principio de inversion de dependencias (DIP) y segregacion de interfaces (ISP).
    // De este modo, si en el futuro queremos avisar por SMS o WhatsApp, solo implementamos esta interfaz.
    public interface IRecordatorio
    {
        void Enviar(CitaMedica cita);
    }
}
