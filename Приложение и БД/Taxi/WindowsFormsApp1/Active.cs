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
using System.Reflection.Emit;
using WindowsFormsApp1.Classes;

namespace WindowsFormsApp1
{
    public partial class Active : Form
    {
        DataBasse dataBasse = new DataBasse();
        int SelectedRow;
        public Active()
        {
            InitializeComponent();
            
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("Status", "Статус");
            dataGridView1.Columns.Add("Adress1", "Откуда");
            dataGridView1.Columns.Add("Adress2", "Куда");
            dataGridView1.Columns.Add("Date", "Куда");
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

                //var id = $"select id_user from register where login_user = '{textBox4.Text}' and password_user = '{textBox5.Text}'";
                var id = @"ReturnID";
                var com = new SqlCommand(id, dataBasse.GetConnection());
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@user_login", textBox1.Text.Trim());

                //SqlDataAdapter adapter = new SqlDataAdapter();
                //DataTable table = new DataTable();
                //adapter.SelectCommand = com;
                //var ids = adapter.Fill(table);
                SqlParameter returnID = new SqlParameter
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.Int

                };
                returnID.Direction = ParameterDirection.Output;
                com.Parameters.Add(returnID);
                com.ExecuteNonQuery();
                string rowId = com.Parameters["@Id"].Value.ToString();
                var ids = int.Parse(rowId);
                dgw.Rows.Clear();
                SqlCommand command = new SqlCommand(@"OutOrders", sqlConn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@login_user", ids);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ReadSingRow(dgw, reader);

                    }
                    reader.Close();

                }


        }

        private async void Active_Load(object sender, EventArgs e)
        {
            CreateColumns();
            textBox1.Text = CurrentUser.user_login;
            RefreshDataGrid(dataGridView1);

        }
        private void label1_Click(object sender, EventArgs e)
        {
            //using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            //{
            //    dataBasse.openConnection();
            //    sqlConn.Open();
            //    SqlCommand command = new SqlCommand(@"ActiveOrder", sqlConn);
            //    command.CommandType = System.Data.CommandType.StoredProcedure;
            //    //command.Parameters.AddWithValue(dateTimePicker1.Value, "@Date");
            //    command.Parameters.AddWithValue( "@Status", label2.Text);
            //    command.Parameters.AddWithValue( "@Adress1", label3.Text);
            //    command.Parameters.AddWithValue( "@Adress2", label4.Text);
            //    SqlDataReader read = command.ExecuteReader();
            //    //while (read.Read())
            //    //{
            //    //    ReadSingRow(dgw, read);
            //    //}
            //    read.Close();

            //}
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
