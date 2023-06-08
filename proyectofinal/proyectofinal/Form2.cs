using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace proyectofinal
{
    public partial class Form2 : Form
    {
        SqlConnection Conexion = new SqlConnection();
        SqlCommand Comando;
        SqlDataAdapter Adaptador = null;
        DataTable Tabla = new DataTable();
        string Sql = " ";
        string Servidor = @"ZAKY-BOOK\SQLEXPRESS";
        string Base_Datos = "bd2022";
        int indice = 0;

        void Conectar()
        {
            try
            {
                Conexion.ConnectionString = "Data Source=" + Servidor + ";" +
                "Initial Catalog=" + Base_Datos + ";" +
                "Integrated security=true";
                try
                {
                    Conexion.Open();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("ClientesBDError al tratar de establecer la conexión " + ex.Message);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error en la conexión: " + ex.Message);
            }
        }

        void RefrescarDatos()
        {
            Sql = "select * from juegos";
            Adaptador = new SqlDataAdapter(Sql, Conexion);
            Tabla.Clear();
            Adaptador.Fill(Tabla);
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        void revision(int indice)
        {
            if (Tabla.Rows.Count >= 0)
            {
                DataRow fila = Tabla.Rows[indice];
                textBox1.Text = fila["ID_juego"].ToString();
                textBox2.Text = fila["nombre"].ToString();
                textBox3.Text = fila["precio"].ToString();
                textBox4.Text = fila["compañia"].ToString();
                textBox5.Text = fila["plataforma"].ToString();
            }
            else
            {
                MessageBox.Show("No hay registros para mostrar");
            }
        }

        void Refrescar()
        {
            Sql = "select * from juegos";
            Adaptador = new SqlDataAdapter(Sql, Conexion);
            Tabla.Clear();
            Adaptador.Fill(Tabla);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conectar(); // Abrir la conexión antes de ejecutar la consulta

            Sql = "insert into juegos(ID_juego, nombre, precio, compañia, plataforma" +")values(@ID_juego,@nombre,@precio,@compañia,@plataforma )";

            Comando = new SqlCommand(Sql, Conexion);
            Comando.Parameters.AddWithValue("@ID_juego", textBox1.Text);
            Comando.Parameters.AddWithValue("@nombre", textBox2.Text);
            Comando.Parameters.AddWithValue("@precio", textBox3.Text);
            Comando.Parameters.AddWithValue("@compañia", textBox4.Text);
            Comando.Parameters.AddWithValue("@plataforma", textBox5.Text);

            try // Bloque try catch para captura de excepciones en ejecución
            {
                Comando.ExecuteNonQuery(); // Ejecutamos la instrucción SQL
                MessageBox.Show("Registro insertado");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Conexion.Close(); // Cerrar la conexión después de ejecutar la consulta
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
