using System;
using System.Data.SQLite;

namespace Thor_Bar
{
    public class ModificarGasto
    {
        public static void Ejecutar(int id, string descripcion, decimal monto, DateTime fecha, string categoria)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.OpenConnection();

            string query = "UPDATE Gastos SET Descripcion = @desc, Monto = @monto, Fecha = @fecha, Categoria = @cat WHERE Id = @id";

            using (SQLiteCommand cmd = new SQLiteCommand(query, db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@desc", descripcion);
                cmd.Parameters.AddWithValue("@monto", monto);
                cmd.Parameters.AddWithValue("@fecha", fecha.ToString("yyyy-MM-dd")); // Se guarda como texto seguro
                cmd.Parameters.AddWithValue("@cat", categoria);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

            db.CloseConnection();
        }
    }
}
