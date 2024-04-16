using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

public static class DataManager
{
    private static string connectionString = "Data Source=DESKTOP-EUFBETK\\SQLEXPRESS;Initial Catalog=BookShop;Integrated Security=True";

    public static DataTable GetData(string query)
    {
        DataTable table = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }
        }
        return table;
    }

    public static void DisplayObject(string objectName, DataGridView dataGridView)
    {
        switch (objectName)
        {
            case "Books":
                dataGridView.DataSource = DataManager.GetData("SELECT * FROM Books");
                break;
            case "Authors":
                dataGridView.DataSource = DataManager.GetData("SELECT * FROM Authors");
                break;
            case "Cities":
                dataGridView.DataSource = DataManager.GetData("SELECT * FROM Cities");
                break;
            case "Countries":
                dataGridView.DataSource = DataManager.GetData("SELECT * FROM Countries");
                break;
            case "Customers":
                dataGridView.DataSource = DataManager.GetData("SELECT * FROM Customers");
                break;
            case "OrderContents":
                dataGridView.DataSource = DataManager.GetData("SELECT * FROM OrderContents");
                break;
            case "Orders":
                dataGridView.DataSource = DataManager.GetData("SELECT * FROM Orders");
                break;
            case "Publishers":
                dataGridView.DataSource = DataManager.GetData("SELECT * FROM Publishers");
                break;
            case "CustomersAndBooks":
                dataGridView.DataSource = DataManager.GetData(@"
                SELECT
                    Customers.customer_id,
                    Customers.customer_name,
                    Books.book_id,
                    Books.title AS book_title
                FROM
                    Customers
                    JOIN Orders ON Customers.customer_id = Orders.customer_id
                    JOIN OrderContents ON Orders.order_id = OrderContents.order_id
                    JOIN Books ON OrderContents.book_id = Books.book_id
                ORDER BY
                    Customers.customer_id,
                    Books.book_id");
                break;
            case "CitiesAndCountries":
                dataGridView.DataSource = DataManager.GetData(@"
                SELECT
                    Cities.city_id,
                    Cities.city_name,
                    Countries.country_name
                FROM
                    Cities
                    JOIN Countries ON Cities.country_id = Countries.country_id");
                break;
            case "OrdersWithDetails":
                dataGridView.DataSource = DataManager.GetData(@"
                SELECT
                    OrderContents.order_content_id,
                    Orders.order_id,
                    Orders.order_date,
                    Orders.total_amount,
                    Customers.customer_name,
                    Books.title AS book_title
                FROM
                    OrderContents
                    JOIN Orders ON OrderContents.order_id = Orders.order_id
                    JOIN Customers ON Orders.customer_id = Customers.customer_id
                    JOIN Books ON OrderContents.book_id = Books.book_id");
                break;
            default:
                dataGridView.DataSource = null;
                break;
        }
    }

    public static void SaveData(DataGridView dataGridView, string tableName)
    {
        DataTable dataTable = (DataTable)dataGridView.DataSource;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {tableName}", connection))
            {
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(dataTable);
            }
        }
    }
}