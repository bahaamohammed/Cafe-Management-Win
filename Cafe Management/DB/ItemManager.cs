using System;
using System.Data;
using System.Data.OleDb;

public class ItemManager
{
    private DatabaseManager dbManager;

    public ItemManager()
    {
        dbManager = new DatabaseManager();
    }

    public bool addItem(string itemName, string itemCat, int itemPrice)
    {
        string insertQuery = "INSERT INTO items (name, cat, price) VALUES (@ItemName, @ItemCat, @ItemPrice)";
        OleDbParameter[] parameters = {
                new OleDbParameter("@ItemName", itemName),
                new OleDbParameter("@ItemCat", itemCat),
                new OleDbParameter("@ItemPrice", itemPrice)
        };

        int result = dbManager.ExecuteNonQuery(insertQuery, parameters);
        return result > 0; // Return true if successful
    }
    public bool updateItem(int itemId, string itemName, string itemCat, string itemPrice)
    {
        string updateQuery = "UPDATE items SET name = @Name, cat = @Cat, price = @Price WHERE id = @ItemId";
        OleDbParameter[] parameters = {
                new OleDbParameter("@Name", itemName),
                new OleDbParameter("@Cat", itemCat),
                new OleDbParameter("@Price", itemPrice),
                new OleDbParameter("@ItemId", itemId),
        };

        int result = dbManager.ExecuteNonQuery(updateQuery, parameters);
        return result > 0; // Return true if successful
    }

    public bool deleteItem(int itemId)
    {
        string deleteQuery = "DELETE FROM items WHERE id = @ItemId";
        OleDbParameter[] parameters = {
            new OleDbParameter("@ItemId", itemId)
        };

        int result = dbManager.ExecuteNonQuery(deleteQuery, parameters);
        return result > 0; // Return true if successful
    }

    public DataTable LoadItems()
    {
        string query = "SELECT id, name, cat, price FROM items"; // Adjust column names as needed
        DataTable dataTable = dbManager.ExecuteQuery(query); // Assuming ExecuteQuery returns a DataTable
        return dataTable; // Return the DataTable with the item data
    }
}
