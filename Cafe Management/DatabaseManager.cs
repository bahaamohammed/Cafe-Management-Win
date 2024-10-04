using System;
using System.Data;
using System.Data.OleDb;

public class DatabaseManager
{
    private OleDbConnection connection;
    private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=dbCafeMng.accdb;Persist Security Info=False;";

    public DatabaseManager()
    {
        connection = new OleDbConnection(connectionString);
    }

    // Open the database connection
    public void OpenConnection()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
    }

    // Close the database connection
    public void CloseConnection()
    {
        if (connection.State == ConnectionState.Open)
        {
            connection.Close();
        }
    }

    // Execute Insert, Update, Delete commands
    public string ExecuteNonQuery(string query, OleDbParameter[] parameters = null)
    {
        try
        {
            OpenConnection();
            using (OleDbCommand cmd = new OleDbCommand(query, connection))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                return cmd.ExecuteNonQuery().ToString(); // Returns number of rows affected
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine("Error: " + ex.Message);
            //return -1;
            return ex.Message;
        }
        finally
        {
            CloseConnection();
        }
    }

    // Execute Select commands and return DataTable
    public DataTable ExecuteQuery(string query, OleDbParameter[] parameters = null)
    {
        DataTable dt = new DataTable();
        try
        {
            OpenConnection();
            using (OleDbCommand cmd = new OleDbCommand(query, connection))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                {
                    adapter.Fill(dt); // Fill the DataTable with result set
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }
        return dt;
    }
}
