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

    public HttpRequester(string baseUri = "https://balance-forms-back.vercel.app/")
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
        HttpResponseMessage response = await _client.GetAsync(_baseUri + endpoint);
        response.EnsureSuccessStatusCode();
        string res = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonBuilder.Deserialize<ApiValues>(res);
        // MessageBox.Show($"{res}");

        if (apiResponse.values.Count > 0)
        {
            var firstItem = apiResponse.values[0];
            return (firstItem.test1.ToArray(), firstItem.test2.ToArray());
        }
        else
        {
            // Handle case where no values are returned
            MessageBox.Show("No values returned from the API.");
            return (null, null);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error: {ex.Message}");
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
            // MessageBox.Show(jsonContent);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            // MessageBox.Show("Algo deu errado ao enviar a resposta.");
            return $"Error: {ex.Message}";
        }
    }
}