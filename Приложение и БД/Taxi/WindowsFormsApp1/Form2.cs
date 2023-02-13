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
    public partial class Form2 : Form
    {
        DataBasse dataBasse = new DataBasse();
        public Form2()
        {
            InitializeComponent();
            SetRoundedShape(panel1, 20);
            SetRoundedShape(panel1, 20);
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox2.MaxLength = 50;
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

        private void button1_Click(object sender, EventArgs e)
        {
            

            //if (command.ExecuteNonQuery() == 1)
            //{
            //    MessageBox.Show("Аккаунт успешно создан!");
            //    Form1 frm_login = new Form1();
            //    this.Hide();
            //    frm_login.ShowDialog();
            //    if (textBox1.Text == "" || password == "") MessageBox.Show("Отсутствует логин или пароль");
            //}
            ////else if (textBox1.Text == "" || password == "") MessageBox.Show("Аккаунт не был создан!");
            //dataBasse.closeConnecction();

            
        }

        //private Boolean checkuser()
        //{
        //    var loginUser = textBox1.Text;
        //    var passUser = textBox2.Text;

        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    DataTable table = new DataTable();
        //    string querystring = $"select id_user, login_user from register where login_user= '{loginUser}' ";
        //    using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
        //    {
        //        dataBasse.openConnection();
        //        sqlConn.Open();
        //        SqlCommand command = new SqlCommand(@"CheckUser", sqlConn);
        //        command.CommandType = System.Data.CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("@login_user", textBox1.Text.Trim());
        //        command.Parameters.AddWithValue("@password_user", textBox2.Text.Trim());
        //        adapter.SelectCommand = command;
        //        adapter.Fill(table);
        //        if (command.ExecuteNonQuery() == 1)
        //      if (table.Rows.Count > 0) { 
        //          MessageBox.Show("Такой пользваотель сущестсвует");
        //          return true;
        //      }
        //      else return false;
        //    }
        //    //SqlCommand command = new SqlCommand(querystring, dataBasse.GetConnection());
        //    //adapter.SelectCommand = command;
        //    //adapter.Fill(table);
        //    //if (table.Rows.Count > 0) { 
        //    //    MessageBox.Show("Такой пользваотель сущестсвует");
        //    //    return true;
        //    //}
        //    //else return false;
        //}

        private void Form2_Load(object sender, EventArgs e)
        {
            BackColor = ColorTranslator.FromHtml("#FFFFD300");
            panel1.BackColor = ColorTranslator.FromHtml("#2e3137");
            button2.BackColor = ColorTranslator.FromHtml("#545d6a");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var password = textBox2.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            //string querystring = $"insert into register(login_user, password_user) values('{login}', '{password}')";
            //using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            //{
            //    dataBasse.openConnection();
            //    sqlConn.Open();
            //    SqlCommand command = new SqlCommand(@"CheckUser", sqlConn);
            //    command.CommandType = System.Data.CommandType.StoredProcedure;
            //    command.Parameters.AddWithValue("@login_user", textBox1.Text.Trim());
            //    command.Parameters.AddWithValue("@password_user", textBox2.Text.Trim());
            //    adapter.SelectCommand = command;
            //    adapter.Fill(table);
            //    if (command.ExecuteNonQuery() == 1)
            //        if (table.Rows.Count > 0)
            //        {
            //            MessageBox.Show("Такой пользваотель сущестсвует");

            //        }
            //    sqlConn.Close();
            //}
            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {
                dataBasse.openConnection();
                sqlConn.Open();
                SqlCommand command = new SqlCommand(@"AddUser", sqlConn);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@login_user", textBox1.Text.Trim());
                command.Parameters.AddWithValue("@password_user", textBox2.Text.Trim());
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт успешно создан!");
                    Form1 frm_login = new Form1();
                    this.Hide();
                    frm_login.ShowDialog();
                    Clear();
                    if (textBox1.Text == "" || password == "") MessageBox.Show("Отсутствует логин или пароль");
                    else MessageBox.Show("Аккаунт не был создан!");
                    sqlConn.Close();
                }

            }


            void Clear()
            {
                textBox1.Text = textBox2.Text = "";
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
