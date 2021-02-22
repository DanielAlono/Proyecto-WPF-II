using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_WPF__II_
{
    public class BaseDatosService
    {
        private readonly SqliteConnection _conexion;
        private SqliteCommand _comando;
        public BaseDatosService()
        {
            _conexion = new SqliteConnection("Data Source=cartelera.db");
            CrearTablas();
        }

        private void CrearTablas()
        {
            _conexion.Open();
            _comando = _conexion.CreateCommand();

            /*_comando.CommandText = @"DROP TABLE IF EXISTS peliculas";
            _comando.ExecuteNonQuery();
            _comando.CommandText = @"DROP TABLE IF EXISTS salas";
            _comando.ExecuteNonQuery();
            _comando.CommandText = @"DROP TABLE IF EXISTS sesiones";
            _comando.ExecuteNonQuery();
            _comando.CommandText = @"DROP TABLE IF EXISTS ventas";
            _comando.ExecuteNonQuery();*/

            _comando.CommandText = @"CREATE TABLE IF NOT EXISTS peliculas (
                            idPelicula   INTEGER PRIMARY KEY,
                            titulo       TEXT,
                            cartel       TEXT,
                            año          INTEGER,
                            genero       TEXT,
                            calificacion TEXT
                            )";
            _comando.ExecuteNonQuery();

            _comando.CommandText = @"CREATE TABLE IF NOT EXISTS salas (
                            idSala     INTEGER PRIMARY KEY AUTOINCREMENT,
                            numero     TEXT,
                            capacidad  INTEGER,
                            disponible BOOLEAN DEFAULT (true) 
                            )";
            _comando.ExecuteNonQuery();

            _comando.CommandText = @"CREATE TABLE IF NOT EXISTS sesiones (
                            idSesion INTEGER PRIMARY KEY AUTOINCREMENT,
                            pelicula INTEGER REFERENCES peliculas (idPelicula),
                            sala     INTEGER REFERENCES salas (idSala),
                            hora     TEXT
                            )";
            _comando.ExecuteNonQuery();

            _comando.CommandText = @"CREATE TABLE IF NOT EXISTS ventas (
                            idVenta  INTEGER PRIMARY KEY AUTOINCREMENT,
                            sesion   INTEGER REFERENCES sesiones (idSesion),
                            cantidad INTEGER,
                            pago     TEXT
                            )";
            _comando.ExecuteNonQuery();

            _conexion.Close();
        }
        //PELICULAS
        public ObservableCollection<Pelicula> ObtenerPeliculas()
        {
            ObservableCollection<Pelicula> peliculas = new ObservableCollection<Pelicula>();

            _conexion.Open();
            _comando = _conexion.CreateCommand();

            _comando.CommandText = "SELECT * FROM peliculas";
            SqliteDataReader lector = _comando.ExecuteReader();

            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    int id = lector.GetInt32(0);
                    string titulo = lector.GetString(1);
                    string cartel = lector.GetString(2);
                    int año = lector.GetInt32(3);
                    string genero = lector.GetString(4);
                    string calificacion = lector.GetString(5);
                    peliculas.Add(new Pelicula(id, titulo, cartel, año, genero, calificacion));
                }
            }
            lector.Close();
            _conexion.Close();

            return peliculas;
        }
        public void BorrarPeliculas()
        {
            _conexion.Open();
            _comando = _conexion.CreateCommand();

            _comando.CommandText = "DROP TABLE IF EXISTS peliculas";
            _comando.ExecuteNonQuery();

            _conexion.Close();
            CrearTablas();
        }

        public void InsertarPelicula(Pelicula pelicula)
        {
            _conexion.Open();
            _comando = _conexion.CreateCommand();

            _comando.CommandText = "INSERT INTO peliculas VALUES (@idPelicula,@titulo,@cartel,@año,@genero,@calificacion)";
            _comando.Parameters.Add("@idPelicula", SqliteType.Integer);
            _comando.Parameters.Add("@titulo", SqliteType.Text);
            _comando.Parameters.Add("@cartel", SqliteType.Text);
            _comando.Parameters.Add("@año", SqliteType.Integer);
            _comando.Parameters.Add("@genero", SqliteType.Text);
            _comando.Parameters.Add("@calificacion", SqliteType.Text);
            _comando.Parameters["@idPelicula"].Value = pelicula.Id;
            _comando.Parameters["@titulo"].Value = pelicula.Titulo;
            _comando.Parameters["@cartel"].Value = pelicula.Cartel;
            _comando.Parameters["@año"].Value = pelicula.Año;
            _comando.Parameters["@genero"].Value = pelicula.Genero;
            _comando.Parameters["@calificacion"].Value = pelicula.Calificacion;
            _comando.ExecuteNonQuery();

            _conexion.Close();
        }

        //SALAS
        public ObservableCollection<Sala> ObtenerSalas()
        {
            ObservableCollection<Sala> salas = new ObservableCollection<Sala>();

            _conexion.Open();
            _comando = _conexion.CreateCommand();

            _comando.CommandText = "SELECT * FROM salas";
            SqliteDataReader lector = _comando.ExecuteReader();

            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    int idSala = lector.GetInt32(0);
                    string numero = lector.GetString(1);
                    int capacidad = lector.GetInt32(2);
                    bool disponible = lector.GetBoolean(3);
                    salas.Add(new Sala(idSala, numero, capacidad, disponible));
                }
            }
            lector.Close();
            _conexion.Close();

            return salas;
        }

        public void InsertarSala(Sala sala)
        {
            _conexion.Open();
            _comando = _conexion.CreateCommand();

            _comando.CommandText = "INSERT INTO salas VALUES (@idSala,@numero,@capacidad,@disponible)";
            _comando.Parameters.Add("@idSala", SqliteType.Integer);
            _comando.Parameters.Add("@numero", SqliteType.Text);
            _comando.Parameters.Add("@capacidad", SqliteType.Integer);
            _comando.Parameters.Add("@disponible", SqliteType.Real);
            _comando.Parameters["@idSala"].Value = sala.IdSala;
            _comando.Parameters["@numero"].Value = sala.Numero;
            _comando.Parameters["@capacidad"].Value = sala.Capacidad;
            _comando.Parameters["@disponible"].Value = sala.Disponible;
            _comando.ExecuteNonQuery();

            _conexion.Close();
        }

        public void ActualizarSala(Sala sala)
        {
            _conexion.Open();
            _comando = _conexion.CreateCommand();

            _comando.CommandText = "UPDATE salas SET numero=@numero, " +
                                                "SET capacidad=@capacidad," +
                                                "SET disponible=@disponible " +
                                                "WHERE idSala=@idSala";
            _comando.Parameters["@idSala"].Value = sala.IdSala;
            _comando.Parameters["@numero"].Value = sala.Numero;
            _comando.Parameters["@capacidad"].Value = sala.Capacidad;
            _comando.Parameters["@disponible"].Value = sala.Disponible;
            _comando.ExecuteNonQuery();

            _conexion.Close();
        }

        //SESIONES
        public ObservableCollection<Sesiones> ObtenerSesiones()
        {
            ObservableCollection<Sesiones> sesiones = new ObservableCollection<Sesiones>();

            _conexion.Open();
            _comando = _conexion.CreateCommand();

            _comando.CommandText = "SELECT * FROM sesiones";
            SqliteDataReader lector = _comando.ExecuteReader();

            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    int idSesion = lector.GetInt32(0);
                    int pelicula = lector.GetInt32(1);
                    int sala = lector.GetInt32(2);
                    string hora = lector.GetString(3);
                    sesiones.Add(new Sesiones(idSesion, pelicula, sala, hora));
                }
            }
            lector.Close();
            _conexion.Close();

            return sesiones;
        }
        public void InsertarSesion(Sesiones sesion)
        {
            _conexion.Open();
            _comando = _conexion.CreateCommand();

            _comando.CommandText = "INSERT INTO sesiones VALUES (@idSesion,@pelicula,@sala,@hora)";
            _comando.Parameters.Add("@idSesion", SqliteType.Integer);
            _comando.Parameters.Add("@pelicula", SqliteType.Integer);
            _comando.Parameters.Add("@sala", SqliteType.Integer);
            _comando.Parameters.Add("@hora", SqliteType.Text);
            _comando.Parameters["@idSesion"].Value = sesion.IdSesion;
            _comando.Parameters["@pelicula"].Value = sesion.Pelicula;
            _comando.Parameters["@sala"].Value = sesion.Sala;
            _comando.Parameters["@hora"].Value = sesion.Hora;
            _comando.ExecuteNonQuery();

            _conexion.Close();
        }

        public void ActualizarSesion(Sesiones sesion)
        {
            _conexion.Open();
            _comando = _conexion.CreateCommand();

            _comando.CommandText = "UPDATE sesiones SET pelicula=@pelicula, " +
                                                    "SET sala=@sala," +
                                                    "SET hora=@hora " +
                                                    "WHERE idSesion=@idSesion";
            _comando.Parameters["@idSesion"].Value = sesion.IdSesion;
            _comando.Parameters["@pelicula"].Value = sesion.Pelicula;
            _comando.Parameters["@sala"].Value = sesion.Sala;
            _comando.Parameters["@hora"].Value = sesion.Hora;
            _comando.ExecuteNonQuery();

            _conexion.Close();
        }

        //VENTAS
        public ObservableCollection<Ventas> ObtenerVentas()
        {
            ObservableCollection<Ventas> ventas = new ObservableCollection<Ventas>();

            _conexion.Open();
            _comando = _conexion.CreateCommand();

            _comando.CommandText = "SELECT * FROM ventas";
            SqliteDataReader lector = _comando.ExecuteReader();

            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    int idVenta = lector.GetInt32(0);
                    int sesion = lector.GetInt32(1);
                    int cantidad = lector.GetInt32(2);
                    string pago = lector.GetString(3);
                    ventas.Add(new Ventas(idVenta, sesion, cantidad, pago));
                }
            }
            lector.Close();
            _conexion.Close();

            return ventas;
        }

        public void InsertarVenta(Ventas venta)
        {
            _conexion.Open();
            _comando = _conexion.CreateCommand();

            _comando.CommandText = "INSERT INTO ventas VALUES (@idVenta,@sesion,@cantidad,@pago)";
            _comando.Parameters.Add("@idVenta", SqliteType.Integer);
            _comando.Parameters.Add("@sesion", SqliteType.Integer);
            _comando.Parameters.Add("@cantidad", SqliteType.Integer);
            _comando.Parameters.Add("@pago", SqliteType.Text);
            _comando.Parameters["@idVenta"].Value = venta.IdVenta;
            _comando.Parameters["@sesion"].Value = venta.Sesion;
            _comando.Parameters["@cantidad"].Value = venta.Cantidad;
            _comando.Parameters["@pago"].Value = venta.Pago;
            _comando.ExecuteNonQuery();

            _conexion.Close();
        }
    }
}
