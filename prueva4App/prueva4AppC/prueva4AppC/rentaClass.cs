using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace prueva4AppC
{
    class rentaClass
    {
        SqlConnection MiConexion;

        public void conectar()
        {
            MiConexion = new SqlConnection("Data Source=DESKTOP-7M522E8;Initial Catalog=VideoClub;User ID=sa;Password=123456");
            MiConexion.Open();
        }

        public void desconectar()
        {
            MiConexion.Close();
        }

        public void ejecutarSql(String query)
        {
            SqlCommand miComando = new SqlCommand(query, MiConexion);
            int filasAfectadas = miComando.ExecuteNonQuery();

            if (filasAfectadas > 0)
            {
                MessageBox.Show("Operacion realizada exitosamente", "La base de datos ah sido modificada",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("No se pudo realizar la modificacion", "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        public void actualizarGrid( DataGridView dg, String query)
        {
            this.conectar();

            System.Data.DataSet miDataSet = new System.Data.DataSet();

            SqlDataAdapter miDataAdapter = new SqlDataAdapter(query, MiConexion);

            

            miDataAdapter.Fill(miDataSet, "renta");

            dg.DataSource = miDataSet;
            dg.DataMember = "renta";

            this.desconectar();


        }
    }
}
