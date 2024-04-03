using System;
using System.Data;
using System.Data.SqlClient;

public static class Queries
{
    #region BaseTables
    private static string connectionString = "Data Source=DESKTOP-EUFBETK\\SQLEXPRESS;Initial Catalog=BookShop;Integrated Security=True";

    public static DataTable Books()
    {
        DataTable table = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Books";
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
    public static DataTable Authors()
    {
        DataTable table = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Authors";
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
    public static DataTable Cities()
    {
        DataTable table = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Cities";
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

    public static DataTable Countries()
    {
        DataTable table = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Countries";
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
    public static DataTable Customers()
    {
        DataTable table = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Customers";
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

    public static DataTable OrderContents()
    {
        DataTable table = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM OrderContents";
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

    public static DataTable Orders()
    {
        DataTable table = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Orders";
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

    public static DataTable Publishers()
    {
        DataTable table = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Publishers";
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
    #endregion

    #region Querries
    public static DataTable CustomersAndBooks()
    {
        DataTable table = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = @"
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
                    Books.book_id";
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
    public static DataTable CitiesAndCountries()
    {
        DataTable table = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = @"
            SELECT
                Cities.city_id,
                Cities.city_name,
                Countries.country_name
            FROM
                Cities
                JOIN Countries ON Cities.country_id = Countries.country_id";
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
    public static DataTable OrdersWithDetails()
    {
        DataTable table = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = @"
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
                JOIN Books ON OrderContents.book_id = Books.book_id";
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
    #endregion
}