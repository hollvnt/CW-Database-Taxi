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
using System.Reflection;

namespace WindowsFormsApp1
{
    public partial class AdminOrder : Form
    {
        DataBasse dataBasse = new DataBasse();
        int selectedRow;
        public AdminOrder()
        {
            InitializeComponent();
        }

        private void CreateColumns()
        {
            dataGridView2.Columns.Add("OrderID", "id Заказа");
            dataGridView2.Columns.Add("Adress1", "Откуда");
            dataGridView2.Columns.Add("Adress2", "Куда");
            dataGridView2.Columns.Add("Category", "Категория");
            dataGridView2.Columns.Add("Price", "Цена");
            dataGridView2.Columns.Add("Date", "Дата");
            dataGridView2.Columns.Add("Status", "Статус");
            dataGridView2.Columns.Add("id_user", "Id Юзера");
            dataGridView2.Columns.Add("id_driver", "Id Водителя");
            dataGridView2.Columns.Add("IsNew", String.Empty);


        }
        private void ReadSingRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetInt32(4), record.GetDateTime(5), 
                record.GetString(6), record.GetInt32(7), record.GetInt32(8), RowState.ModifeidNew);

        }
        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox7.Text = "";
            textBox6.Text = "";
            textBox5.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";

        }

        private void RefreshDataGrid(DataGridView dgw)
        {

            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True;"))
            {
                dataBasse.openConnection();
                sqlConn.Open();
                dgw.Rows.Clear();
                SqlCommand command = new SqlCommand(@"RefreshOrders", sqlConn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ReadSingRow(dgw, reader);

                }
                reader.Close();

            }


        }

        private void adminOrder_Load(object sender, EventArgs e)
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
                textBox7.Text = row.Cells[3].Value.ToString();
                textBox6.Text = row.Cells[4].Value.ToString();
                textBox5.Text = row.Cells[5].Value.ToString();
                textBox10.Text = row.Cells[6].Value.ToString();
                textBox9.Text = row.Cells[7].Value.ToString();
                textBox8.Text = row.Cells[8].Value.ToString();


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminCreateOrder adminCreateOrder = new AdminCreateOrder();
            adminCreateOrder.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

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
                SqlCommand command = new SqlCommand(@"SearchOrderForAdmin", sqlConn);
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
                dataGridView2.Rows[index].Cells[8].Value = RowState.Deleted;
                return;
            }
        }

        private void Update()
        {
            dataBasse.openConnection();
            for (int index = 0; index < dataGridView2.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView2.Rows[index].Cells[9].Value;
                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
                    {
                        var id = Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value);
                        dataBasse.openConnection();
                        sqlConn.Open();
                        SqlCommand command = new SqlCommand(@"DeleteOrder", sqlConn);

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();

                    }

                }

                if (rowState == RowState.Modifeid)
                {
                    var id = dataGridView2.Rows[index].Cells[0].Value.ToString();
                    var adress1 = dataGridView2.Rows[index].Cells[1].Value.ToString();
                    var adress2 = dataGridView2.Rows[index].Cells[2].Value.ToString();
                    var category = dataGridView2.Rows[index].Cells[3].Value.ToString();
                    var price = dataGridView2.Rows[index].Cells[4].Value.ToString();
                    var date = dataGridView2.Rows[index].Cells[5].Value.ToString();
                    var status = dataGridView2.Rows[index].Cells[6].Value.ToString();
                    var idUser = dataGridView2.Rows[index].Cells[7].Value.ToString();
                    var idDriver = dataGridView2.Rows[index].Cells[8].Value.ToString();
                    //var driver_status = dataGridView2.Rows[index].Cells[3].Value.ToString();
                    using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
                    {
                        dataBasse.openConnection();
                        sqlConn.Open();
                        SqlCommand command = new SqlCommand(@"UpdateOrders", sqlConn);

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@Adress1", adress1);
                        command.Parameters.AddWithValue("@Adress2", adress2);
                        command.Parameters.AddWithValue("@Category", category);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@Date", date);
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@IdUser ", idUser);
                        command.Parameters.AddWithValue("@IdDriver", idDriver);

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
            var adress1 = textBox2.Text;
            var adress2 = textBox3.Text;
            var category = textBox7.Text;
            var price = textBox6.Text;
            var date = textBox5.Text;
            var status = textBox10.Text;
            var idUser = textBox9.Text;
            var idDriver = textBox8.Text;


            if (dataGridView2.Rows[selectedRowIndew].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView2.Rows[selectedRowIndew].SetValues(id, adress1, adress2, category, price, date, status, idUser, idDriver);
                dataGridView2.Rows[selectedRowIndew].Cells[9].Value = RowState.Modifeid;
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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void AdminOrder_MouseDown(object sender, MouseEventArgs e)
        {
            this.Capture = false;
            Message n = Message.Create(this.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref n);
        }
    }
}
