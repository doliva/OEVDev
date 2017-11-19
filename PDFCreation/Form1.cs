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

        private Random rnd = new Random();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PDFUtils pdfUtils = new PDFUtils();

                Voucher voucher = new Voucher();
                voucher.Numero = "112233445566";
                voucher.Estado = "Pagado";
                voucher.Cantindad = 2;

                Producto producto = new Producto();
                producto.Nombre = "UnProductoooooo";
                
                voucher.Producto = producto;

                Cliente cliente = new Cliente();
                cliente.Dni = "666";
                cliente.Apellido = "Ponce";
                cliente.Nombre = "Natalia Alejandra";
                cliente.Domicilio = "Mateo Churich 1234";


                voucher.Cliente = cliente;

                String filePath = pdfUtils.crearPDFVoucher(voucher);

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

        }

        private void generarPDFVentas_Click(object sender, EventArgs e)
        {
            try
            {

                List<Venta> ventas = new List<Entities.Venta>();

                int elements = rnd.Next(1, 7);

                for (int i = 0; i < elements; i++)
                {

                    ventas.Add(CreateVenta());

                }

                PDFUtils pdfUtils = new PDFUtils();

                String filePath = pdfUtils.crearPDFVentas(ventas);

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

        }

        private Venta CreateVenta()
        {
            Cliente cliente1 = new Cliente();
            cliente1.Apellido = randomString((Int16)rnd.Next(6, 12));
            cliente1.Nombre = randomString((Int16)rnd.Next(6, 12));
            cliente1.Celular = "celularCliente1";
            cliente1.Ciudad = "ciudadCliente1";
            cliente1.Dni = rnd.Next(10000, 100000).ToString();
            cliente1.Domicilio = "domicilioCliente1";
            cliente1.Email = "emailCliente1";
            cliente1.Estado = 1;

            Factura factura1 = new Factura();
            factura1.Estado = "estadoFactura1";
            factura1.Fecha = new DateTime();
            factura1.IdFactura = (Int16) rnd.Next(10, 1000) ;
            factura1.Importe = Convert.ToDecimal(rnd.NextDouble());
            factura1.TipoPago = "Efectivo";

            factura1.Items = new List<DetalleFactura>();

            factura1.Items.AddRange(generarItemsFactura());

            Venta venta1 = new Venta();
            venta1.Codigo = randomString((Int16)rnd.Next(6, 12));
            venta1.ClienteVenta = cliente1;
            venta1.FacturaVenta = factura1;
            return venta1;
        }

        private List<DetalleFactura> generarItemsFactura()
        {
            List<DetalleFactura> dfList = new List<DetalleFactura>();

            int elements = rnd.Next(1, 7);

            DetalleFactura detalleFactura;

            for (int i = 0; i < elements; i++)
            {
                detalleFactura = new DetalleFactura();

                detalleFactura.Cantidad = (Int16)rnd.Next(1, 7);

                detalleFactura.Detalle = randomString((Int16)rnd.Next(6, 12));

                detalleFactura.IdDetalle = (Int16)rnd.Next(10, 1000);

                detalleFactura.Precio = Convert.ToDecimal(rnd.NextDouble());

                dfList.Add(detalleFactura);


            }

            return dfList;
        }

        private string randomString(int length)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var builder = new StringBuilder();


            for (var i = 0; i < length; i++)
            {
                var c = pool[rnd.Next(0, pool.Length)];
                builder.Append(c);
            }

            return builder.ToString();
        }

    }
}
