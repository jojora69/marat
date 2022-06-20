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
    public partial class add : Form
    {
        public string modeS = "";
        int item;
        void setMode(string mode)//Использование модов для добавние и редактирования данных. Также автоматическое изменение кнопки button1 на нужный параметр по mode
        {
            if (mode == "add")
            {
                button1.Text = "Добавить";
            }
            else if (mode == "change")
            {
                button1.Text = "Изменить";
                string Info = "select ProductArticleNumber,ProductName,ProductDescription,ProductCategory,ProductManufacturer,ProductCost,ProductDiscountAmount,ProductQuantityInStock,ProductStatus  from product where ProductArticleNumber =" + item.ToString() + ";";
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmInfo = new MySqlCommand(Info, conn);
                MySqlDataReader inRead;
                cmInfo.CommandTimeout = 600;
                try
                {
                    conn.Open();
                    inRead = cmInfo.ExecuteReader();
                    if (inRead.HasRows)
                    {
                        while (inRead.Read())
                        {

                            textBox1.Text = inRead.GetString(0);
                            textBox2.Text = inRead.GetString(1);
                            textBox3.Text = inRead.GetString(2);
                            textBox4.Text = inRead.GetString(3);
                            textBox5.Text = inRead.GetString(4);
                            textBox6.Text = inRead.GetString(5);
                            textBox7.Text = inRead.GetString(6);
                            textBox8.Text = inRead.GetString(7);
                            textBox9.Text = inRead.GetString(8);

                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public add(string mode, int id)
        {
            InitializeComponent();
            modeS = mode;
            item = id;
            setMode(mode);
        }

        private void add_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Полнеые запросы на добавление и редактирование данных по texbox 1-9
        {
            if (modeS == "add")
            {

                string query = "insert into product (ProductArticleNumber, ProductName, ProductDescription, ProductCategory, ProductManufacturer, ProductCost, ProductDiscountAmount, ProductQuantityInStock, ProductStatus) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "', '" + textBox8.Text + "', '" + textBox9.Text + "');";
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmDB = new MySqlCommand(query, conn);
                cmDB.CommandTimeout = 600;
                try
                {
                    conn.Open();
                    MySqlDataReader rd = cmDB.ExecuteReader();
                    conn.Close();
                    AdminForm Win = new AdminForm();
                    Win.Show();
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (modeS == "change")
            {

                string query = "update product set ProductArticleNumber ='" + textBox1.Text + "',ProductName ='" + textBox2.Text + "',ProductDescription = '" + textBox3.Text + "',ProductCategory = '" + textBox4.Text + "',ProductManufacturer = '" + textBox5.Text + "',ProductCost = '" + textBox6.Text + "',ProductDiscountAmount = '" + textBox7.Text + "',ProductQuantityInStock = '" + textBox8.Text + "',ProductStatus = '" + textBox9.Text + "' where ProductArticleNumber =" + item.ToString() + ";";
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmDB = new MySqlCommand(query, conn);
                cmDB.CommandTimeout = 600;
                try
                {
                    conn.Open();
                    MySqlDataReader rd = cmDB.ExecuteReader();
                    conn.Close();
                    AdminForm Win = new AdminForm();
                    Win.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //Возвращение на форму AdminForm
        {
            AdminForm Win = new AdminForm();
            this.Hide();
        }
    }
}
