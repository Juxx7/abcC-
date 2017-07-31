using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prueva4AppC
{
    public partial class Form1 : Form
    {
        rentaClass rent = new rentaClass();
        String codigo;
        Boolean editar;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            editar = false;
            rent.actualizarGrid(this.dataGridView1,"SELECT * FROM renta");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (editar)
            {
                rent.conectar();
                String Query1 = "update renta set ID_socios ='" + textidSocios.Text + "', ID_pelicula ='" + textidPelicula.Text + "', Total = " + textTotal.Text + ", Fecha = " + textFecha.Text + " where ID_renta = '" + codigo + "';";

                rent.ejecutarSql(Query1);
                rent.actualizarGrid(this.dataGridView1,"SELECT * FROM renta;");
                rent.desconectar();
                this.limpiarCajaText();
                editar = false;
            }else
            {
                rent.conectar();

                String query = "INSERT INTO renta (ID_renta,ID_socios,ID_pelicula,Total,Fecha) values ('" + textidRenta.Text + "','" + textidSocios.Text + "','" + textidPelicula.Text + "'," + textTotal.Text + "," + textFecha.Text + ");";

                rent.ejecutarSql(query);

                rent.actualizarGrid(this.dataGridView1, "SELECT * FROM renta;");

                rent.desconectar();

                this.limpiarCajaText();
            }
         }

        public void limpiarCajaText()
        {
            textidRenta.Text = " ";
            textidSocios.Text = " ";
            textidPelicula.Text = " ";
            textTotal.Text = " ";
            textFecha.Text = " ";
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            editar = true;

            codigo = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textidSocios.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textidPelicula.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textTotal.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textFecha.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void textBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            rent.actualizarGrid(this.dataGridView1, "select * from renta where fecha like '" + textBuscar.Text + "%';");

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            codigo = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();

            var resultado = MessageBox.Show("Desea eliminar el registro: " + codigo, "Confirme la acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                rent.conectar();

                String query = "DELETE FROM renta WHERE ID_renta = '" + codigo + "';";

                rent.ejecutarSql(query);

                rent.actualizarGrid(this.dataGridView1, "SELECT * FROM renta;");

                rent.desconectar();
            }else
            {
                //no hace nada
                return;
            }
        }
    }
}
