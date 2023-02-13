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
using WindowsFormsApp1.Classes;

namespace WindowsFormsApp1
{
    public partial class order_taxi : Form
    {
        DataBasse dataBasse = new DataBasse();
        Random random = new Random();
        public order_taxi()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            SetRoundedShape(panel1, 20);
            this.Load += new EventHandler(order_taxi_Load);
        }

        private void order_taxi_Load(object sender, EventArgs e)
        {
            BackColor = ColorTranslator.FromHtml("#FFFFD300");
            textBox4.Text = CurrentUser.user_login;
            panel1.BackColor = ColorTranslator.FromHtml("#2e3137");
            //textBox4.Text = MyOrder.ToString();
            //textBox5.Text = MyOrder2.ToString();
        }
        public string MyOrder
        {
            get;
            set;

        }
        public string MyOrder2
        {
            get;
            set;
        }

        static void SetRoundedShape(Control control, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLine(radius, 0, control.Width - radius, 0);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddLine(control.Width, radius, control.Width, control.Height - radius);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddLine(control.Width - radius, control.Height, radius, control.Height);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.AddLine(0, control.Height - radius, 0, radius);
            path.AddArc(0, 0, radius, radius, 180, 90);
            control.Region = new Region(path);
        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 1) textBox3.Text = random.Next(2, 7).ToString();
            if(checkedListBox1.CheckedItems.Count == 2) textBox3.Text = random.Next(5, 10).ToString();
            if(checkedListBox1.CheckedItems.Count == 3) textBox3.Text = random.Next(2, 15).ToString();
            checkedListBox1.BackColor = ColorTranslator.FromHtml("#545d6a");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.BackColor = ColorTranslator.FromHtml("#2e3137");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var status = "Active";
            //var id = $"select id_user from register where login_user = '{textBox4.Text}' and password_user = '{textBox5.Text}'";
            //var com = new SqlCommand(id, dataBasse.GetConnection());
            //var ids = com.ExecuteNonQuery();
            //var changeQuerry = $"update register set login_user = '{login}', password_user = '{pass}' where id_user = '{id}'";
            //var command = new SqlCommand(changeQuerry, dataBasse.GetConnection());

            button2.BackColor = ColorTranslator.FromHtml("#FFFFD300");
            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {
                dataBasse.openConnection();
                sqlConn.Open();
                //var id = $"select id_user from register where login_user = '{textBox4.Text}' and password_user = '{textBox5.Text}'";
                var id = @"ReturnID";
                var com = new SqlCommand(id, dataBasse.GetConnection());
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@user_login", textBox4.Text.Trim());
              
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
                SqlCommand command = new SqlCommand(@"AddOrder", sqlConn);
                
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Adress1", textBox1.Text.Trim());
                command.Parameters.AddWithValue("@Adress2", textBox2.Text.Trim());
                command.Parameters.AddWithValue("@Category", checkedListBox1.Text.Trim());
                command.Parameters.AddWithValue("@Price", textBox3.Text.Trim());
                command.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@Id_driver", 1);
                command.Parameters.AddWithValue("@Id", ids);
                if (command.ExecuteNonQuery() == 1)
                {
                    if (textBox1.Text == "" || textBox2.Text == "" || dateTimePicker1.Text == "") MessageBox.Show("Заполните данные значения");
                    else
                    {
                        MessageBox.Show("Заказ успешно оформлен");
                        user user = new user();
                        this.Hide();
                        user.ShowDialog();
                        Clear();
                        //if (textBox1.Text == "" || password == "") MessageBox.Show("Отсутствует логин или пароль");
                        //else MessageBox.Show("Аккаунт не был создан!");
                        sqlConn.Close();
                    }
                }

            }

            void Clear()
            {
                textBox1.Text = textBox2.Text = textBox3.Text = "";
                checkedListBox1.Items.Remove(checkedListBox1.SelectedItems);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
          

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
    
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
   
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //string searchString = $"select * from register where concat(id_user, login_user) like '%" + textBox4.Text + "%'";
            //SqlCommand com = new SqlCommand(searchString, dataBasse.GetConnection());
            //dataBasse.openConnection();
            //SqlDataReader read = com.ExecuteReader(); 
            //while (read.Read())
            //{
            //    ReadSingRow(dgw, read);
            //}
            //read.Close();
            //string id = $"select id_user from register where login_user = '{textBox4.Text}' and password_user = '{textBox5.Text}'";
            //SqlCommand command = new SqlCommand(id, dataBasse.GetConnection());
            //dataBasse.openConnection();

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
