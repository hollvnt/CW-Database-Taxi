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

namespace WindowsFormsApp1
{
    public partial class AdminCreateOrder : Form
    {
        DataBasse dataBasse = new DataBasse();
        public AdminCreateOrder()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConn = new SqlConnection(@"Data Source=LAPTOP-ORDDEIA2; Initial Catalog=teeest;Integrated Security=True"))
            {
                dataBasse.openConnection();
                sqlConn.Open();
                //var id = $"select id_user from register where login_user = '{textBox4.Text}' and password_user = '{textBox5.Text}'";
                
                SqlCommand command = new SqlCommand(@"CreateOrder", sqlConn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Adress1", textBox1.Text.Trim());
                command.Parameters.AddWithValue("@Adress2", textBox2.Text.Trim());
                command.Parameters.AddWithValue("@Category", checkedListBox1.Text.Trim());
                command.Parameters.AddWithValue("@Price", textBox3.Text.Trim());
                command.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@Status", textBox6.Text.Trim());
                command.Parameters.AddWithValue("@IdUser ", textBox4.Text.Trim());
                command.Parameters.AddWithValue("@IdDriver", textBox5.Text.Trim());

                if (command.ExecuteNonQuery() == 1)
                {
                    if (textBox1.Text == "" || textBox2.Text == "" || dateTimePicker1.Text == "") MessageBox.Show("Заполните данные значения");
                    else
                    {
                        MessageBox.Show("Заказ успешно Создан");
                      
                        this.Hide();
                     
                    }
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
