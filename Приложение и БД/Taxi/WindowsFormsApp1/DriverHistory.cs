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

namespace WindowsFormsApp1
{
    
    public partial class DriverHistory : Form
    {
        DataBasse dataBasse = new DataBasse();
        public DriverHistory()
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
                SqlCommand command = new SqlCommand(@"OutHistoryDrivers", sqlConn);
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

        private async void DriverHistory_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {
                dataBasse.openConnection();
                sqlConn.Open();

                SqlCommand com = new SqlCommand(@"AddHistory", sqlConn);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.ExecuteNonQuery();
            }

            CreateColumns();
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
