using System; // Necesario para IDisposable
using System.Data;
using System.Data.SQLite;

namespace Thor_Bar // Asegúrate de que este sea el namespace correcto para tu clase
{
    // La clase ahora implementa la interfaz IDisposable
    public class DatabaseConnection : IDisposable
    {
        private string connectionString = "Data Source=thor_bar.sqlite;Version=3;";
        private SQLiteConnection connection;
        private bool disposed = false; // Para controlar si los recursos ya se liberaron

        public DatabaseConnection()
        {
            connection = new SQLiteConnection(connectionString);
        }

        public SQLiteConnection GetConnection()
        {
            return connection;
        }

        public void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Implementación del método Dispose de la interfaz IDisposable.
        /// Este es el método principal que se llamará al salir de un bloque 'using'.
        /// </summary>
        public void Dispose()
        {
            // Llama al método de limpieza real y suprime la finalización.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Método de Dispose protegido virtual para la lógica de limpieza.
        /// </summary>
        /// <param name="disposing">
        /// true si se llama desde el método Dispose() (limpieza explícita o 'using');
        /// false si se llama desde el finalizador (limpieza por el recolector de basura).
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed) // Solo ejecutar la limpieza una vez
            {
                if (disposing)
                {
                    // Liberar recursos administrados (como la conexión SQLite)
                    if (connection != null)
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close(); // Asegurarse de cerrar la conexión
                        }
                        connection.Dispose(); // Liberar la conexión
                        connection = null; // Liberar la referencia
                    }
                }

                // Aquí iría la lógica para liberar recursos no administrados (si los hubiera directamente).
                // Para SQLiteConnection, su propio Dispose() ya maneja sus recursos no administrados.

                disposed = true; // Marcar como dispuesto
            }
        }

        /// <summary>
        /// Finalizador (destructor) en caso de que Dispose() no se llame explícitamente.
        /// </summary>
        ~DatabaseConnection()
        {
            // Llama al método de limpieza real indicando que no es una disposición explícita.
            Dispose(false);
        }
    }
}