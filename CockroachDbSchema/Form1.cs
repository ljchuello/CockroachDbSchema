using System;
using System.Linq;
using System.Windows.Forms;
using CockroachDbSchema.Libreria;

namespace CockroachDbSchema
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void txtGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                // Host
                if (Cadena.Vacia(txtHost.Text))
                {
                    MessageBox.Show("Debe ingresar el servidor");
                    return;
                }

                // Port
                if (Cadena.Vacia(txtPort.Text))
                {
                    MessageBox.Show("Debe ingresar un puerto");
                    return;
                }

                // Port
                if (!Cadena.EsInt(txtPort.Text))
                {
                    MessageBox.Show("El puerto debe ser un número");
                    return;
                }

                // Usuario
                if (Cadena.Vacia(txtUser.Text))
                {
                    MessageBox.Show("Debe ingresar el usuario");
                    return;
                }

                // Contraseña
                if (Cadena.Vacia(txtPassword.Text))
                {
                    MessageBox.Show("Debe ingresar la contraseña");
                    return;
                }

                // Database
                if (Cadena.Vacia(txtDatabase.Text))
                {
                    MessageBox.Show("Debe ingresar la base de datos");
                    return;
                }

                // Set
                Conexion conexion = new Conexion();
                conexion.Id = Cadena.GenerarId();
                conexion.Host = Cadena.Depurar(txtHost.Text);
                conexion.Port = Convert.ToInt32(txtPort.Text);
                conexion.SslMode = chSsl.Checked;
                conexion.Username = Cadena.Depurar(txtUser.Text);
                conexion.Password = Cadena.Depurar(txtPassword.Text);
                conexion.Database = Cadena.Depurar(txtDatabase.Text);

                // Probamos la conexion
                await conexion.Test(conexion);

                // Validamos si ya existe
                var lista = await Fichero.Leer();
                if (lista.Exists(x => x.Host.ToLower() == conexion.Host.ToLower() && x.Database.ToLower() == conexion.Database.ToLower()))
                {
                    MessageBox.Show("La conexión que desea agregar ya está registrada");
                    return;
                }

                // Metemos
                await Fichero.Escribir(conexion);

                // Libre de pecados
                MessageBox.Show($"Se ha registrado éxitosamente la conexión");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ah ocurrido un error; {ex.Message}");
            }
        }
    }
}
