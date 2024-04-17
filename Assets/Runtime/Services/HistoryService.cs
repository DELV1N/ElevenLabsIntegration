using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[Serializable]
public class HistoryService : IHistoryService
{
    public async Task<History> GetGeneratedItems()
    {
        var uri = $"{ElevenLabsConst.baseUrl}history";
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Add("xi-api-key", ElevenLabsConst.apiKey);

        using (var httpClient = new HttpClient())
        {
            try
            {
                var response = await httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var text = JsonConvert.DeserializeObject<History>(json);
                if (response.IsSuccessStatusCode)
                    return text;
                throw new Exception(response.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }

    public async Task<History> GetGeneratedItems(int pageSize, string startAfterId)
    {
        string uri;

        if (!string.IsNullOrWhiteSpace(startAfterId))
            uri = $"{ElevenLabsConst.baseUrl}history?page_size={pageSize}&start_after_history_item_id={startAfterId}";
        else
            uri = $"{ElevenLabsConst.baseUrl}history?page_size={pageSize}";

        var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("xi-api-key", ElevenLabsConst.apiKey);

        using (var httpClient = new HttpClient())
        {
            try
            {
                var response = await httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var text = JsonConvert.DeserializeObject<History>(json);
                if (response.IsSuccessStatusCode)
                    return text;
                throw new Exception(response.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}