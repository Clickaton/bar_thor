using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mysqlx.Cursor;

namespace Thor_Bar.Service
{
    internal class cajaService
    {
        public void crearNuevaCaja() {
            


            Caja nuevaCaja = new Caja(DateTime.Now, 0.00m, 0.00m, 0.00m);
            using (var conn = new SQLiteConnection("Data Source=thor_bar.sqlite"))
            {
             /*   conn.Open();
                string query = @"";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idedido);
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable tabla = new DataTable();
                        adapter.Fill(tabla);

         
                 
                    }
                }
             */
            }

            
        }

    }
    
}
