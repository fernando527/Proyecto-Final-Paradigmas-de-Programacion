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
        //aqui esta el read (optener reportes para admin)
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

        public List<Reporte> ObtenerReportesPorUsuario(int idUsuario)
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

              WHERE R.IdUsuario = @IdUsuario

              ORDER BY R.FechaReporte DESC";


            return con.Query<Reporte>(
                sql,
                new
                {
                    IdUsuario = idUsuario
                }
            ).ToList();
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

        //aqui esta el edit

        public Reporte ObtenerReportePorId(int id)
        {
            using var con = conexion.ObtenerConexion();


            string sql = @"
               SELECT 
                   r.*,
                   u.Nombre AS NombreUsuario,
                   e.Nombre AS NombreEstado
               FROM Reportes r

               INNER JOIN Usuarios u
                   ON r.IdUsuario = u.IdUsuario

               INNER JOIN Estados e
                   ON r.IdEstado = e.IdEstado

               WHERE r.IdReporte = @Id";


            return con.QueryFirstOrDefault<Reporte>(
                sql,
                new { Id = id }
            );
        }

        public void ActualizarReporte(Reporte reporte)
        {
            using var con = conexion.ObtenerConexion();

            string sql = @"
            UPDATE Reportes
             SET
            Edificio = @Edificio,
            Aula = @Aula,
            IdTipoDanio = @IdTipoDanio,
            Descripcion = @Descripcion,
            IdPrioridad = @IdPrioridad,
            EsUrgente = @EsUrgente
             WHERE IdReporte = @IdReporte";

            con.Execute(sql, reporte);
        }

        //eliminar

        public void EliminarReporte(int id)
        {
            using var con = conexion.ObtenerConexion();

            string sql = @"DELETE FROM Reportes
                   WHERE IdReporte = @Id";

            con.Execute(sql, new { Id = id });
        }

        //editar estado

        public void ActualizarEstadoReporte(int idReporte, int idEstado)
        {
            using var con = conexion.ObtenerConexion();


            string sql = @"
        UPDATE Reportes
        SET IdEstado = @IdEstado
        WHERE IdReporte = @IdReporte";


            con.Execute(sql, new
            {
                IdReporte = idReporte,
                IdEstado = idEstado
            });
        }


        //consulta para los dashbord

        public Dashboard ObtenerDashboard()
        {
            using var con = conexion.ObtenerConexion();


            string sql = @"SELECT
            COUNT(*) AS TotalReportes,
                SUM(CASE WHEN IdEstado = 1 THEN 1 ELSE 0 END) AS Pendientes,
                SUM(CASE WHEN IdEstado = 2 THEN 1 ELSE 0 END) AS EnRevision,
                SUM(CASE WHEN IdEstado = 3 THEN 1 ELSE 0 END) AS EnProceso,
                SUM(CASE WHEN IdEstado = 4 THEN 1 ELSE 0 END) AS Reparados,
                SUM(CASE WHEN EsUrgente = 1 THEN 1 ELSE 0 END) AS Urgentes,
                SUM(CASE WHEN IdPrioridad = 3 THEN 1 ELSE 0 END) AS PrioridadAlta,
                SUM(CASE WHEN IdPrioridad = 2 THEN 1 ELSE 0 END) AS PrioridadMedia,
                SUM(CASE WHEN IdPrioridad = 1 THEN 1 ELSE 0 END) AS PrioridadBaja
                FROM Reportes ";
            return con.QueryFirstOrDefault<Dashboard>(sql);

        }

    }


}
