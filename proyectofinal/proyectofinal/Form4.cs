using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace proyectofinal
{
    public partial class Form4 : Form
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
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            {
                if (Tabla.Rows.Count > 0)
                {
                    int id = int.Parse(textBox1.Text);
                    DataRow[] fila = Tabla.Select(String.Format("ID_juego ={0}", id)); //Buscamos la fila deseada
                    if (fila.Length > 0)//Si se encontro la fila
                    {
                        indice = Tabla.Rows.IndexOf(fila[0]); //Obtenemos el indice la fila buscada
                        //Pasamos el indice como parametro al metodo CargarDatos
                        
                    }
                    else
                    {
                        MessageBox.Show("El juego no esta en el almacen");
                    }
                }
            }

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
