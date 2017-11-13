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

            PdfPTable tabFot = new PdfPTable(new float[] { 3F });
            //tabFot.SpacingAfter = 10F;
            PdfPCell cell;
            tabFot.TotalWidth = doc.PageSize.Width -50F;
            
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("Resources/LogoOEV.png");
            logo.ScalePercent(23f);

            cell = new PdfPCell(logo);
            cell.Colspan = 3; 
            tabFot.AddCell(cell);
            //tabFot.WriteSelectedRows(0, -1, 10, doc.Top, wri.DirectContent);

            doc.Add(tabFot);

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
            
            doc.Close();

            return filePath ;
        }

        private string getPDFFilePath(Voucher voucher)
        {

            String fileName = voucher.Cliente.Dni + "-" + voucher.Producto.Nombre +".pdf";
            return "C:\\Users\\Natalia\\Desktop\\" + fileName;
        }

    }
}
