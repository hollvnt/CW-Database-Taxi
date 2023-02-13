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
    public partial class DriverProfile : Form
    {
        DataBasse dataBasse = new DataBasse();  
        public DriverProfile()
        {
           
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            SetRoundedShape(panel2, 20);
            SetRoundedShape(panel2, 20);
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

        private void label1_Click(object sender, EventArgs e)
        {
            Driver driver = new Driver();
            this.Hide();
            driver.ShowDialog();
            this.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            DriverCurrentOrder driverCurrentOrder = new DriverCurrentOrder();
            this.Hide();
            driverCurrentOrder.ShowDialog();
            this.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            DriverHistory driverHistory = new DriverHistory();
            this.Hide();
            driverHistory.ShowDialog();
            this.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = ColorTranslator.FromHtml("#242223");
        }

        private void DriverProfile_Load(object sender, EventArgs e)
        {
            textBox1.Text = CurrentUser.user_login;
            button1.BackColor = ColorTranslator.FromHtml("#FFFFD300");


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {
                dataBasse.openConnection();
                sqlConn.Open();
                SqlCommand command = new SqlCommand(@"UpdateDriver", sqlConn);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@login_driver", textBox1.Text.Trim());
                command.Parameters.AddWithValue("@password_driver ", textBox2.Text.Trim());
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Данные аккаунта Обновлены!");
                    //Clear();

                }
                else MessageBox.Show("Не введен логин или пароль");
                sqlConn.Close();

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
 
}
