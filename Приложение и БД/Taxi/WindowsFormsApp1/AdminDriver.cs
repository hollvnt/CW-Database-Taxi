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

namespace WindowsFormsApp1
{
    public partial class AdminDriver : Form
    {
        SqlConnection sqlConnection;
        DataBasse dataBasse = new DataBasse();
        int selectedRow;
        public AdminDriver()
        {
            InitializeComponent();
        }
        private void CreateColumns()
        {
            dataGridView2.Columns.Add("id_driver", "id");
            dataGridView2.Columns.Add("login_driver", "login");
            dataGridView2.Columns.Add("password_driver", "password");
            dataGridView2.Columns.Add("IsNew", String.Empty);


        }

  
        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void ReadSingRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), RowState.ModifeidNew);

        }
        private void RefreshDataGrid(DataGridView dgw)
        {

            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True;"))
            {
                dataBasse.openConnection();
                sqlConn.Open();
                dgw.Rows.Clear();
                SqlCommand command = new SqlCommand(@"RefreshDriver", sqlConn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ReadSingRow(dgw, reader);

                }
                reader.Close();

            }


        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void adminDriver_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView2);

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[selectedRow];

                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
            

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddDriver addDriver = new AddDriver();
            addDriver.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView2);
            ClearFields();
        }
        private void Search(DataGridView dgw)
        {
            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {
                dataBasse.openConnection();
                sqlConn.Open();
                dgw.Rows.Clear();
                SqlCommand command = new SqlCommand(@"SearchDriver", sqlConn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@search", textBox4.Text);
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

        private void deleteRow()
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            dataGridView2.Rows[index].Visible = false;

            if (dataGridView2.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView2.Rows[index].Cells[2].Value = RowState.Deleted;
                return;
            }
        }

        private void Update()
        {
            dataBasse.openConnection();
            for (int index = 0; index < dataGridView2.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView2.Rows[index].Cells[3].Value;
                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
                    {
                        var id = Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value);
                        dataBasse.openConnection();
                        sqlConn.Open();
                        SqlCommand command = new SqlCommand(@"DeleteDriver", sqlConn);

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();

                    }

                }

                if (rowState == RowState.Modifeid)
                {
                    var id = dataGridView2.Rows[index].Cells[0].Value.ToString();
                    var login = dataGridView2.Rows[index].Cells[1].Value.ToString();
                    var pass = dataGridView2.Rows[index].Cells[2].Value.ToString();
                    //var driver_status = dataGridView2.Rows[index].Cells[3].Value.ToString();
                    using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
                    {
                        dataBasse.openConnection();
                        sqlConn.Open();
                        SqlCommand command = new SqlCommand(@"UpdateDriver", sqlConn);

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@login_driver", login);
                        command.Parameters.AddWithValue("@password_driver", pass);
           
                        command.ExecuteNonQuery();

                    }
                    if (rowState == RowState.AddDriver)
                    {

                    }
                    //var changeQuerry = $"update register set login_user = '{login}', password_user = '{pass}' where id_user = '{id}'";
                    //var command = new SqlCommand(changeQuerry, dataBasse.GetConnection());
                    //command.ExecuteNonQuery();
                }
            }
            dataBasse.closeConnecction();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            Search(dataGridView2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deleteRow();
            ClearFields();
        }
        private void Change()
        {
            var selectedRowIndew = dataGridView2.CurrentCell.RowIndex;
            var id = textBox1.Text;
            var login = textBox2.Text;
            var pass = textBox3.Text;
        

            if (dataGridView2.Rows[selectedRowIndew].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView2.Rows[selectedRowIndew].SetValues(id, login, pass);
                dataGridView2.Rows[selectedRowIndew].Cells[3].Value = RowState.Modifeid;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Change();
            ClearFields();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AdminDriver_MouseDown_1(object sender, MouseEventArgs e)
        {
            this.Capture = false;
            Message n = Message.Create(this.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref n);
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
