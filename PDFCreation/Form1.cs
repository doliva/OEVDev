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

                Cliente cliente1 = new Cliente();
                cliente1.Apellido = "apellidoCliente1";
                cliente1.Celular = "celularCliente1";
                cliente1.Ciudad = "ciudadCliente1";
                cliente1.Dni = "dniCliente1";
                cliente1.Domicilio = "domicilioCliente1";
                cliente1.Email = "emailCliente1";
                cliente1.Estado = 1;

                Factura factura1 = new Factura();
                factura1.Estado = "estadoFactura1";
                factura1.Fecha = new DateTime();
                factura1.IdFactura = 1;
                factura1.Importe = new Decimal(33.12);
                factura1.TipoPago = "Efectivo";

                factura1.Items = new List<DetalleFactura>();

                DetalleFactura item1 = new DetalleFactura();
                item1.Cantidad = 3;
                item1.Detalle = "Una cosa que se vendió1";
                item1.IdDetalle = 1;
                item1.Precio = new Decimal(293.12);

                factura1.Items.Add(item1);

                DetalleFactura item2 = new DetalleFactura();
                item2.Cantidad = 5;
                item2.Detalle = "Una cosa linda que se vendió1";
                item2.IdDetalle = 2;
                item2.Precio = new Decimal(23.84);

                factura1.Items.Add(item2);

                Venta venta1 = new Venta();
                venta1.Codigo = "1122334455";
                venta1.ClienteVenta = cliente1;
                venta1.FacturaVenta = factura1;

                ventas.Add(venta1);

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

    }
}
