using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API.Shared.Helpers
{
    public class QueryHelper
    {
        private readonly string _filePath;
        public QueryHelper(string filePath)
        {
            _filePath = filePath;
        }

        public string GetQuery(string key)
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    throw new FileNotFoundException($"Query JSON file not found at {_filePath}");
                }
                using (var streamReader = new StreamReader(_filePath))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    while (jsonReader.Read())
                    {
                        if (jsonReader.TokenType == JsonToken.PropertyName && (string)jsonReader.Value! == key)
                        {
                            jsonReader.Read(); // Read the start of the object
                            var queryObject = JObject.Load(jsonReader);
                            return queryObject["Query"]!.ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            throw new KeyNotFoundException($"The key '{key}' was not found in the JSON file.");
        }

    }
}