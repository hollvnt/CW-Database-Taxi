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
using System.Reflection;
using WindowsFormsApp1.Classes;

namespace WindowsFormsApp1
{
    
    public partial class Driver : Form
    {
        DataBasse dataBasse = new DataBasse();
        int selectedRow;
        public Driver()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("Status", "Статус");
            dataGridView1.Columns.Add("Adress1", "Откуда");
            dataGridView1.Columns.Add("Adress2", "Куда");
            dataGridView1.Columns.Add("Date", "Дата");
            //dataGridView1.Columns.Add("id_driver", "Водитель");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        
        private void ReadSingRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2), record.GetDateTime(3), RowState.ModifeidNew);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox1.Text = row.Cells[1].Value.ToString();
                //textBox1.Text = row.Cells[1].Value.ToString();
                //textBox1.Text = row.Cells[2].Value.ToString();
                //textBox1.Text = row.Cells[3].Value.ToString();
                //textBox1.Text = row.Cells[4].Value.ToString();


            }
        }
        private void RefreshDataGrid(DataGridView dgw)
        {

            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {
                dataBasse.openConnection();
                sqlConn.Open();
                dgw.Rows.Clear();
                SqlCommand command = new SqlCommand(@"OutAllUsers", sqlConn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ReadSingRow(dgw, reader);

                }
                reader.Close();

            }


        }

        private async void Driver_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
            textBox3.Text = CurrentUser.user_login;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                
        }

        private void UpdateStatus()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedRowIndew = dataGridView1.CurrentCell.RowIndex;
            string statsus = dataGridView1.CurrentCell.Value.ToString();

            //DataGridViewCell cell = dataGridView1.Rows[rowIndex].Cells[0];
            //dataGridView1.CurrentCell = cell;
            for (int index = 0; index < dataGridView1.Rows.Count; index ++)
            {

                using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
                {
                    string status = dataGridView1.Rows[index].Cells[1].ToString();
                    dataBasse.openConnection();
                    sqlConn.Open();
                    var id = @"ReturnDriverID";
                    var com = new SqlCommand(id, dataBasse.GetConnection());
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@driver_login", textBox3.Text.Trim());

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

                    SqlCommand command = new SqlCommand(@"UpdateOrderInProcess", sqlConn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Adress1", textBox1.Text);
                    //command.Parameters.AddWithValue("@Id", textBox3.Text.Trim());
                    command.ExecuteNonQuery();


                    SqlCommand comm = new SqlCommand(@"AddDriverId", sqlConn);

                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@id_user", textBox2.Text.Trim());
                    comm.Parameters.AddWithValue("@id", ids);
                    comm.ExecuteNonQuery();

                    //SqlCommand comm = new SqlCommand(@"AddDriverId", sqlConn);
                    //comm.CommandType = System.Data.CommandType.StoredProcedure;
                    //comm.Parameters.AddWithValue("@id", ids);
                    //comm.ExecuteNonQuery();

                    //if (command.ExecuteNonQuery() == 1)
                    //{
                    //    MessageBox.Show("Запись добавлена!");

                    //    sqlConn.Close();
                    //}
                }


                    
                
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
                command.Parameters.AddWithValue("@search", textBox1.Text);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            DriverCurrentOrder driverCurrentOrder = new DriverCurrentOrder();
            this.Hide();
            driverCurrentOrder.ShowDialog();
            this.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            DriverHistory driverHistory = new DriverHistory();
            this.Hide();
            driverHistory.ShowDialog();
            this.Show();
        }

        //private void Update()
        //{
        //    dataBasse.openConnection();
        //    for (int index = 0; index < dataGridView1.Rows.Count; index++)
        //    {
        //        var driver = dataGridView1.Rows[index].Cells[4].Value.ToString();
        //        using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
        //        {
        //            dataBasse.openConnection();
        //            sqlConn.Open();
        //            SqlCommand command = new SqlCommand(@"AddDriverId", sqlConn);

        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@id_user", textBox2.Text.Trim());
        //            command.Parameters.AddWithValue("@id", driver);
        //            command.ExecuteNonQuery();

        //        }
        //    }
        //}

        //    private void Change()
        //{
        //    var selectedRowIndew = dataGridView1.CurrentCell.RowIndex;
        //    var pass = textBox3.Text;
 

        //    if (dataGridView1.Rows[selectedRowIndew].Cells[0].Value.ToString() != string.Empty)
        //    {
        //        dataGridView1.Rows[selectedRowIndew].SetValues(pass);
        //        dataGridView1.Rows[selectedRowIndew].Cells[5].Value = RowState.Modifeid;
        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            //Change();
            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {

                dataBasse.openConnection();
                sqlConn.Open();


                SqlCommand command = new SqlCommand(@"AddDriverId", sqlConn);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_user", textBox2.Text.Trim());
                command.Parameters.AddWithValue("@id", textBox3.Text.Trim());
                command.ExecuteNonQuery();




            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Update();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            DriverHistory driverHistory = new DriverHistory();
            this.Hide();
            driverHistory.ShowDialog();
            this.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            DriverCurrentOrder driverCurrentOrder = new DriverCurrentOrder();
            this.Hide();
            driverCurrentOrder.ShowDialog();
            this.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            DriverProfile driverProfile = new DriverProfile();
            this.Hide();
            driverProfile.ShowDialog();
            this.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = ColorTranslator.FromHtml("#242223");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
