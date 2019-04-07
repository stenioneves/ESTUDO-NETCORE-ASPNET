using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyFinance.Util
{
    public class DAL
    {
        private static string server = "server01.mysql.local";
        private static string database = "base";
        private static string user = "";
        private static string password = "";
        private static string connectionString = $"Server={server};Database={database};Uid={user};Pwd={password}; SslMode=none";
        private MySqlConnection connection;

        public DAL()
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        // Executa select
        public DataTable RetDataTable(string sql)
        {
            DataTable dataTable = new DataTable();
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dataTable);
            return dataTable;


        }

        // Executa Inserts, Updates e Deletes
        public void ExecutarComandoSQL(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();

        }


    public string Consulta(string sql){
      MySqlCommand command = new MySqlCommand(sql,connection);
      string retorno = Convert.ToString(command.ExecuteScalar());
      return retorno;
    }
    }

}
