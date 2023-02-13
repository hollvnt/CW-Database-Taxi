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
    public partial class AddDriver : Form
    {
        DataBasse dataBasse = new DataBasse();
        public AddDriver()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {
                int driver_status = 0;
                dataBasse.openConnection();
                sqlConn.Open();
                SqlCommand command = new SqlCommand(@"CreateDriver", sqlConn);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@login_driver", textBox1.Text.Trim());
                command.Parameters.AddWithValue("@password_driver", textBox2.Text.Trim());

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
    }
}
