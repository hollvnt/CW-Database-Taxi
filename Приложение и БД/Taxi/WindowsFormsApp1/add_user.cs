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
    public partial class Form3 : Form
    {
        DataBasse dataBasse = new DataBasse();
        public Form3()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            SetRoundedShape(panel1, 20);
            SetRoundedShape(panel1, 20);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {
                int driver_status = 0;
                dataBasse.openConnection();
                sqlConn.Open();
                SqlCommand command = new SqlCommand(@"AddUser", sqlConn);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@login_user", textBox1.Text.Trim());
                command.Parameters.AddWithValue("@password_user", textBox2.Text.Trim());

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Запись добавлена!");
                    this.Hide();
                    Clear();
                    if (textBox1.Text == "" || textBox2.Text == "") MessageBox.Show("Отсутствует логин или пароль");
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

        private void Form3_Load(object sender, EventArgs e)
        {

            BackColor = ColorTranslator.FromHtml("#FFFFD300");
        }
    }
}
