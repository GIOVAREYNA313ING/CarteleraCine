using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace CarteleraCine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FuncionRepositorio repositorio = new FuncionRepositorio();
            int opcion;
            do
            {
                Console.WriteLine("=== SISTEMA DE CARTELERA DE CINE ===");
                Console.WriteLine("1. Registrar función");
                Console.WriteLine("2. Listar funciones");
                Console.WriteLine("3. Buscar función por Id");
                Console.WriteLine("4. Actualizar precio");
                Console.WriteLine("5. Eliminar función");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opción inválida.\n");
                    continue;
                }
                try
                {
                    switch (opcion)
                    {
                        case 1:
                            Console.Write("Película: ");
                            string pelicula = Console.ReadLine();
                            Console.Write("Sala: ");
                            string sala = Console.ReadLine();
                            Console.Write("Precio: ");
                            string textoPrecio = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(pelicula) ||
                                string.IsNullOrWhiteSpace(sala) ||
                                !decimal.TryParse(textoPrecio, out decimal precio) ||
                                precio <= 0)
                            {
                                Console.WriteLine("Datos inválidos.\n");
                                break;
                            }
                            Funcion nueva = new Funcion
                            {
                                Pelicula = pelicula,
                                Sala = sala,
                                Precio = precio
                            };
                            repositorio.Insertar(nueva);
                            Console.WriteLine("Función registrada.\n");
                            break;
                        case 2:
                            List<Funcion> lista = repositorio.ListarTodos();
                            if (lista.Count == 0)
                            {
                                Console.WriteLine("No hay funciones registradas.\n");
                                break;
                            }
                            decimal total = 0;
                            foreach (Funcion f in lista)
                            {
                                Console.WriteLine(f.ToString());
                                total += f.Precio;
                            }
                            Console.WriteLine($"TOTAL DE PRECIOS: Q {total:F2}\n");
                            break;
                        case 3:
                            Console.Write("Ingrese el Id: ");
                            if (!int.TryParse(Console.ReadLine(), out int idBuscar))
                            {
                                Console.WriteLine("Id inválido.\n");
                                break;
                            }
                            Funcion encontrada = repositorio.BuscarPorId(idBuscar);

                            if (encontrada == null)
                            {
                                Console.WriteLine("No existe ese Id.\n");
                            }
                            else
                            {
                                Console.WriteLine(encontrada.ToString() + "\n");
                            }
                            break;
                        case 4:
                            Console.Write("Ingrese el Id: ");
                            if (!int.TryParse(Console.ReadLine(), out int idActualizar))
                            {
                                Console.WriteLine("Id inválido.\n");
                                break;
                            }
                            Funcion existe = repositorio.BuscarPorId(idActualizar);
                            if (existe == null)
                            {
                                Console.WriteLine("No existe ese Id.\n");
                                break;
                            }
                            Console.Write("Nuevo precio: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal nuevoPrecio) || nuevoPrecio <= 0)
                            {
                                Console.WriteLine("Precio inválido.\n");
                                break;
                            }
                            existe.Precio = nuevoPrecio;
                            repositorio.Actualizar(existe);
                            Console.WriteLine("Función actualizada.\n");
                            break;
                        case 5:
                            Console.Write("Ingrese el Id: ");
                            if (!int.TryParse(Console.ReadLine(), out int idEliminar))
                            {
                                Console.WriteLine("Id inválido.\n");
                                break;
                            }
                            bool eliminado = repositorio.Eliminar(idEliminar);
                            if (eliminado)
                            {
                                Console.WriteLine("Función eliminada.\n");
                            }
                            else
                            {
                                Console.WriteLine("No existe ese Id.\n");
                            }
                            break;
                        case 6:
                            Console.WriteLine("Saliendo del sistema...");
                            break;
                        default:
                            Console.WriteLine("Opción inválida.\n");
                            break;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error de base de datos: {ex.Message}\n");
                }
            } while (opcion != 6);
        }
    }
}