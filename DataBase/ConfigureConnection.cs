using System.Text.Json;

namespace DataBase
{
    internal class ConfigureConnection
    {
        private const string _pathToJsonFile = @"C:\Users\Стас\source\repos\DataBase\DataBase\bin\Debug\net6.0\Connections.json";
        public static string GetConnectionStringFromJSON()
        {
            using (JsonDocument jsonDocument = JsonDocument.Parse(File.OpenRead(_pathToJsonFile)))
            {
                JsonElement root = jsonDocument.RootElement;
                JsonElement jsonConnString = root.GetProperty("Connections").GetProperty("DefaultConnection");
                string connectionString = jsonConnString.GetString()!;
                return connectionString;
            }        
        }
    }
}
