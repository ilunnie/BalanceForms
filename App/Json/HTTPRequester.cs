using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class HttpRequester
{
    private readonly HttpClient _client;
    private readonly string _baseUri;

    public HttpRequester(string baseUri = "http://127.0.0.1:5000/")
    {
        _client = new HttpClient();
        _baseUri = baseUri;
    }

    public async Task<string> GetResAsync(string endpoint)
    {
        try
        {
            // MessageBox.Show(_baseUri + endpoint);
            HttpResponseMessage response = await _client.GetAsync(_baseUri + endpoint);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
            // MessageBox.Show("Response from server:\n" + a);
            // return a;
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    public async Task<(int[], int[])> GetValuesAsync(string endpoint)
    {
        try
        {
            // MessageBox.Show(_baseUri + endpoint);
            HttpResponseMessage response = await _client.GetAsync(_baseUri + endpoint);

            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            ApiValues apiValues = JsonBuilder.Deserialize<ApiValues>(res);

            // Extract the values from prova1 and prova2 properties
            List<int> prova1Values = apiValues.prova1;
            List<int> prova2Values = apiValues.prova2;
            // MessageBox.Show(prova1Values[2].ToString());

            return (prova1Values.ToArray(), prova2Values.ToArray());
        }
        catch (Exception ex)
        {
            MessageBox.Show( $"Error: {ex.Message}");
            return (null, null);
        }
    }

    public async Task<string> PostAsync(string endpoint, string jsonContent)
    {
        try
        {
            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_baseUri + endpoint, content);
            response.EnsureSuccessStatusCode();
            // response.Dispose();
            System.Console.WriteLine(jsonContent);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Algo deu errado ao enviar a resposta.");
            return $"Error: {ex.Message}";
        }
    }
}