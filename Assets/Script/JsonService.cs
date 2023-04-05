using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class JsonService : MonoBehaviour
{
    private readonly HttpClient _httpClient = new HttpClient();
    public Text textObject; // ссылка на объект Text на сцене
    public async Task<T> GetJsonAsync<T>(string url) where T : class
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(json);
        return result;
    }
    // Отображение полученных данных на сцене
    private async void Start()
    {
        var data = await GetJsonAsync<Example>("https://jsonplaceholder.typicode.com/posts/1");
        textObject.text = data?.userId.ToString() ?? "Data not found";
    }
}
// Пример класса для десериализации JSON-объекта
public class Example
{
    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public string body { get; set; }
}
