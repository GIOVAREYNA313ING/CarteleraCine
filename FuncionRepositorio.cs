using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
namespace CarteleraCine
{
    public class FuncionRepositorio
    {
        private const string CadenaConexion = "Server=(localdb)\\MSSQLLocalDB;Database=CarteleraDB;Integrated Security=True;TrustServerCertificate=True";
        public void Insertar(Funcion funcion)
        {
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                string consulta = "INSERT INTO Funciones (Pelicula, Sala, Precio) VALUES (@Pelicula, @Sala, @Precio)";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Pelicula", funcion.Pelicula);
                    comando.Parameters.AddWithValue("@Sala", funcion.Sala);
                    comando.Parameters.AddWithValue("@Precio", funcion.Precio);
                    comando.ExecuteNonQuery();
                }
            }
        }
        public List<Funcion> ListarTodos()
        {
            List<Funcion> lista = new List<Funcion>();
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                string consulta = "SELECT Id, Pelicula, Sala, Precio FROM Funciones ORDER BY Id";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            lista.Add(new Funcion
                            {
                                Id = lector.GetInt32(0),
                                Pelicula = lector.GetString(1),
                                Sala = lector.GetString(2),
                                Precio = lector.GetDecimal(3)
                            });
                        }
                    }
                }
            }
            return lista;
        }
        public Funcion? BuscarPorId(int id)
        {
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                string consulta = "SELECT Id, Pelicula, Sala, Precio FROM Funciones WHERE Id = @Id";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            return new Funcion
                            {
                                Id = lector.GetInt32(0),
                                Pelicula = lector.GetString(1),
                                Sala = lector.GetString(2),
                                Precio = lector.GetDecimal(3)
                            };
                        }
                    }
                }
            }
            return null;
        }
        public bool Actualizar(Funcion funcion)
        {
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                string consulta = "UPDATE Funciones SET Pelicula = @Pelicula, Sala = @Sala, Precio = @Precio WHERE Id = @Id";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Pelicula", funcion.Pelicula);
                    comando.Parameters.AddWithValue("@Sala", funcion.Sala);
                    comando.Parameters.AddWithValue("@Precio", funcion.Precio);
                    comando.Parameters.AddWithValue("@Id", funcion.Id);
                    int filas = comando.ExecuteNonQuery();
                    return filas == 1;
                }
            }
        }
        public bool Eliminar(int id)
        {
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                string consulta = "DELETE FROM Funciones WHERE Id = @Id";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);
                    int filas = comando.ExecuteNonQuery();
                    return filas == 1;
                }
            }
        }
    }
}