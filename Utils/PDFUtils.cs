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
        public String createPDFVoucher(Voucher voucher)
        {

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            String filePath = getPDFFilePath(voucher);
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);
            doc.Open();

            PdfPTable tabFot = new PdfPTable(3);

            tabFot.TotalWidth = doc.PageSize.Width - 50F;

            generarEncabezadoVoucher(tabFot, voucher);

            generarDatosUsuarioVoucher(tabFot, voucher);

            generarDatosComplementariosVoucher(tabFot, voucher);

            generarContenidoVoucher(tabFot, voucher);

            generarPieDePagina(tabFot, voucher);


            doc.Add(tabFot);

            doc.Close();

            return filePath;

            //Console.WriteLine("Table columns=" + tabFot.NumberOfColumns);

            /*

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("Resources/LogoOEV.png");
            logo.ScalePercent(25f);
            logo.SetAbsolutePosition(doc.PageSize.Width - 150f, doc.PageSize.Height - 100f);

            doc.Add(logo);

            PdfContentByte cb = wri.DirectContent;


            // we tell the ContentByte we're ready to draw text
            cb.BeginText();

            cb.SetTextMatrix(10, 20);
            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false), 10);
            cb.ShowText("Dirección falsa 123 Piso 1 Oficina 666 \n Código Postal 1234 \n Teléfono 1533881234 \n Buenos Aires - Argentina");


            // we tell the contentByte, we've finished drawing text
            cb.EndText();

            Paragraph paragraph = new Paragraph("This is my first lines using paragraph. \n Te amo MUCHOOOOOOO");
            doc.Add(paragraph);
            */
            

        }

        private void generarDatosComplementariosVoucher(PdfPTable tabFot, Voucher voucher)
        {
            PdfPCell celdaDatosComplementarios = new PdfPCell();
            celdaDatosComplementarios.Colspan = 3;

            Paragraph paragraph = new Paragraph("Cantidad total de pasajeros: " + voucher.Cantindad);

            celdaDatosComplementarios.AddElement(paragraph);

            tabFot.AddCell(celdaDatosComplementarios);
        }

        private void generarPieDePagina(PdfPTable tabFot, Voucher voucher)
        {
            PdfPCell celdaEstadoVaucher = new PdfPCell();
            celdaEstadoVaucher.Colspan = 2;

            Paragraph textoEstadoVoucher = new Paragraph("El estado de este voucher es: " + voucher.Estado);

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

        private void generarEncabezadoVoucher(PdfPTable table, Voucher voucher)
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

            Paragraph textoDatosVaoucher = new Paragraph("Voucher: " + voucher.Numero);

            celdaVoucherDatos.AddElement(textoDatosVaoucher);

            table.AddCell(celdaVoucherDatos);
        }
    }
}
