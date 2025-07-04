using System;
using System.Data.SQLite;

namespace Thor_Bar
{
    public class AgregarGasto
    {
        public static void Ejecutar(string descripcion, decimal monto, string fecha, string categoria)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.OpenConnection();

            string query = "INSERT INTO Gastos (Descripcion, Monto, Fecha, Categoria) VALUES (@desc, @monto, @fecha, @cat)";
            using (SQLiteCommand cmd = new SQLiteCommand(query, db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@desc", descripcion);
                cmd.Parameters.AddWithValue("@monto", monto);
                cmd.Parameters.AddWithValue("@fecha", fecha); // Guardado como texto
                cmd.Parameters.AddWithValue("@cat", categoria);

                cmd.ExecuteNonQuery();
            }

            db.CloseConnection();
        }
    }
}