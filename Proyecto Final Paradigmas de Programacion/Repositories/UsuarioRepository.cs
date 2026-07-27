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

        //inicio de sesion 
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

        //creacion de usuarios nuevos
        public void CrearUsuario(Usuario usuario)
        {
            using var con = conexion.ObtenerConexion();


            string sql = @"
               INSERT INTO Usuarios
               (
                   Nombre,
                   Correo,
                   Contrasena,
                   Rol
               )
               VALUES
               (
                   @Nombre,
                   @Correo,
                   @Contrasena,
                   'Usuario'
               )";


            con.Execute(sql, usuario);
        }

        //validar que no hayan correos repetidos
        public bool ExisteCorreo(string correo)
        {
            using var con = conexion.ObtenerConexion();


            string sql = @"
                  SELECT COUNT(*)
                  FROM Usuarios
                  WHERE Correo = @Correo";


            int cantidad = con.ExecuteScalar<int>(
                sql,
                new
                {
                    Correo = correo
                });


            return cantidad > 0;
        }


    }
}