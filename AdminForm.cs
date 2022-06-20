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
    public partial class AdminForm : Form
    {
        string query_uni = "SELECT * FROM user1_db.product;"; //запрос на автоматический вывод данных черезе get_info
        public AdminForm()
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

        private void textBox1_TextChanged(object sender, EventArgs e) //Поиск данных из всей бд для вывода нужной информации(использовал concat(для связки всех столбцов) и like %..% (для поиска от начала и конца))
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

        private void button4_Click(object sender, EventArgs e) //Кнопка удаления данных по выбору из всей строки
        {
            //string query = "delete from product where = ";
            string idLocRemv = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string query = "delete from user1_db.product where `ProductArticleNumber` = " + idLocRemv;
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            cmDB.CommandTimeout = 600;
            try
            {
                conn.Open();
                MySqlDataReader rd = cmDB.ExecuteReader();
                get_Info(query_uni + ";");
                conn.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e) //обновление данных 
        {
            get_Info(query_uni + ";");
        }

        private void button2_Click(object sender, EventArgs e)//Добавление данных по mode = "add"
        {
            add Win = new add("add",0);
            Win.Show();
        }

        private void button3_Click(object sender, EventArgs e) //Редактирование данных по mode "change"
        {
            string idLocRemv = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            add Win = new add("change", Convert.ToInt32(Convert.ToString(idLocRemv)));
            Win.Show();
        }

        /* private void button5_Click(object sender, EventArgs e) что-то не работает xd
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;

            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
        
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    ExcelApp.Cells[i + 1, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                }
            }

            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
        }*/

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //Линк на возвращение в форму авторизации
        {
            Auto Win = new Auto();
            Win.Show();
            this.Hide();
        }
    }
}
