using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Utils
{
    public class PDFUtils
    {

        private static String defaulDateFormat = "dd/MM/yyyy";

        public String crearPDFVoucher(Voucher voucher)
        {

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            String filePath = getPDFFilePath(voucher);
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);
            doc.Open();

            PdfPTable tabFot = new PdfPTable(3);

            tabFot.TotalWidth = doc.PageSize.Width - 50F;

            generarEncabezado(tabFot, "Voucher: " + voucher.Numero);

            generarDatosUsuarioVoucher(tabFot, voucher);

            generarDatosComplementariosVoucher(tabFot, voucher);

            generarContenidoVoucher(tabFot, voucher);

            generarPieDePagina(tabFot, "El estado de este voucher es: " + voucher.Estado);


            doc.Add(tabFot);

            doc.Close();

            return filePath;

        }

        public String crearPDFVentas(List<Venta> ventas)
        {

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            String filePath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf"; 
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);
            doc.Open();

            PdfPTable tabFot = new PdfPTable(3);

            tabFot.TotalWidth = doc.PageSize.Width - 50F;

            generarEncabezado(tabFot, "Generado el:" + DateTime.Now.ToString("yyyy/MM/dd-HH:mm"));

            generarTitulo(tabFot, "Reporte de ventas");

            Decimal total = generarContenidoReporteVentas(tabFot, ventas);

            generarReporteDeVentasTotal(tabFot, total);

            generarPieDePagina(tabFot, "Pie de pagina");

            doc.Add(tabFot);

            doc.Close();

            return agregarNumeroDePagina(filePath);



        }


        private String agregarNumeroDePagina(String filePath)
        {
            byte[] bytes = File.ReadAllBytes(@filePath);
            Font blackFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(bytes);
                using (PdfStamper stamper = new PdfStamper(reader, stream))
                {
                    int pages = reader.NumberOfPages;
                    for (int i = 1; i <= pages; i++)
                    {
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase(i.ToString()+"/"+pages.ToString(), blackFont), 568f, 15f, 0);
                    }
                }
                bytes = stream.ToArray();
            }
            String filePathFinal = generarPDFVentasNombreDeArchivo();
            File.WriteAllBytes(@filePathFinal, bytes);

            return filePathFinal;
        }


        private void generarReporteDeVentasTotal(PdfPTable tabFot, decimal total)
        {
            PdfPCell celdaDatosComplementarios = new PdfPCell();
            celdaDatosComplementarios.Colspan = 3;

            Paragraph paragraph = new Paragraph("Total: " + total.ToString());
            paragraph.Alignment = Element.TITLE;
            //paragraph.Font.Size = 16F;
            paragraph.Font = FontFactory.GetFont("Verdana", 14F, Font.BOLD);
            paragraph.SpacingAfter = 10F;

            celdaDatosComplementarios.AddElement(paragraph);

            tabFot.AddCell(celdaDatosComplementarios);
        }

        private Decimal generarContenidoReporteVentas(PdfPTable tabFot, List<Venta> ventas)
        {
            Decimal total = 0;
            Decimal saleTotal;
            PdfPCell celdaDatosVenta;
            PdfPCell celdaTotalVenta;
            Paragraph parrafoTextoLinea;
            Paragraph parrafoTotalLinea;

            DateTime menorFecha = DateTime.MinValue;
            DateTime mayorFecha = DateTime.MinValue;

            foreach (Venta venta in ventas)
            {
                saleTotal = 0;
                for (int i = 0; i < venta.FacturaVenta.Items.Count; i++)
                {
                    DetalleFactura detalleFactura = venta.FacturaVenta.Items[i];
                    Paragraph paragraph = new Paragraph(detalleFactura.Detalle);

                    saleTotal = saleTotal + (detalleFactura.Cantidad * detalleFactura.Precio);

                }

                celdaDatosVenta = new PdfPCell();
                celdaDatosVenta.Colspan = 2;

                celdaTotalVenta = new PdfPCell();
                celdaTotalVenta.Colspan = 1;

                String textoLinea = "Fecha: "+venta.FacturaVenta.Fecha.ToString(defaulDateFormat) +" Cliente: " + venta.ClienteVenta.Apellido + ", " + venta.ClienteVenta.Nombre + "- DNI: " + venta.ClienteVenta.Dni + " - Codigo Factura: " + venta.FacturaVenta.IdFactura;

                if (menorFecha == DateTime.MinValue || menorFecha > venta.FacturaVenta.Fecha)
                {
                    menorFecha = venta.FacturaVenta.Fecha;
                }

                if (mayorFecha == DateTime.MinValue || mayorFecha < venta.FacturaVenta.Fecha)
                {
                    mayorFecha = venta.FacturaVenta.Fecha;
                }

                parrafoTextoLinea = new Paragraph(textoLinea);
                parrafoTextoLinea.Font.Size = 10F;

                celdaDatosVenta.AddElement(parrafoTextoLinea);

                parrafoTotalLinea = new Paragraph(saleTotal.ToString());
                parrafoTotalLinea.Font.Size = 10F;

                celdaTotalVenta.AddElement(parrafoTotalLinea);

                tabFot.AddCell(celdaDatosVenta);
                tabFot.AddCell(celdaTotalVenta);

                total = total + saleTotal;

            }

            PdfPCell celdaFechaVenta = new PdfPCell();
            celdaFechaVenta.Colspan = 3;

            Paragraph parrafoFechaVenta = new Paragraph("Datos entre fechas: " + menorFecha.ToString(defaulDateFormat) + " y " + mayorFecha.ToString(defaulDateFormat));
            parrafoFechaVenta.Font.Size = 10F;
            parrafoFechaVenta.Alignment = Element.TITLE;

            celdaFechaVenta.AddElement(parrafoFechaVenta);

            tabFot.AddCell(celdaFechaVenta);

            return total;
        }

        private void generarTitulo(PdfPTable tabFot, String titulo)
        {
            PdfPCell celdaDatosComplementarios = new PdfPCell();
            celdaDatosComplementarios.Colspan = 3;

            Paragraph paragraph = new Paragraph(titulo);
            paragraph.Alignment = Element.TITLE;
            paragraph.Font.Size = 16F;
            paragraph.SpacingAfter = 10F;

            celdaDatosComplementarios.AddElement(paragraph);

            tabFot.AddCell(celdaDatosComplementarios);
        }

        private string generarPDFVentasNombreDeArchivo()
        {
            String fileName = "reporteDeVentas" + DateTime.Now.ToString("yyyyMMdd-HHmmss") +".pdf";
            return "C:\\Users\\Leonora\\Desktop\\" + fileName;
        }


        private void generarDatosComplementariosVoucher(PdfPTable tabFot, Voucher voucher)
        {
            PdfPCell celdaDatosComplementarios = new PdfPCell();
            celdaDatosComplementarios.Colspan = 3;

            Paragraph paragraph = new Paragraph("Cantidad total de pasajeros: " + voucher.Cantindad);

            celdaDatosComplementarios.AddElement(paragraph);

            tabFot.AddCell(celdaDatosComplementarios);
        }

        private void generarPieDePagina(PdfPTable tabFot, String detalle)
        {
            PdfPCell celdaEstadoVaucher = new PdfPCell();
            celdaEstadoVaucher.Colspan = 2;

            Paragraph textoEstadoVoucher = new Paragraph(detalle);
            textoEstadoVoucher.Font.Size = 10F;

            celdaEstadoVaucher.AddElement(textoEstadoVoucher);

            tabFot.AddCell(celdaEstadoVaucher);

            PdfPCell celdaFirma = new PdfPCell();
            celdaFirma.FixedHeight = 60F;
            celdaFirma.Colspan = 1;

            Paragraph textoFirma = new Paragraph("Firma autorizadora: ");
            textoFirma.Alignment = Element.ALIGN_TOP;
            textoFirma.Font.Size = 6F;


            celdaFirma.AddElement(textoFirma);

            tabFot.AddCell(celdaFirma);
        }


        private void generarContenidoVoucher(PdfPTable tabFot, Voucher voucher)
        {
            PdfPCell contenido = new PdfPCell();
            contenido.Colspan = 3;

            Paragraph paragraph = new Paragraph("Contenido");

            contenido.AddElement(paragraph);

            tabFot.AddCell(contenido);
        }

        private void generarDatosUsuarioVoucher(PdfPTable tabFot, Voucher voucher)
        {
            PdfPCell celdaDatosCliente = new PdfPCell();
            celdaDatosCliente.Colspan = 3;

            Paragraph paragraph = new Paragraph("Sr/Sra: " + voucher.Cliente.Apellido + ", " + voucher.Cliente.Nombre + ". DNI: " + voucher.Cliente.Dni + " Dirección: " + voucher.Cliente.Domicilio);

            celdaDatosCliente.AddElement(paragraph);

            tabFot.AddCell(celdaDatosCliente);
        }

        private string getPDFFilePath(Voucher voucher)
        {

            String fileName = voucher.Cliente.Dni + "-" + voucher.Producto.Nombre +".pdf";
            return "C:\\Users\\Leonora\\Desktop\\" + fileName;
        }

        private void generarEncabezado(PdfPTable table, String detalle)
        {
            PdfPCell imagenEncabezadoCell = new PdfPCell();
            imagenEncabezadoCell.PaddingBottom = 1f;
            imagenEncabezadoCell.PaddingTop = 1f;
            imagenEncabezadoCell.PaddingLeft = 1f;


            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("Resources/LogoOEV.png");
            logo.ScalePercent(23f);
            imagenEncabezadoCell.AddElement(logo);

            table.AddCell(imagenEncabezadoCell);

            PdfPCell celdaDatosCompania = new PdfPCell();

            Paragraph textoDatosCompania = new Paragraph("Lavalle 715 Piso  9 Of A. \n CPA 1047 \n Teléfono: 52565200 \n Buenos Aires. Argentina");
            textoDatosCompania.Font.Size = 8F;
            celdaDatosCompania.AddElement(textoDatosCompania);

            table.AddCell(celdaDatosCompania);

            PdfPCell celdaVoucherDatos = new PdfPCell();

            Paragraph textoDatosVaoucher = new Paragraph(detalle);

            celdaVoucherDatos.AddElement(textoDatosVaoucher);
            
            table.AddCell(celdaVoucherDatos);
        }
    }
}
