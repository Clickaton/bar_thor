using System.Data.SQLite;

namespace Thor_Bar
{
    public class EliminarGasto
    {
        public static void Ejecutar(int id)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.OpenConnection();

            string query = "DELETE FROM Gastos WHERE Id = @id";
            using (SQLiteCommand cmd = new SQLiteCommand(query, db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            db.CloseConnection();
        }
    }
}
