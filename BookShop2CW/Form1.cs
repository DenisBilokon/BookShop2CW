using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace BookShop2CW
{
    public partial class Form1 : Form
    {
        private string selectedQueryType;
        private DataTable currentDataTable = new DataTable();
        private SqlConnection sqlConnection = DatabaseHelper.GetConnection();
        private SaveData saver = new SaveData();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookShopDB"].ConnectionString);

            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Hurray");
            }

            listBoxQueries.Items.Add("books");
            listBoxQueries.Items.Add("Authors");
            listBoxQueries.Items.Add("Cities");
            listBoxQueries.Items.Add("Countries");
            listBoxQueries.Items.Add("Customers");
            listBoxQueries.Items.Add("OrderContents");
            listBoxQueries.Items.Add("Orders");
            listBoxQueries.Items.Add("Publishers");

            listBoxQueries.Items.Add("CustomersAndBooks");
            listBoxQueries.Items.Add("CitiesAndCountries");
            listBoxQueries.Items.Add("OrdersWithDetails");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (selectedQueryType)
            {
                case "books":
                    dataGridView1.DataSource = Queries.Books();
                    break;
                case "Authors":
                    dataGridView1.DataSource = Queries.Authors();
                    break;
                case "Cities":
                    dataGridView1.DataSource = Queries.Cities();
                    break;
                case "Countries":
                    dataGridView1.DataSource = Queries.Countries();
                    break;
                case "Customers":
                    dataGridView1.DataSource = Queries.Customers();
                    break;
                case "OrderContents":
                    dataGridView1.DataSource = Queries.OrderContents();
                    break;
                case "Orders":
                    dataGridView1.DataSource = Queries.Orders();
                    break;
                case "Publishers":
                    dataGridView1.DataSource = Queries.Publishers();
                    break;

                    //
                case "CustomersAndBooks":
                    dataGridView1.DataSource = Queries.CustomersAndBooks();
                    break;
                case "CitiesAndCountries":
                    dataGridView1.DataSource = Queries.CitiesAndCountries();
                    break;
                case "OrdersWithDetails":
                    dataGridView1.DataSource = Queries.OrdersWithDetails();
                    break;
            }
        }

        private void listBoxQueries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxQueries.SelectedItem != null)
            {
                selectedQueryType = listBoxQueries.SelectedItem.ToString();
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string selectedTable = listBoxQueries.SelectedItem?.ToString();

            if (selectedTable != null)
            {
                saver.SaveDataToTable(dataGridView1, selectedTable);
                MessageBox.Show("Дані успішно збережені.");
            }
            else
            {
                MessageBox.Show("Не обрано жодної таблиці для збереження.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
