using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class DriverCurrentOrder : Form
    {
        DataBasse dataBasse = new DataBasse();
        public DriverCurrentOrder()
        {
            InitializeComponent();
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("Status", "Статус");
            dataGridView1.Columns.Add("Adress1", "Откуда");
            dataGridView1.Columns.Add("Adress2", "Куда");
            dataGridView1.Columns.Add("Date", "Дата");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        private void ReadSingRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2), record.GetDateTime(3), RowState.ModifeidNew);

        }



        private void RefreshDataGrid(DataGridView dgw)
        {

            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {
                dataBasse.openConnection();
                sqlConn.Open();
                dgw.Rows.Clear();
                SqlCommand command = new SqlCommand(@"OutAllCurrentOrders", sqlConn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", textBox1.Text.Trim());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ReadSingRow(dgw, reader);

                }
                reader.Close();

            }


        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
            
        private async void DriverCurrentOrder_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {

                dataBasse.openConnection();
                sqlConn.Open();
                SqlCommand comm = new SqlCommand(@"DonedOrder", sqlConn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Adress1", textBox2.Text.Trim());
                comm.ExecuteNonQuery();
            }
                

        }


        private void Search(DataGridView dgw)
        {
            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {
                dataBasse.openConnection();
                sqlConn.Open();
                dgw.Rows.Clear();
                SqlCommand command = new SqlCommand(@"SearchOrder", sqlConn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@search", textBox2.Text);
                SqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    ReadSingRow(dgw, read);
                }
                read.Close();

            }
            //string searchString = $"select * from register where concat(id_user, login_user) like '%" + textBox4.Text + "%'";
            //SqlCommand com = new SqlCommand(searchString, dataBasse.GetConnection());
            //dataBasse.openConnection();
            //SqlDataReader read = com.ExecuteReader(); 
            //while (read.Read())
            //{
            //    ReadSingRow(dgw, read);
            //}
            //read.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {
                dataBasse.openConnection();
                sqlConn.Open();
                SqlCommand comm = new SqlCommand(@"CancelledOrder", sqlConn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Adress1", textBox2.Text.Trim());
                comm.ExecuteNonQuery();
            }

        }
    }
}
