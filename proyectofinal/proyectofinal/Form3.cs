using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace proyectofinal
{
    public partial class Form3 : Form
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
        void CargarDatos(int indice)
        {
            if (Tabla.Rows.Count > 0) //Si el objeto Tabla posee registros procedemos a realizar la asignación
            {
                DataRow fila = Tabla.Rows[indice];
                textBox1.Text = fila["ID_juego"].ToString();
                

            }
            else
            {
                MessageBox.Show("No hay registros para mostrar");
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {

            Conectar();
           
            Sql = "select * from juegos";
            Adaptador = new SqlDataAdapter(Sql, Conexion);
            Adaptador.Fill(Tabla);
     
            CargarDatos(indice);
            Sql = "delete from juegos where ID_juego=@ID_juego";
            Comando = new SqlCommand(Sql, Conexion);
            Comando.Parameters.AddWithValue("@ID_juego", textBox1.Text);
            try
            
            {
                Comando.ExecuteNonQuery();
                MessageBox.Show("Registro eliminado");

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        public Form3()
        {
            InitializeComponent();
        }



        private void Form3_Load(object sender, EventArgs e)
        {

        }



    }

}