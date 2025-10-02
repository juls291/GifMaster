using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace GifMaster
{
    public class KeyHandler
    {
        private const string EncryptionKey = "fdh456!fe!!@dnfREFGHn34tn3sgSRgerntg3"; // Use a strong key

        public static string FetchApiKey()
        {
            string encryptedApiKey = GetEncryptedApiKeyFromServer();
            if (string.IsNullOrEmpty(encryptedApiKey))
            {
                throw new Exception("Failed to fetch API key from the server.");
            }

            return DecryptApiKey(encryptedApiKey);
        }

        private static string GetEncryptedApiKeyFromServer()
        {
            string connectionString = "Server=INFPRO-WS109\\MSSQLSERVER01;Database=GifMaster;User ID=sa;Password=dionysys08admin;MultipleActiveResultSets=True;Connect Timeout=5;Pooling=true;";
            string query = "SELECT EncryptedApiKey FROM ApiKeysTable WHERE AppName = 'GifMaster'"; // Update table/column names accordingly

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }

        private static string DecryptApiKey(string encryptedApiKey)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] key = Encoding.UTF8.GetBytes(EncryptionKey);
                byte[] iv = new byte[16];

                using (ICryptoTransform decryptor = aes.CreateDecryptor(key, iv))
                {
                    byte[] encryptedBytes = Convert.FromBase64String(encryptedApiKey);
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }

        public static void StoreEncryptedApiKey(string plainApiKey)
        {
            string encryptedApiKey = EncryptApiKey(plainApiKey);
            SaveEncryptedApiKeyToServer(encryptedApiKey);
        }

        private static string EncryptApiKey(string plainApiKey)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] key = Encoding.UTF8.GetBytes(EncryptionKey);
                byte[] iv = new byte[16];

                using (ICryptoTransform encryptor = aes.CreateEncryptor(key, iv))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainApiKey);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        private static void SaveEncryptedApiKeyToServer(string encryptedApiKey)
        {
            string connectionString = "Server=INFPRO-WS109\\MSSQLSERVER01;Database=GifMaster;User ID=sa;Password=dionysys08admin;MultipleActiveResultSets=True;Connect Timeout=5;Pooling=true;"; // Replace with your actual connection string
            string query = "UPDATE ApiKeysTable SET EncryptedApiKey = @EncryptedApiKey WHERE AppName = 'GifMaster'"; // Update table/column names accordingly

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EncryptedApiKey", encryptedApiKey);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}