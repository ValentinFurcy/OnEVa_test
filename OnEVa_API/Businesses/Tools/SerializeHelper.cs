using Microsoft.AspNetCore.Http;
using System.IO.Pipelines;
using System.Text.Json;

namespace OnEVa_API.Tools
{
    public class SerializeHelper
    {
        public async Task<List<T>> DeserializeFileAsync<T>(string? nameFile, IFormFile? personFileJSON)
        {
            try
            {
                if (nameFile != null)
                {
                    if (File.Exists(nameFile))
                    {
                        var fileContent = File.ReadAllText(nameFile);
                        var data = JsonSerializer.Deserialize<List<T>>(fileContent);
                        if (data != null && data.Any())
                        {
                            return data;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else return null;
                }
                else if (personFileJSON != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        await personFileJSON.CopyToAsync(stream);
                        stream.Position = 0;
                        using (var reader = new StreamReader(stream))
                        {
                            var fileContent = await reader.ReadToEndAsync();
                            var data = JsonSerializer.Deserialize<List<T>>(fileContent);
                            return data != null && data.Any() ? data : null;
                        }
                    }
                }
                else return null; 
            }
            catch (Exception e)
            {
                throw new Exception("Error deserializing file: " + e.Message);
            }
        }
    }
}
