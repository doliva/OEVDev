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

        private static String cabeceraFechaFormato = "yyyy/MM/dd HH:mm";

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

            generarEncabezado(tabFot, "Generado el: " + DateTime.Now.ToString(cabeceraFechaFormato));

            generarTitulo(tabFot, "Reporte de ventas");

            Decimal total = generarContenidoReporteVentas(tabFot, ventas);

            generarReporteDeVentasTotal(tabFot, total);

            generarPieDePagina(tabFot, "Pie de pagina");

            doc.Add(tabFot);

            doc.Close();

            return agregarNumeroDePagina(filePath, generarNombreDeArchivoPDFVentas());



        }


        public String crearPDFInscripcion(ClienteActividad clienteActividad)
        {

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            String filePath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);
            doc.Open();

            PdfPTable tabFot = new PdfPTable(3);

            generarEncabezado(tabFot, "Generado el: " + DateTime.Now.ToString(cabeceraFechaFormato));

            generarTitulo(tabFot, "Constancia de Inscripción");

            generarContenidoCliente(tabFot, clienteActividad.Cliente);

            generarContenidoActividad(tabFot, clienteActividad.Actividades);

            generarPieDePagina(tabFot, "Pie de pagina");

            doc.Add(tabFot);

            doc.Close();


            return agregarNumeroDePagina(filePath, generarNombreDeArchivoPDFInscripcion());



        }

        private void generarContenidoActividad(PdfPTable tabFot, List<Actividad> list)
        {
            PdfPCell informacionActividad = new PdfPCell();
            informacionActividad.Colspan = 3;

            Paragraph informacionActividadParagraph = new Paragraph("Datos inscripción");
            informacionActividadParagraph.Alignment = Element.TITLE;
            informacionActividadParagraph.Font = FontFactory.GetFont("Verdana", 12F, Font.BOLD);
            informacionActividadParagraph.SpacingAfter = 8f;

            informacionActividad.AddElement(informacionActividadParagraph);

            tabFot.AddCell(informacionActividad);

            float sparcingAfter = 4f;

            foreach (Actividad actividad in list)
            {
                PdfPCell celdaNombre = new PdfPCell();

                Paragraph nombreParagraph = new Paragraph(actividad.Nombre);
                nombreParagraph.SpacingAfter = sparcingAfter;
                nombreParagraph.Alignment = Element.TITLE;
                nombreParagraph.Font = FontFactory.GetFont("Verdana", 8F, Font.NORMAL);

                celdaNombre.AddElement(nombreParagraph);
                
                tabFot.AddCell(celdaNombre);

                PdfPCell celdaDescripcion = new PdfPCell();
                celdaDescripcion.Colspan = 2;

                Paragraph descripcionParagraph = new Paragraph(actividad.Descripcion);
                descripcionParagraph.Alignment = Element.TITLE;
                descripcionParagraph.SpacingAfter = sparcingAfter;
                descripcionParagraph.Font = FontFactory.GetFont("Verdana", 8F, Font.NORMAL);

                celdaDescripcion.AddElement(descripcionParagraph);

                tabFot.AddCell(celdaDescripcion);

                PdfPCell celdaDias = new PdfPCell();

                Paragraph diasParagraph = new Paragraph(actividad.Dias);
                diasParagraph.Alignment = Element.TITLE;
                diasParagraph.SpacingAfter = sparcingAfter;
                diasParagraph.Font = FontFactory.GetFont("Verdana", 8F, Font.NORMAL);

                celdaDias.AddElement(diasParagraph);

                tabFot.AddCell(celdaDias);

                PdfPCell celdaHorarios = new PdfPCell();

                Paragraph horariosParagraph = new Paragraph(actividad.Horarios);
                horariosParagraph.Alignment = Element.TITLE;
                horariosParagraph.SpacingAfter = sparcingAfter;
                horariosParagraph.Font = FontFactory.GetFont("Verdana", 8F, Font.NORMAL);

                celdaHorarios.AddElement(horariosParagraph);

                tabFot.AddCell(celdaHorarios);

                PdfPCell celdaDificultad = new PdfPCell();

                Paragraph dificultadParagraph = new Paragraph(actividad.Dificultad);
                dificultadParagraph.Alignment = Element.TITLE;
                dificultadParagraph.SpacingAfter = sparcingAfter;
                dificultadParagraph.Font = FontFactory.GetFont("Verdana", 8F, Font.NORMAL);

                celdaDificultad.AddElement(dificultadParagraph);

                tabFot.AddCell(celdaDificultad);
            }
        }

        private void generarContenidoCliente(PdfPTable tabFot, Cliente cliente)
        {

            PdfPCell informacionCliente = new PdfPCell();
            informacionCliente.Colspan = 3;

            Paragraph informacionClienteParagraph = new Paragraph("Datos Usuario");
            informacionClienteParagraph.Alignment = Element.TITLE;
            informacionClienteParagraph.Font = FontFactory.GetFont("Verdana", 12F, Font.BOLD);
            informacionClienteParagraph.SpacingAfter = 8f;

            informacionCliente.AddElement(informacionClienteParagraph);

            tabFot.AddCell(informacionCliente);

            // Linea 1

            PdfPCell linea1Columna1 = new PdfPCell();

            Paragraph nombreParagraph = new Paragraph("Nombre");
            nombreParagraph.Alignment = Element.TITLE;
            nombreParagraph.Font = FontFactory.GetFont("Verdana", 8F, Font.BOLD);

            linea1Columna1.AddElement(nombreParagraph);

            Paragraph nombreParagraphValor = new Paragraph(cliente.Nombre);
            nombreParagraphValor.Alignment = Element.TITLE;
            nombreParagraphValor.Font = FontFactory.GetFont("Verdana", 8F, Font.NORMAL);

            linea1Columna1.AddElement(nombreParagraphValor);

            tabFot.AddCell(linea1Columna1);

            PdfPCell linea1Columna2 = new PdfPCell();

            Paragraph apellidoParagraph = new Paragraph("Apellido");
            apellidoParagraph.Alignment = Element.TITLE;
            apellidoParagraph.Font = FontFactory.GetFont("Verdana", 8F, Font.BOLD);

            linea1Columna2.AddElement(apellidoParagraph);

            Paragraph apellidoParagraphValor = new Paragraph(cliente.Apellido);
            apellidoParagraphValor.Alignment = Element.TITLE;
            apellidoParagraphValor.Font = FontFactory.GetFont("Verdana", 8F, Font.NORMAL);

            linea1Columna2.AddElement(apellidoParagraphValor);

            tabFot.AddCell(linea1Columna2);

            PdfPCell linea1Columna3 = new PdfPCell();

            Paragraph dniParagraph = new Paragraph("DNI");
            dniParagraph.Alignment = Element.TITLE;
            dniParagraph.Font = FontFactory.GetFont("Verdana", 8F, Font.BOLD);

            linea1Columna3.AddElement(dniParagraph);

            Paragraph dniParagraphValor = new Paragraph(cliente.Dni);
            dniParagraphValor.Alignment = Element.TITLE;
            dniParagraphValor.Font = FontFactory.GetFont("Verdana", 8F, Font.NORMAL);

            linea1Columna3.AddElement(dniParagraphValor);

            tabFot.AddCell(linea1Columna3);
            
            

            //Linea 2

            PdfPCell linea2Columna1 = new PdfPCell();

            Paragraph domicilioParagraph = new Paragraph("Domicilio");
            domicilioParagraph.Alignment = Element.TITLE;
            domicilioParagraph.Font = FontFactory.GetFont("Verdana", 8F, Font.BOLD);

            linea2Columna1.AddElement(domicilioParagraph);

            Paragraph domicilioParagraphValor = new Paragraph(cliente.Domicilio);
            domicilioParagraphValor.Alignment = Element.TITLE;
            domicilioParagraphValor.Font = FontFactory.GetFont("Verdana", 8F, Font.NORMAL);

            linea2Columna1.AddElement(domicilioParagraphValor);

            tabFot.AddCell(linea2Columna1);

            PdfPCell linea2Columna2 = new PdfPCell();

            Paragraph ciudadParagraph = new Paragraph("Ciudad");
            ciudadParagraph.Alignment = Element.TITLE;
            ciudadParagraph.Font = FontFactory.GetFont("Verdana", 8F, Font.BOLD);

            linea2Columna2.AddElement(ciudadParagraph);

            Paragraph ciudadParagraphValor = new Paragraph(cliente.Ciudad);
            ciudadParagraphValor.Alignment = Element.TITLE;
            ciudadParagraphValor.Font = FontFactory.GetFont("Verdana", 8F, Font.NORMAL);

            linea2Columna2.AddElement(ciudadParagraphValor);

            tabFot.AddCell(linea2Columna2);

            PdfPCell linea2Columna3 = new PdfPCell();

            Paragraph emailParagraph = new Paragraph("Email");
            emailParagraph.Alignment = Element.TITLE;
            emailParagraph.Font = FontFactory.GetFont("Verdana", 8F, Font.BOLD);

            linea2Columna3.AddElement(emailParagraph);

            Paragraph emailParagraphValor = new Paragraph(cliente.Email);
            emailParagraphValor.Alignment = Element.TITLE;
            emailParagraphValor.Font = FontFactory.GetFont("Verdana", 8F, Font.NORMAL);

            linea2Columna3.AddElement(emailParagraphValor);

            tabFot.AddCell(linea2Columna3);


            //Linea 3

            PdfPCell linea3Columna1 = new PdfPCell();

            Paragraph telefonoParagraph = new Paragraph("Telefono");
            telefonoParagraph.Alignment = Element.TITLE;
            telefonoParagraph.Font = FontFactory.GetFont("Verdana", 8F, Font.BOLD);

            linea3Columna1.AddElement(telefonoParagraph);

            Paragraph telefonoParagraphValor = new Paragraph(cliente.Telefono);
            telefonoParagraphValor.Alignment = Element.TITLE;
            telefonoParagraphValor.Font = FontFactory.GetFont("Verdana", 8F, Font.NORMAL);

            linea3Columna1.AddElement(telefonoParagraphValor);

            tabFot.AddCell(linea3Columna1);

            PdfPCell linea3Columna2 = new PdfPCell();

            Paragraph celularParagraph = new Paragraph("Celular");
            celularParagraph.Alignment = Element.TITLE;
            celularParagraph.Font = FontFactory.GetFont("Verdana", 8F, Font.BOLD);

            linea3Columna2.AddElement(celularParagraph);

            Paragraph celularParagraphValor = new Paragraph(cliente.Celular);
            celularParagraphValor.Alignment = Element.TITLE;
            celularParagraphValor.Font = FontFactory.GetFont("Verdana", 8F, Font.NORMAL);

            linea3Columna2.AddElement(celularParagraphValor);

            tabFot.AddCell(linea3Columna2);

            PdfPCell linea3Columna3 = new PdfPCell();

            /*
            Paragraph emailParagraph = new Paragraph("Email");
            emailParagraph.Alignment = Element.TITLE;
            emailParagraph.Font = FontFactory.GetFont("Verdana", 8F, Font.BOLD);

            linea2Columna3.AddElement(emailParagraph);

            Paragraph emailParagraphValor = new Paragraph(cliente.Email);
            emailParagraphValor.Alignment = Element.TITLE;
            emailParagraphValor.Font = FontFactory.GetFont("Verdana", 8F, Font.NORMAL);

            linea2Columna3.AddElement(emailParagraphValor);
            */
            tabFot.AddCell(linea3Columna3);

        }


        private String agregarNumeroDePagina(String filePath, String filePathFinal)
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

        private string generarNombreDeArchivoPDFVentas()
        {
            String fileName = "reporteDeVentas" + DateTime.Now.ToString("yyyyMMdd-HHmmss") +".pdf";
            return "C:\\Users\\Leonora\\Desktop\\" + fileName;
        }

        private string generarNombreDeArchivoPDFInscripcion()
        {
            String fileName = "reporteInscreipcion" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".pdf";
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
            textoDatosVaoucher.Alignment = Element.ALIGN_BOTTOM;
            //paragraph.Font.Size = 16F;
            textoDatosVaoucher.Font = FontFactory.GetFont("Verdana",9F, Font.NORMAL);

            celdaVoucherDatos.AddElement(textoDatosVaoucher);
            
            table.AddCell(celdaVoucherDatos);
        }
    }
}
