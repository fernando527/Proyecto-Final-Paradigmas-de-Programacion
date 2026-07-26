using Dapper;
using Proyecto_Final_Paradigmas_de_Programacion.Data;
using Proyecto_Final_Paradigmas_de_Programacion.Models;

namespace Proyecto_Final_Paradigmas_de_Programacion.Repositories
{
    public class UsuarioRepository
    {
        private readonly ConexionDB conexion;

        public UsuarioRepository()
        {
            conexion = new ConexionDB();
        }

        public Usuario? Login(string correo, string contrasena)
        {
            using var con = conexion.ObtenerConexion();

            string sql = @"
                SELECT *
                FROM Usuarios
                WHERE Correo = @Correo
                AND Contrasena = @Contrasena";

            return con.QueryFirstOrDefault<Usuario>(
                sql,
                new
                {
                    Correo = correo,
                    Contrasena = contrasena
                });
        }
    }
}