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

        private SqlConnection sqlConnection = DatabaseHelper.GetConnection();

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
            listBoxQueries.Items.Add("CustomersAndBooks");
            listBoxQueries.Items.Add("CitiesAndCountries");
            listBoxQueries.Items.Add("OrdersWithDetails");

            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter; //редагування та збереження змін
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
                button1.Enabled = true; // Робимо кнопку активною, коли обрано елемент
            }
            else
            {
                button1.Enabled = false; // Робимо кнопку неактивною, якщо нічого не обрано
            }
        }

        private void AddButton_Click_1(object sender, EventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

        }
    }
}
