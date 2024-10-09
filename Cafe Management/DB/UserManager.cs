using System;
using System.Data;
using System.Data.OleDb;

public class UserManager
{
    private DatabaseManager dbManager;

    public UserManager()
    {
        dbManager = new DatabaseManager();
    }

    public bool AddUser(string username, string password, string phone)
    {
        string insertQuery = "INSERT INTO users (username, phone, [password]) VALUES (@Username, @Phone, @Password)";
        OleDbParameter[] parameters = {
            new OleDbParameter("@Username", username),
            new OleDbParameter("@Phone", phone),
            new OleDbParameter("@Password", password)
        };

        int result = dbManager.ExecuteNonQuery(insertQuery, parameters);
        return result > 0; // Return true if successful
    }

    public bool UpdateUser(int userId, string username, string password, string phone)
    {
        string updateQuery = "UPDATE users SET username = @Username, phone = @Phone, [password] = @Password WHERE id = @UserId";
        OleDbParameter[] parameters = {
            new OleDbParameter("@Username", username),
            new OleDbParameter("@Phone", phone),
            new OleDbParameter("@Password", password),
            new OleDbParameter("@UserId", userId)
        };

        int result = dbManager.ExecuteNonQuery(updateQuery, parameters);
        return result > 0; // Return true if successful
    }

    public bool DeleteUser(int userId)
    {
        string deleteQuery = "DELETE FROM users WHERE id = @UserId";
        OleDbParameter[] parameters = {
            new OleDbParameter("@UserId", userId)
        };

        int result = dbManager.ExecuteNonQuery(deleteQuery, parameters);
        return result > 0; // Return true if successful
    }

    public DataTable LoadUsers()
    {
        string query = "SELECT id, username, phone, [password] FROM users"; // Adjust column names as needed
        DataTable dataTable = dbManager.ExecuteQuery(query); // Assuming ExecuteQuery returns a DataTable
        return dataTable; // Return the DataTable with the user data
    }
}
