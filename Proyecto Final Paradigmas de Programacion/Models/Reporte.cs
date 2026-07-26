namespace Proyecto_Final_Paradigmas_de_Programacion.Models
{
    public class Reporte
    {

        public int IdReporte { get; set; }

        public int IdUsuario { get; set; }

        public string Edificio { get; set; }

        public string Aula { get; set; }

        public int IdTipoDanio { get; set; }

        public string Descripcion { get; set; }

        public int IdPrioridad { get; set; }

        public int IdEstado { get; set; }

        public DateTime FechaReporte { get; set; }

        public bool EsUrgente { get; set; }

        public string Imagen { get; set; }


        // Datos adicionales para mostrar en tablas
        public string NombreUsuario { get; set; }

        public string NombreDanio { get; set; }

        public string NombreEstado { get; set; }

        public string NombrePrioridad { get; set; }
    }
}
