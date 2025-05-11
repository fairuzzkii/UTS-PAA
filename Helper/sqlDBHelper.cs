using Npgsql;
using System.Data;

namespace API_Dealer_Mobil_Acc.Helper
{
    public class sqlDBHelper
    {
        private NpgsqlConnection connection;
        private string __constr;

        public sqlDBHelper(string pConstr)
        {
            __constr = pConstr;
            connection = new NpgsqlConnection();
            connection.ConnectionString = __constr;
        }

        public NpgsqlCommand GetNpgsqlCommand(string query)
        {
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public void closeConnection()
        {
            connection.Close();
        }

    }
}
