namespace ReservaSalas.Modelos
{
    public enum Calificacion
    {
        Regular,
        Buena,
        MuyBuena
    }

    public class Encuesta
    {
        public int ID { get; set; }
        public Calificacion PreparacionYLimpieza { get; set; }
        public bool MaterialesBrindados { get; set; }
        public bool ServicioATiempo { get; set; }
        public bool ServicioAGusto { get; set; }
        public string Observacion { get; set; }
    }
}
