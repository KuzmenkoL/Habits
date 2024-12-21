using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace priv
{
    public class DataBase : IDisposable
    {
        private SqlConnection sqlConnection = new SqlConnection(@"Server=DESKTOP-K2O72KN;Database=KumenkoLA;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true;encrypt=false");

        // Метод для открытия соединения с базой данных
        public void OpenConnection()
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        // Метод для закрытия соединения с базой данных
        public void CloseConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }

        public DataTable SelectData(string query)
        {
            using (SqlCommand sqlCommand = new SqlCommand(query, GetConnection()))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        // Реализация IDisposable
        public void Dispose()
        {
            CloseConnection();
            sqlConnection.Dispose();
        }

    }
}
