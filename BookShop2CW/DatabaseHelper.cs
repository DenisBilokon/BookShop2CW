using System;
using System.Data;
using System.Data.SqlClient;

public static class DatabaseHelper
{
    public static SqlConnection GetConnection()
    {
        string connectionString = "Data Source=DESKTOP-EUFBETK\\SQLEXPRESS;Initial Catalog=BookShop;Integrated Security=True";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        return sqlConnection;
    }
    
    public static DataTable GetData(string query)
    {
        string connectionString = "Data Source=DESKTOP-EUFBETK\\SQLEXPRESS;Initial Catalog=BookShop;Integrated Security=True";
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
}
