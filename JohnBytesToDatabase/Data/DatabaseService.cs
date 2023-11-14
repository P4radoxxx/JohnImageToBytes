using System.Data.SqlClient;

public class DatabaseService
{
    private string CString = "Data Source=DESKTOP-FH1BO1J\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

    public void UploadImage(byte[] imageData)
    {
        using (SqlConnection sqlConnection = new SqlConnection(CString))
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandText = " IF NOT EXISTS (SELECT TOP 1 * FROM Image) " +
                                         " INSERT INTO Image (ImageData) VALUES (@ImageData) " +
                                         " ELSE " +
                                         " UPDATE Image SET ImageData = @ImageData";
                sqlCommand.Parameters.Add("@ImageData", System.Data.SqlDbType.VarBinary, -1).Value = imageData;
                sqlCommand.ExecuteNonQuery();
            }
        }
    }


    public byte[] GetImage()
    {
        using (SqlConnection connection = new SqlConnection(CString))
        {
            connection.Open();

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT ImageData FROM Image";


                // Retourne un tableau de byte avec les data de la colonne
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (byte[])reader["ImageData"];
                    }
                }
            }
        }

        return null;
    }
}
