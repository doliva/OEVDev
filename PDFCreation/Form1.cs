using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Utils;
using Entities;

namespace PDFCreation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PDFUtils pdfUtils = new PDFUtils();

                Voucher voucher = new Voucher();

                Producto producto = new Producto();
                producto.Nombre = "UnProductoooooo";

                voucher.Producto = producto;

                Cliente cliente = new Cliente();
                cliente.Dni = "666";

                voucher.Cliente = cliente;

                String filePath = pdfUtils.createPDFVoucher(voucher);

                if (!String.IsNullOrEmpty(filePath))
                {
                    System.Diagnostics.Process.Start(@filePath);
                }

            }
            catch (DirectoryNotFoundException exception)
            {
                MessageBox.Show("No es posible hallar el directorio determinado, por favor revise la configuración", "Alerta");
            }
            catch (IOException exception)
            {
                MessageBox.Show("No es posible crear el archivo, ya existe un archivo con el mismo nombre", "Alerta");
            }
            catch (Exception exception)
            {
                MessageBox.Show("No es posible generar el archivo. Póngase en contacto con el administrador", "Alerta");
            }

            /*
            PDFUtils pdfUtils = new PDFUtils();

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("C:\\Users\\Natalia\\Desktop\\Test.pdf", FileMode.Create));
            doc.Open();

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("Resources/LogoOEV.png");
            logo.ScalePercent(25f);
            logo.SetAbsolutePosition(doc.PageSize.Width -150f, doc.PageSize.Height -100f);

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
            doc.Close();
            */
        }
    }
}
