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
    public partial class user : Form
    {
        public TextBox text;
        DataBasse dataBasse = new DataBasse();
        Form1 form;
        

        
        public user()
        {
       
            InitializeComponent();
            textBox1.Text = CurrentUser.user_login;
            StartPosition = FormStartPosition.CenterScreen;
            label1.Click += new EventHandler(button1_Click);
            this.Load += new EventHandler(user_Load);
            SetRoundedShape(panel2, 20);
            SetRoundedShape(panel2, 20);
        }
        
        private void user_Load(object sender, EventArgs e)
        {
            textBox1.Text = CurrentUser.user_login;
            panel1.BackColor = ColorTranslator.FromHtml("#2e3137");
            button1.BackColor = ColorTranslator.FromHtml("#FFFFD300");
            
            //textBox1.Text = MyProperty.ToString();
            //textBox2.Text = MyProperty2.ToString();
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

        public string MyProperty
        {
            get;
            set;

        }
        public string MyProperty2
        {
            get;
            set;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            order_taxi order = new order_taxi();
            this.Hide();
            //order.MyOrder = textBox1.Text;
            //order.MyOrder2 = textBox2.Text;
            order.ShowDialog();
            this.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Change()
        {
            var userLogin = textBox1.Text;
            var userPass = textBox2.Text;
            
            if(userLogin == "" || userPass == "") MessageBox.Show("Не заполненны все поля");

        }
        private void button1_Click(object sender, EventArgs e)
        {
            

            //void Clear()
            //{
            //    textBox1.Text = textBox2.Text = "";
            //}

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //((Form1)this.Tag).textBox1.Text = textBox1.Text;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = ColorTranslator.FromHtml("#242223");
        }

        private void label8_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Active active = new Active();
            this.Hide();
            active.ShowDialog();
            this.Show();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            form.textBox1.Text = "eee";
        }

        private void label4_Click(object sender, EventArgs e)
        {
            UserHistory userHistory = new UserHistory();
            this.Hide();
            userHistory.ShowDialog();
            this.Show();
        }

        private void label8_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {
                dataBasse.openConnection();
                sqlConn.Open();
                SqlCommand command = new SqlCommand(@"UserUpdate", sqlConn);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@login_user", textBox1.Text.Trim());
                command.Parameters.AddWithValue("@password_user", textBox2.Text.Trim());
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Данные аккаунта Обновлены!");
                    //Clear();

                }
                else MessageBox.Show("Не введен логин или пароль");
                sqlConn.Close();

            }
        }
    }
}
