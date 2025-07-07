using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms; 
namespace Thor_Bar.Managers 
{
    public class GenerarComprobante
    {
        private DataGridView _dgvComprobantes;
        private RichTextBox _rtbDetallesComprobante;
        /// <param name="dgv">El DataGridView donde se mostrarán los comprobantes.</param>
        /// <param name="rtb">El RichTextBox donde se mostrarán los detalles del comprobante.</param>
        public GenerarComprobante(DataGridView dgv, RichTextBox rtb)
        {
            _dgvComprobantes = dgv ?? throw new ArgumentNullException(nameof(dgv));
            _rtbDetallesComprobante = rtb ?? throw new ArgumentNullException(nameof(rtb));

            // Configuración básica del DataGridView de comprobantes
            _dgvComprobantes.AllowUserToAddRows = false;
            _dgvComprobantes.AllowUserToResizeRows = false;
            _dgvComprobantes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvComprobantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvComprobantes.MultiSelect = false;
            _dgvComprobantes.BackgroundColor = System.Drawing.Color.White;

            // Suscripción al evento SelectionChanged directamente aquí
            _dgvComprobantes.SelectionChanged += DgvComprobantes_SelectionChanged;

            // Configuración básica del RichTextBox
            _rtbDetallesComprobante.ReadOnly = true;
            _rtbDetallesComprobante.BackColor = System.Drawing.Color.LightYellow;
        }

        public void CargarComprobantes()
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=thor_bar.sqlite"))
                {
                    conn.Open();
                    string query = "SELECT id, pedido_id, mesa_numero, fecha_hora_cierre, total_pedido, detalles_productos FROM comprobantes ORDER BY fecha_hora_cierre DESC";
                    using (var adapter = new SQLiteDataAdapter(query, conn))
                    {
                        DataTable comprobantesTable = new DataTable();
                        adapter.Fill(comprobantesTable);
                        _dgvComprobantes.DataSource = comprobantesTable;

                        if (_dgvComprobantes.Columns.Contains("id"))
                            _dgvComprobantes.Columns["id"].Visible = false;

                        if (_dgvComprobantes.Columns.Contains("pedido_id"))
                            _dgvComprobantes.Columns["pedido_id"].HeaderText = "ID Pedido";

                        if (_dgvComprobantes.Columns.Contains("mesa_numero"))
                            _dgvComprobantes.Columns["mesa_numero"].HeaderText = "Mesa";

                        if (_dgvComprobantes.Columns.Contains("fecha_hora_cierre"))
                            _dgvComprobantes.Columns["fecha_hora_cierre"].HeaderText = "Fecha/Hora Cierre";

                        if (_dgvComprobantes.Columns.Contains("total_pedido"))
                        {
                            _dgvComprobantes.Columns["total_pedido"].HeaderText = "Total";
                            _dgvComprobantes.Columns["total_pedido"].DefaultCellStyle.Format = "C2";
                        }

                        if (_dgvComprobantes.Columns.Contains("detalles_productos"))
                            _dgvComprobantes.Columns["detalles_productos"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar comprobantes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvComprobantes_SelectionChanged(object sender, EventArgs e)
        {
            if (_dgvComprobantes.CurrentRow != null)
            {
                _rtbDetallesComprobante.Text = _dgvComprobantes.CurrentRow.Cells["detalles_productos"].Value?.ToString();
            }
            else
            {
                _rtbDetallesComprobante.Clear();
            }
        }
    }
}