using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Makar
{
    public partial class Auto : Form
    {
        public Auto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //Кнопка входа с входом по ролям на разные формы
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                string mod = "";
                string id = "";
                string query = "select UserID, UserRole from user where UserLogin ='" + textBox1.Text + "' and UserPassword = '" + textBox2.Text + "';";//Запрос на выход по данным из textbox1/2
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmDB = new MySqlCommand(query, conn);
                cmDB.CommandTimeout = 60;
                try
                {
                    conn.Open();
                    MySqlDataReader rd = cmDB.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            id = rd.GetString(0);
                            mod = rd.GetString(1);
                        }
                    }
                    conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка авторизации. Попробуйте еще раз.");
                    MessageBox.Show(ex.Message);
                }
                if (Convert.ToInt32(id) > 0)
                {
                    if (mod == "1")
                    {
                        AdminForm Win = new AdminForm();
                        Win.Show();
                        this.Hide();

                    }
                    else if (mod =="3")
                    {
                        Form1 Win = new Form1();
                        Win.Show();
                        this.Hide();
                        
                    }
                    else if (mod =="2")
                    {
                        Form1 Win = new Form1();
                        Win.Show();
                        this.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //Выход как гость
        {
            Form1 Win = new Form1();
            Win.Show();
            this.Hide();
        }
    }
}

