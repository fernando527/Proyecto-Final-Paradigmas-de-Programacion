using Dapper;
using Proyecto_Final_Paradigmas_de_Programacion.Data;
using Proyecto_Final_Paradigmas_de_Programacion.Models;

namespace Proyecto_Final_Paradigmas_de_Programacion.Repositories
{
    public class ReporteRepository
    {
        private readonly ConexionDB conexion;

        public ReporteRepository()
        {
            conexion = new ConexionDB();
        }
        //aqui esta el read
        public List<Reporte> ObtenerReportes()
        {
            using var con = conexion.ObtenerConexion();

            string sql = @"
             SELECT
             R.IdReporte,
             R.IdUsuario,
             U.Nombre AS NombreUsuario,
             R.Edificio,
             R.Aula,
             R.IdTipoDanio,
             T.Nombre AS NombreDanio,
             R.Descripcion,
             R.IdPrioridad,
             P.Nombre AS NombrePrioridad,
             R.IdEstado,
             E.Nombre AS NombreEstado,
             R.FechaReporte,
             R.EsUrgente,
             R.Imagen
                FROM Reportes R
                INNER JOIN Usuarios U
                ON R.IdUsuario = U.IdUsuario
                INNER JOIN TiposDanio T
                ON R.IdTipoDanio = T.IdTipoDanio
                INNER JOIN Prioridades P
                ON R.IdPrioridad = P.IdPrioridad
                INNER JOIN Estados E
                ON R.IdEstado = E.IdEstado
                ORDER BY R.FechaReporte DESC";

            return con.Query<Reporte>(sql).ToList();
        }

        //aqui esta el create
        public void CrearReporte(Reporte reporte)
        {
            using var con = conexion.ObtenerConexion();

            string sql = @"
             INSERT INTO Reportes
            (IdUsuario,
            Edificio,
            Aula,
            IdTipoDanio,
            Descripcion,
            IdPrioridad,
            IdEstado,
            EsUrgente)
        VALUES(
            @IdUsuario,
            @Edificio,
            @Aula,
            @IdTipoDanio,
            @Descripcion,
            @IdPrioridad,
            1,
            @EsUrgente)";
            con.Execute(sql, reporte);
        }

        //siguiente

    }
}