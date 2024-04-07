using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms.DataVisualization.Charting;

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

            listBox2.Items.Clear();
            listBox2.Items.Add("OrdersSummary");
            listBox2.Items.Add("AuthorsAndBookAmount");
            //listBox2.SelectedIndex = 0;


            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Connection Established");
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

                if (selectedQueryType == "CitiesAndCountries" || selectedQueryType == "OrdersWithDetails" || selectedQueryType == "CustomersAndBooks")
                {
                    label1.Text = "this is a query, you cannot change it";
                }
                else
                {
                    label1.Text = "this is a DB table, you can change it";
                }
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
        
        private string currentQuery = "";
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a query type.");
                return;
            }

            string selectedQuery = listBox2.SelectedItem.ToString();

            switch (selectedQuery)
            {
                case "OrdersSummary":
                    string query1 = "SELECT order_date, SUM(total_amount) AS total_amount FROM Orders GROUP BY order_date";
                    DataTable table1 = DatabaseHelper.GetData(query1);
                    chart1.Series.Clear();
                    chart1.ChartAreas.Clear();
                    chart1.ChartAreas.Add(new ChartArea());
                    chart1.Series.Add(new Series());
                    chart1.Series[0].ChartType = SeriesChartType.Line;
                    foreach (DataRow row in table1.Rows)
                    {
                        chart1.Series[0].Points.AddXY(row["order_date"], row["total_amount"]);
                    }
                    chart1.Series[0].Name = "";
                    chart1.Series[0].IsVisibleInLegend = false;
                    chart1.Show();
                    break;
                case "AuthorsAndBookAmount":
                    string query2 = @"
                SELECT Authors.author_name, COUNT(Books.book_id) AS book_count
                FROM Authors
                JOIN Books ON Authors.author_id = Books.author_id
                GROUP BY Authors.author_name";
                    DataTable table2 = DatabaseHelper.GetData(query2);
                    chart1.Series.Clear();
                    chart1.ChartAreas.Clear();
                    chart1.ChartAreas.Add(new ChartArea());
                    chart1.Series.Add(new Series());
                    chart1.Series[0].ChartType = SeriesChartType.Column;
                    foreach (DataRow row in table2.Rows)
                    {
                        chart1.Series[0].Points.AddXY(row["author_name"], row["book_count"]);
                    }
                    chart1.Series[0].Name = "";
                    chart1.Series[0].IsVisibleInLegend = false;
                    chart1.Show();
                    break;
                default:
                    MessageBox.Show("Unknown query type.");
                    break;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedChart = listBox2.SelectedItem.ToString();
            string description = "";

            switch (selectedChart)
            {
                case "OrdersSummary":
                    description = "Graph of orders by date and total price by that date";
                    break;
                case "AuthorsAndBookAmount":
                    description = "Graph of authors and the number of their books";
                    break;
                default:
                    description = "Select a chart type";
                    break;
            }

            label2.Text = description;
        }
    }
}
