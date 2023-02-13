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
using WindowsFormsApp1.Classes;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DataBasse dataBasse = new DataBasse();
        user usr;
        public user form;
        public static string us, pass;
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            SetRoundedShape(panel1, 20);
            SetRoundedShape(panel2, 20);
  

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox2.MaxLength = 50;
            BackColor = ColorTranslator.FromHtml("#FFFFD300");
            button1.BackColor = ColorTranslator.FromHtml("#545d6a");
            textBox1.BackColor = ColorTranslator.FromHtml("#545d6a");
            textBox2.BackColor = ColorTranslator.FromHtml("#545d6a");

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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //textBox1.BackColor = ColorTranslator.FromHtml("#545d6a");
            //textBox1.Height += 50;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           // textBox2.BackColor = ColorTranslator.FromHtml("#545d6a");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form2 frm_sign = new Form2();
            frm_sign.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlDataAdapter adapter2 = new SqlDataAdapter();
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            if (loginUser == "" || passUser == "") MessageBox.Show("Неверный логин или пароль");
            else
            {
                if (checkBox2.Checked)
                {

                    using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
                    {
                        dataBasse.openConnection();
                        sqlConn.Open();
                        SqlCommand command = new SqlCommand(@"HaveAdmins", sqlConn);

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@admin_login", loginUser);
                        command.Parameters.AddWithValue("@admin_password", passUser);
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                        if (table.Rows.Count == 1)
                        {
                            MessageBox.Show("Вы успешно вошли как администратор!");
                            admin admin = new admin();
                            this.Hide();
                            admin.ShowDialog();
                            this.Show();


                        }
                        else MessageBox.Show("Не введен логин или пароль");
                        sqlConn.Close();
                    }
                }
                else if (checkBox2.Checked == false && checkBox1.Checked == false)
                {
                    using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
                    {
                        dataBasse.openConnection();
                        sqlConn.Open();
                        SqlCommand command = new SqlCommand(@"HaveUsers", sqlConn);

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@login_user", loginUser);
                        command.Parameters.AddWithValue("@password_user", passUser);
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                        if (table.Rows.Count == 1)
                        {
                            MessageBox.Show("Вы успешно вошли!");
                            user user = new user();
                            order_taxi order_Taxi = new order_taxi();
                            this.Hide();
                            CurrentUser.user_login = textBox1.Text;
                            //new user(this).Show
                            //

                            user.MyProperty = textBox1.Text;
                            user.MyProperty2 = textBox2.Text;
                            //order_Taxi.MyOrder = textBox1.Text;
                            //order_Taxi.MyOrder2 = textBox2.Text;
                            user userr = new user();
                            
                            user.ShowDialog();
                            userr.textBox1.Text = this.textBox1.Text;
                            this.Show();


                            

                        }
                        else MessageBox.Show("Не введен логин или пароль");
                        sqlConn.Close();
                        

                    }
                }
                else if (checkBox1.Checked)
                {
                    using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
                    {
                        dataBasse.openConnection();
                        sqlConn.Open();
                        SqlCommand command = new SqlCommand(@"HaveDrivers", sqlConn);

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@login_driver", loginUser);
                        command.Parameters.AddWithValue("@password_driver", passUser);
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                        if (table.Rows.Count == 1)
                        {
                            MessageBox.Show("Вы успешно вошли!");
                            DriverProfile driver = new DriverProfile();
                            //order_taxi order_Taxi = new order_taxi();
                            this.Hide();
                            CurrentUser.user_login = textBox1.Text;
                            //new user(this).Show
                            driver.textBox1.Text = this.textBox1.Text;
                            //

                            //user.MyProperty = textBox1.Text;
                            //user.MyProperty2 = textBox2.Text;
                            //order_Taxi.MyOrder = textBox1.Text;
                            //order_Taxi.MyOrder2 = textBox2.Text;
                            driver.ShowDialog();
                            this.Show();


                                

                        }
                        else MessageBox.Show("Не введен логин или пароль");
                        sqlConn.Close();
                    }
                }
                else
                    MessageBox.Show("Такого аккаунта не существует", "Аккаунта нет", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //BackColor = Color.FromArgb(0, 0, 0);
            panel1.BackColor = ColorTranslator.FromHtml("#2e3137");

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {
            panel2.BackColor = ColorTranslator.FromHtml("#2e3137");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Capture = false;
            Message n = Message.Create(this.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref n);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}