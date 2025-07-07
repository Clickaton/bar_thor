// Archivo: Helpers/PdfGenerator.cs
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms;
using System;
using System.Data;

namespace Thor_Bar.Helpers
{
    public class PdfGenerator
    {
        public static void GenerarFacturaPdf(string rutaArchivo, string detallesComprobante, DataTable productosComprobante, string totalComprobante)
        {
            Document doc = new Document(PageSize.A4);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
                doc.Open();

                // --- Encabezado de la Factura ---
                doc.Add(new Paragraph("THOR BAR", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Av. Principal 123, Ciudad, País", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)) { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Tel: 123-4567 | Email: info@thorbar.com", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)) { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("\n")); // Salto de línea

                doc.Add(new Paragraph($"Fecha: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)));
                doc.Add(new Paragraph($"Factura Nº: {new Random().Next(1000, 9999)}", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10))); // Número de factura simple, podrías tener uno real de DB
                doc.Add(new Paragraph("\n"));

                // --- Detalles del Comprobante (Descripción) ---
                doc.Add(new Paragraph("Detalles del Comprobante:", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD)));
                doc.Add(new Paragraph(detallesComprobante));
                doc.Add(new Paragraph("\n"));

                // --- Tabla de Productos/Items ---
                if (productosComprobante != null && productosComprobante.Rows.Count > 0)
                {
                    PdfPTable table = new PdfPTable(productosComprobante.Columns.Count);
                    table.WidthPercentage = 100;
                    table.SpacingBefore = 10f;
                    table.SpacingAfter = 10f; // Esto añade espacio después de la tabla

                    // Añadir encabezados de la tabla
                    foreach (DataColumn column in productosComprobante.Columns)
                    {
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                        headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                        table.AddCell(headerCell);
                    }

                    // Añadir filas de la tabla
                    foreach (DataRow row in productosComprobante.Rows)
                    {
                        foreach (object cell in row.ItemArray)
                        {
                            table.AddCell(new Phrase(cell.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9)));
                        }
                    }
                    doc.Add(table);
                }
                else
                {
                    doc.Add(new Paragraph("No hay productos detallados para este comprobante.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)));
                }

                // AÑADIDO: Más saltos de línea para empujar el total hacia abajo
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n")); // Salto de línea adicional
                doc.Add(new Paragraph("\n")); // Otro salto de línea adicional

                // --- Total ---
                doc.Add(new Paragraph(totalComprobante, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_RIGHT });
                doc.Add(new Paragraph("\n"));

                // --- Pie de página (Opcional) ---
                doc.Add(new Paragraph("¡Gracias por su visita!", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)) { Alignment = Element.ALIGN_CENTER });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error de PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                doc.Close();
            }
        }
    }
}