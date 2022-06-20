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
    public partial class Form1 : Form
    {
        string query_uni = "SELECT * FROM user1_db.product;"; //Автоматический вывод данных при переходе на форму с помощью get_info
        public Form1()
        {
            InitializeComponent();
            get_Info(query_uni + ";"); ;
        }
        void get_Info(string query)
        {

            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlDataAdapter sda = new MySqlDataAdapter(query, conn);
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            DataTable dt = new DataTable();
            cmDB.CommandTimeout = 99999;


            try
            {

                conn.Open();
                sda.Fill(dt);
                for (int z = 0; z < dataGridView1.Columns.Count; z++)
                {
                    dataGridView1.Columns[z].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }
                for (int z = 0; z < dataGridView1.Columns.Count; z++)
                {
                    dataGridView1.Columns[z].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.DataSource = dt;
                dataGridView1.ClearSelection();
                dataGridView1.Columns[0].HeaderText = "Артикул";
                dataGridView1.Columns[0].Selected = true;
                conn.Close();
                dataGridView1.Columns[1].HeaderText = "Наименование";
                dataGridView1.Columns[2].HeaderText = "Единица измерения";
                dataGridView1.Columns[3].HeaderText = "Категория товара";
                dataGridView1.Columns[4].HeaderText = "Изображение";
                dataGridView1.Columns[5].HeaderText = "Производитель";
                dataGridView1.Columns[6].HeaderText = "Стоимость";
                dataGridView1.Columns[7].HeaderText = "Действующая скидка";
                dataGridView1.Columns[8].HeaderText = "Кол-во на складе";
                dataGridView1.Columns[9].HeaderText = "Описание";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденая ошибка!" + Environment.NewLine + ex.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) //Поиск для гостей\менеджеров\клиентов
        {
            string query = "SELECT * FROM user1_db.product where concat(ProductArticleNumber,ProductName,ProductDescription,ProductCategory,ProductManufacturer,ProductCost,ProductDiscountAmount,ProductQuantityInStock,ProductStatus) like '%" + textBox1.Text + "%'";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlDataAdapter sda = new MySqlDataAdapter(query, conn);
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            DataTable dt = new DataTable();
            cmDB.CommandTimeout = 99999;


            try
            {

                conn.Open();
                sda.Fill(dt);
                for (int z = 0; z < dataGridView1.Columns.Count; z++)
                {
                    dataGridView1.Columns[z].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }
                for (int z = 0; z < dataGridView1.Columns.Count; z++)
                {
                    dataGridView1.Columns[z].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.DataSource = dt;
                dataGridView1.ClearSelection();
                dataGridView1.Columns[0].HeaderText = "Артикул";
                dataGridView1.Columns[0].Selected = true;
                conn.Close();
                dataGridView1.Columns[1].HeaderText = "Наименование";
                dataGridView1.Columns[2].HeaderText = "Единица измерения";
                dataGridView1.Columns[3].HeaderText = "Категория товара";
                dataGridView1.Columns[4].HeaderText = "Изображение";
                dataGridView1.Columns[5].HeaderText = "Производитель";
                dataGridView1.Columns[6].HeaderText = "Стоимость";
                dataGridView1.Columns[7].HeaderText = "Действующая скидка";
                dataGridView1.Columns[8].HeaderText = "Кол-во на складе";
                dataGridView1.Columns[9].HeaderText = "Описание";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденая ошибка!" + Environment.NewLine + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //Линк на форму авторизации
        {
            Auto Win = new Auto();
            Win.Show();
            this.Hide();
        }
    }
}
