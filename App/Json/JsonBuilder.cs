using System.IO;
using System.Text.Json;

public class JsonBuilder
{
    private TestResult json;

    public JsonBuilder(string name, string nasc, Test test1 = null, Test test2 = null)
    {
        this.json = new TestResult
        {
            nome = name,
            nascimento = nasc,
            prova1 = test1 ?? new Test(),
            prova2 = test2 ?? new Test()
        };
    }

    public JsonBuilder AddProva1(Test test)
    {
        json.prova1 = test;
        return this;
    }

    public JsonBuilder AddProva2(Test test)
    {
        json.prova2 = test;
        return this;
    }

    public JsonBuilder SetName(string name)
    {
        json.nome = name;
        return this;
    }

    public JsonBuilder SetBirth(string nasc)
    {
        json.nascimento = nasc;
        return this;
    }

    public TestResult Build()
    {
        return json;
    }
    public static ApiResponse Deserialize(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<ApiResponse>(json);
        }
        catch
        {
            // Handle or log the exception as needed
            return new ApiResponse { response = 0 };
        }
    }

    public static string Serialize<T>(T obj)
    {
        // Create a memory stream to hold the serialized JSON
        using (MemoryStream stream = new MemoryStream())
        {
            // Create a JsonSerializer object
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true // If you want the JSON to be indented for readability
            };
            // Serialize the object to JSON and write it to the stream
            JsonSerializer.Serialize(stream, obj, options);

            // Reset the stream position to the beginning
            stream.Position = 0;

            // Read the serialized JSON from the stream
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}