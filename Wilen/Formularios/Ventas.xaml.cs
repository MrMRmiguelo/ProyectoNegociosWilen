using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proyecto_Inmobiliaria.Formularios
{
    /// <summary>
    /// Lógica de interacción para Ventas.xaml
    /// </summary>
    public partial class Ventas : Window
    {

        SqlConnection sqlconnection;
        public Ventas()
        {
            InitializeComponent();
            FechaSys.Text = DateTime.Now.ToString("dd/MM/yyyy");
            string connectionString = @"server=Data Source=LAPTOP-61P3GV5K\SQLEXPRESS;Initial Catalog=WILEN;Integrated Security=True";
            sqlconnection = new SqlConnection(connectionString);

            Mostrar();
            ListarArticulos();
            ListarClientes();
            ListarComprobantes();
            ListarVentas();
            double isv = Mostrar_ISC() * 100;
            txtISV.Text = string.Format("{0}%", isv);
            txtISV.IsEnabled = false;
        }

        private void ListarVentas()
        {
            try
            {
                // El query ha realizar en la BD
                string query = "SELECT * FROM Venta";

                // SqlDataAdapter es una interfaz entre las tablas y los objetos utilizables en C#
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {
                    // Objecto en C# que refleja una tabla de una BD
                    DataTable tablaP = new DataTable();

                    // Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaP);

                    // ¿Cuál información de la tabla en el DataTable debería se desplegada en nuestro ListBox?
                    dgllenar.DisplayMemberPath = "Id_Venta";
                    dgllenar.DisplayMemberPath = "Id_Empleado";
                    dgllenar.DisplayMemberPath = "Id_Cliente";
                    dgllenar.DisplayMemberPath = "Id_Comprobante";
                    dgllenar.DisplayMemberPath = "Serie";
                    dgllenar.DisplayMemberPath = "Id_ISV";
                    dgllenar.DisplayMemberPath = "Fecha";
                    // ¿Qué valor debe ser entregado cuando un elemento de nuestro ListBox es seleccionado?
                    dgllenar.SelectedValuePath = "[Id_Venta]";
                    // ¿Quién es la referencia de los datos para el ListBox (popular)
                    dgllenar.ItemsSource = tablaP.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ListarComprobantes()
        {
            try
            {
                // El query ha realizar en la BD
                string query = "SELECT * FROM Comprobante";

                // SqlDataAdapter es una interfaz entre las tablas y los objetos utilizables en C#
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {
                    // Objecto en C# que refleja una tabla de una BD
                    DataTable tablaA = new DataTable();

                    // Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaA);

                    // ¿Cuál información de la tabla en el DataTable debería se desplegada en nuestro ListBox?
                    cbComprobante.DisplayMemberPath = "Tipo";
                    // ¿Qué valor debe ser entregado cuando un elemento de nuestro ListBox es seleccionado?
                    cbComprobante.SelectedValuePath = "Id_Comprobante";
                    // ¿Quién es la referencia de los datos para el ListBox (popular)
                    cbComprobante.ItemsSource = tablaA.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ListarArticulos()
        {
            try
            {
                // El query ha realizar en la BD
                string query = "SELECT * FROM Articulo";

                // SqlDataAdapter es una interfaz entre las tablas y los objetos utilizables en C#
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {
                    // Objecto en C# que refleja una tabla de una BD
                    DataTable tablaA = new DataTable();

                    // Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaA);

                    // ¿Cuál información de la tabla en el DataTable debería se desplegada en nuestro ListBox?
                    cbArticulo.DisplayMemberPath = "Nombre";
                    // ¿Qué valor debe ser entregado cuando un elemento de nuestro ListBox es seleccionado?
                    cbArticulo.SelectedValuePath = "Id_Articulo";
                    // ¿Quién es la referencia de los datos para el ListBox (popular)
                    cbArticulo.ItemsSource = tablaA.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ListarClientes()
        {
            try
            {
                // El query ha realizar en la BD
                string query = "SELECT * FROM Cliente";

                // SqlDataAdapter es una interfaz entre las tablas y los objetos utilizables en C#
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {
                    // Objecto en C# que refleja una tabla de una BD
                    DataTable tablaP = new DataTable();

                    // Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaP);

                    // ¿Cuál información de la tabla en el DataTable debería se desplegada en nuestro ListBox?
                    cbCliente.DisplayMemberPath = "Nombre";
                    // ¿Qué valor debe ser entregado cuando un elemento de nuestro ListBox es seleccionado?
                    cbCliente.SelectedValuePath = "Id_Cliente";
                    // ¿Quién es la referencia de los datos para el ListBox (popular)
                    cbCliente.ItemsSource = tablaP.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private double Mostrar_ISC()
        {
            double ISV = 0;

            try
            {
                string query = "SELECT ISV FROM ISV";

                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                // Reemplazar el parámetro con su valor respectivo

                sqlCommand.ExecuteNonQuery();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    string x;
                    x = reader["ISV"].ToString();
                    ISV = double.Parse(x);

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                sqlconnection.Close();
            }



            return ISV;
        }
        private void Mostrar()
        {
            try
            {

                string query = "SELECT * FROM Venta";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {

                    DataTable tabla1 = new DataTable();


                    sqlDataAdapter.Fill(tabla1);

                    dgllenar.DisplayMemberPath = "Id_Venta";
                    dgllenar.DisplayMemberPath = "Id_Empleado";
                    dgllenar.DisplayMemberPath = "Id_Cliente";
                    dgllenar.DisplayMemberPath = "Id_Comprobante";
                    dgllenar.DisplayMemberPath = "Id_Detalle_Venta";
                    dgllenar.DisplayMemberPath = "Serie";
                    dgllenar.DisplayMemberPath = "Correlativo";
                    dgllenar.DisplayMemberPath = "Id_ISV";
                    dgllenar.DisplayMemberPath = "Fecha";

                    dgllenar.SelectedValuePath = "Id_Venta";
                    dgllenar.ItemsSource = tabla1.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        private void BtnListado_Click(object sender, RoutedEventArgs e)
        {


            PanelListado.Visibility = Visibility.Visible;
            PanelMantenimientoBotones.Visibility = Visibility.Hidden;
            PanelMantenimientoPart1.Visibility = Visibility.Hidden;
            PanelMantenimientoPart2.Visibility = Visibility.Hidden;
            PanelMantenimientoPart3.Visibility = Visibility.Hidden;
            PanelMantenimientoPart4.Visibility = Visibility.Hidden;
        }

        private void BtnMantenimiento_Click(object sender, RoutedEventArgs e)
        {
            PanelMantenimientoPart1.Visibility = Visibility.Visible;
            PanelMantenimientoPart2.Visibility = Visibility.Visible;
            PanelMantenimientoPart3.Visibility = Visibility.Visible;
            PanelMantenimientoPart4.Visibility = Visibility.Visible;
            PanelMantenimientoBotones.Visibility = Visibility.Visible;
            PanelListado.Visibility = Visibility.Hidden;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {

            this.Close();




        }
        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            btnRest.Visibility = Visibility.Visible;
            btnMax.Visibility = Visibility.Collapsed;
            PanelListado.Width = 1200;
        }

        private void BtnRest_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            btnMax.Visibility = Visibility.Visible;
            btnRest.Visibility = Visibility.Collapsed;
            PanelListado.Width = 1000;
        }

        #region  Efectos Brillo al Pasar el Mouse

        private void BtnListado_MouseEnter(object sender, MouseEventArgs e)
        {

            btnListado.Background = Brushes.Blue;

        }

        private void BtnListado_MouseLeave(object sender, MouseEventArgs e)
        {
            btnListado.Background = new SolidColorBrush(Color.FromRgb(27, 100, 207));
        }

        private void BtnCerrar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnCerrar.Background = new SolidColorBrush(Color.FromRgb(27, 100, 207));
        }



        private void BtnCerrar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCerrar.Background = Brushes.Crimson;
        }



        private void BtnMax_MouseEnter(object sender, MouseEventArgs e)
        {
            btnMax.Background = Brushes.Blue;
        }

        private void BtnMax_MouseLeave(object sender, MouseEventArgs e)
        {
            btnMax.Background = new SolidColorBrush(Color.FromRgb(27, 100, 207));
        }


        private void BtnMantenimiento_MouseEnter(object sender, MouseEventArgs e)
        {
            btnMantenimiento.Background = Brushes.Blue;
        }

        private void BtnMantenimiento_MouseLeave(object sender, MouseEventArgs e)
        {
            btnMantenimiento.Background = new SolidColorBrush(Color.FromRgb(27, 100, 207));
        }

        private void BtnRest_MouseEnter(object sender, MouseEventArgs e)
        {

            btnMantenimiento.Background = Brushes.Blue;
        }


        private void BtnRest_MouseLeave(object sender, MouseEventArgs e)
        {
            btnMantenimiento.Background = new SolidColorBrush(Color.FromRgb(27, 100, 207));
        }

        #endregion

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //DragMove();
        }

        private void BtnLBuscarVentas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "SELECT * FROM Categoria WHERE Fecha = @fecha ";



                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);


                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {

                    sqlCommand.Parameters.AddWithValue("@fecha", DpInicialFecha.Text);


                    DataTable tabla = new DataTable();


                    sqlDataAdapter.Fill(tabla);


                    dgllenar.DisplayMemberPath = "Id_Venta";
                    // dgllenar.DisplayMemberPath = "Id_Empleado";
                    dgllenar.DisplayMemberPath = "Id_Cliente";
                    dgllenar.DisplayMemberPath = "Id_Comprobante";
                    dgllenar.DisplayMemberPath = "Id_Detalle_Venta";
                    dgllenar.DisplayMemberPath = "Serie";
                    dgllenar.DisplayMemberPath = "Correlativo";
                    dgllenar.DisplayMemberPath = "Id_ISV";
                    dgllenar.DisplayMemberPath = "Fecha";

                    dgllenar.SelectedValuePath = "Id_Venta";
                    dgllenar.ItemsSource = tabla.DefaultView;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnLEliminarVentas_Click(object sender, RoutedEventArgs e)
        {
            if (DpInicialFecha.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar una categoria");
                DpInicialFecha.Focus();
            }
            else
            {
                try
                {
                    string query = "DELETE Venta WHERE Nombre =  @nombre";

                    SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);



                    sqlconnection.Open();

                    sqlCommand.Parameters.AddWithValue("@nombre", dgllenar.SelectedValue);
                    sqlCommand.ExecuteNonQuery();




                    MessageBox.Show("Se ha borrado exitosamente");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    sqlconnection.Close();
                    Mostrar();
                }
            }
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            cbArticulo.SelectedValue = null;
            txtCantidadVentas.Text = string.Empty;
            cbCliente.SelectedValue = null;
            txtDescuento.Text = string.Empty;
            txtISV.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtDetalle.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            cbComprobante.SelectedValue = null;
            txttotal.Text = String.Format("0");
            dgDetalle.Columns.Clear();
            btnGuardar.IsEnabled = true;
            btnAgregar.IsEnabled = false;
            Mostrar();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO Venta(Id_Cliente, Id_Comprobante,Serie, Id_ISV) VALUES( @Id_Cliente, @Id_Comprobante,@serie,@Id_ISV)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);

                // Abrir la conexión
                sqlconnection.Open();

                // Reemplazar el parámetro con su valor respectivo

                sqlCommand.Parameters.AddWithValue("@Id_Cliente", cbCliente.SelectedValue.ToString());
                sqlCommand.Parameters.AddWithValue("@Id_Comprobante", cbComprobante.SelectedValue.ToString());
                sqlCommand.Parameters.AddWithValue("@Id_ISV", "1");
                sqlCommand.Parameters.AddWithValue("@serie", txtNumero.Text.ToString());


                // sqlCommand.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("DD/MM/YY"));
                // Ejecutamos el query de inserción
                // https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand.executescalar



                if (sqlCommand.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("La operación se ha completado correctamente");
                }
                else
                {
                    MessageBox.Show("La operación No se ha completado correctamente");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlconnection.Close();
                // Actualizar el ListBox de Articulos
                btnAgregar.IsEnabled = true;
                btnGuardar.IsEnabled = false;
                ListarVentas();
            }

        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            cbArticulo.SelectedValue = null;
            txtCantidadVentas.Text = string.Empty;
            cbCliente.SelectedValue = null;
            txtDescuento.Text = string.Empty;
            txtISV.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtPrecio.Text = String.Empty;
            txtDetalle.Text = String.Empty;

            cbComprobante.SelectedValue = null;
            txttotal.Text = String.Format("0");
            dgDetalle.Columns.Clear();
            btnGuardar.IsEnabled = true;
            btnAgregar.IsEnabled = false;
            Mostrar();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO Detalle_Venta(Id_Venta, Id_Articulo,Detalle,Precio,Cantidad,descuento) VALUES(@Id_Venta, @Id_Articulo,@Detalle,@Precio,@Cantidad,@descuento)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);

                // Abrir la conexión


                //Id_Venta, Id_Articulo,Detalle,Precio,Cantidad,descuento

                sqlCommand.Parameters.AddWithValue("@Id_Venta", MostrarId());
                sqlCommand.Parameters.AddWithValue("@Id_Articulo", cbArticulo.SelectedValue.ToString());
                sqlCommand.Parameters.AddWithValue("@Detalle", txtDetalle.Text);
                sqlCommand.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                sqlCommand.Parameters.AddWithValue("@Descuento", txtDescuento.Text);

                sqlCommand.Parameters.AddWithValue("@Cantidad", txtCantidadVentas.Text);



                // sqlCommand.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("DD/MM/YY"));
                // Ejecutamos el query de inserción
                // https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand.executescalar

                sqlconnection.Open();

                if (sqlCommand.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("La operación se ha completado correctamente");


                }
                else
                {
                    MessageBox.Show("La operación No se ha completado correctamente");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlconnection.Close();
                // Actualizar el ListBox de Articulos
                ListarDetalle_Compra();
                MostrarTotal();
                ListarArticulos();
                txtCantidadVentas.Text = String.Empty;
                txtPrecio.Text = String.Empty;
                txtDetalle.Text = String.Empty;
                txtDescuento.Text = String.Empty;

            }
        }

        private void MostrarTotal()
        {
            double amt = 0;
            try
            {
                string query = "SELECT SUM(Precio) FROM Detalle_Venta Where Id_Venta=@id";
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                sqlCommand.Parameters.AddWithValue("@Id", MostrarId());
                sqlconnection.Open();
                amt = (double)sqlCommand.ExecuteScalar();   //arror is at this part
                sqlconnection.Close();
                //total = total + int.Parse(txtPrecioCompra.Text);
                //txttotal.Text = total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

                txttotal.Text = amt.ToString();
            }

        }

        private void ListarDetalle_Compra()
        {

            try
            {

                string query = "SELECT * FROM Detalle_Venta WHERE Id_Venta=@Id";


                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlCommand.Parameters.AddWithValue("@Id", MostrarId());
                using (sqlDataAdapter)
                {

                    DataTable tabla1 = new DataTable();


                    sqlDataAdapter.Fill(tabla1);

                    dgDetalle.DisplayMemberPath = "Id_Venta";
                    dgDetalle.DisplayMemberPath = "Id_Articulo";
                    dgDetalle.DisplayMemberPath = "Detalle";
                    dgDetalle.DisplayMemberPath = "Cantidad";
                    dgDetalle.DisplayMemberPath = "Precio";
                    dgDetalle.DisplayMemberPath = "Descuento";


                    dgDetalle.SelectedValuePath = "Id_Venta";
                    dgDetalle.ItemsSource = tabla1.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string MostrarId()
        {
            string x = "";


            try
            {
                string query = "select Id_Venta from Venta where Id_Venta=(select MAX(Id_Venta) from Venta)";

                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                // Reemplazar el parámetro con su valor respectivo

                sqlCommand.ExecuteNonQuery();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    x = reader["Id_Venta"].ToString();


                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                sqlconnection.Close();
            }



            return x;
        }
    }
}

