using Microsoft.Data.SqlClient;

namespace Proyecto_Final_Paradigmas_de_Programacion.Data
{
    public class ConexionDB
    {
        private readonly string cadenaConexion;

        public ConexionDB()
        {
            cadenaConexion =
                "workstation id=ADPteam.mssql.somee.com;" +
                "packet size=4096;" +
                "user id=ADP_SQLLogin_1;" +
                "pwd=y4u5h2qmqk;" +
                "data source=ADPteam.mssql.somee.com;" +
                "persist security info=False;" +
                "initial catalog=ADPteam;" +
                "TrustServerCertificate=True;";
        }


        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}