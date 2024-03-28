using System;
using System.Data.SqlClient;

public static class DatabaseHelper
{
    public static SqlConnection GetConnection()
    {
        string connectionString = "Data Source=DESKTOP-EUFBETK\\SQLEXPRESS;Initial Catalog=BookShop;Integrated Security=True";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        return sqlConnection;
    }
}