using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShop2CW
{
    public class SaveData
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BookShopDB"].ConnectionString;

        public void SaveBooks(DataGridView dataGridView)
        {
            SaveDataToTable(dataGridView, "Books");
        }

        public void SaveDataToTable(DataGridView dataGridView, string tableName)
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

        public void DeleteBooks(DataGridView dataGridView)
        {
            DeleteDataFromTable(dataGridView, "Books");
        }

        private void DeleteDataFromTable(DataGridView dataGridView, string tableName)
        {
            DataTable dataTable = (DataTable)dataGridView.DataSource;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {tableName}", connection))
                {
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.DeleteCommand = builder.GetDeleteCommand();
                    adapter.Update(dataTable);
                }
            }
        }

    }
}
