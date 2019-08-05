using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Configuration;

namespace Proyecto_Inmobiliaria.Formularios
{
    /// <summary>
    /// Lógica de interacción para Proveedores.xaml
    /// </summary>
    public partial class Vendedores : Window
    {
        SqlConnection sqlconnection;
        public Vendedores()
        {
            InitializeComponent();
            string connectionString = @"server=Data Source=LAPTOP-61P3GV5K\SQLEXPRESS;Initial Catalog=WILEN;Integrated Security=True";
            sqlconnection = new SqlConnection(connectionString);

            Mostrar();
        }

        private void Mostrar()
        {
            try
            {

                string query = "SELECT * FROM Proveedor";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {

                    DataTable tabla1 = new DataTable();


                    sqlDataAdapter.Fill(tabla1);

                    dgllenar.DisplayMemberPath = "Id_Proveedor";
                    dgllenar.DisplayMemberPath = "Nombre";
                    dgllenar.DisplayMemberPath = "Sector_Comercial";
                    dgllenar.DisplayMemberPath = "Tipo_Documento";
                    dgllenar.DisplayMemberPath = "Documento";
                    dgllenar.DisplayMemberPath = "Direccion";
                    dgllenar.DisplayMemberPath = "Telefono";
                    dgllenar.DisplayMemberPath = "Correo";
                    dgllenar.DisplayMemberPath = "Fecha";


                    dgllenar.SelectedValuePath = "Id_Proveedor";
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

        }

        private void BtnMantenimiento_Click(object sender, RoutedEventArgs e)
        {
            PanelMantenimientoPart1.Visibility = Visibility.Visible;

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
                string query = "SELECT * FROM Proveedor WHERE Nombre = @nombre ";



                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);


                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {

                    sqlCommand.Parameters.AddWithValue("@nombre", txtbuscar.Text);


                    DataTable tabla = new DataTable();


                    sqlDataAdapter.Fill(tabla);

                    dgllenar.DisplayMemberPath = "Id_Proveedor";
                    dgllenar.DisplayMemberPath = "Nombre";
                    dgllenar.DisplayMemberPath = "Sector_Comercial";
                    dgllenar.DisplayMemberPath = "Tipo_Documento";
                    dgllenar.DisplayMemberPath = "Documento";
                    dgllenar.DisplayMemberPath = "Direccion";
                    dgllenar.DisplayMemberPath = "Telefono";
                    dgllenar.DisplayMemberPath = "Correo";
                    dgllenar.DisplayMemberPath = "Fecha";


                    dgllenar.SelectedValuePath = "Id_Proveedor";
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
            if (txtbuscar.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar un nombre");
                txtbuscar.Focus();
            }
            else
            {
                try
                {
                    string query = "DELETE Proveedor WHERE Nombre =  @nombre";

                    SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);



                    sqlconnection.Open();

                    sqlCommand.Parameters.AddWithValue("@nombre", txtbuscar.Text);
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
            Limpiar();

        }

        private void Limpiar()
        {
            txtSectorComercial.Text = String.Empty;
            txtDireccion.Text = String.Empty;
            cbTipoDocumento.SelectedValue = null;
            txtNombre.Text = String.Empty;
            txtCorreo.Text = String.Empty;
            txtTelefono.Text = String.Empty;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text == string.Empty && txtDireccion.Text == string.Empty && txtDocumento.Text == string.Empty)
            {
                MessageBox.Show("Debe rellenar todos los campos vacios.");
                txtNombre.Focus();

            }
            else
            {
                try
                {
                    string query = "INSERT INTO Proveedor(Nombre,Sector_Comercial,Tipo_Documento,Documento,Direccion,Telefono,Correo) VALUES(@Nombre,@Sector,@TipoDoc,@Doc,@Direccion,@Telefono,@Correo)";

                    SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);

                    sqlconnection.Open();


                    sqlCommand.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    sqlCommand.Parameters.AddWithValue("@Sector", txtSectorComercial.Text);
                    sqlCommand.Parameters.AddWithValue("@TipoDoc", cbTipoDocumento.Text);
                    sqlCommand.Parameters.AddWithValue("@Doc", txtDocumento.Text);
                    sqlCommand.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    sqlCommand.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    sqlCommand.Parameters.AddWithValue("@Correo", txtCorreo.Text);

                    sqlCommand.ExecuteNonQuery();
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


        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Debes ingresar el nombre del proveedor en la caja de texto.");
                txtNombre.Focus();
            }
            else
            {
                try
                {
                    string query = "UPDATE Proveedor SET Documento = @Doc, Direccion = @Direccion, Telefono = @Telefono, Correo = @Correo WHERE Nombre = @Nombre";
                    //Articulo(Codigo,Nombre,Descripcion,Id_Categoria,Fecha) VALUES(@codigo,@nombre,@descripcion,@id_Categoria)
                    SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);

                    sqlconnection.Open();
                    sqlCommand.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    sqlCommand.Parameters.AddWithValue("@Doc", txtDocumento.Text);
                    sqlCommand.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    sqlCommand.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    sqlCommand.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    sqlCommand.ExecuteNonQuery();
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

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }


    }


}
