using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class TextToSpeechService : ITextToSpeech
{

    public async Task<byte[]> GenerateFile(string voiceId, string outputFormat, TextToSpeech Data)
    {
        var uri = $"{ElevenLabsConst.baseUrl}text-to-speech/{voiceId}?output_format={outputFormat}";
        var request = new StringContent(JsonConvert.SerializeObject(Data), Encoding.UTF8, "application/json");
        request.Headers.Add("xi-api-key", ElevenLabsConst.apiKey);

        using (var httpClient = new HttpClient())
        {
            try
            {
                var response = await httpClient.PostAsync(uri, request);
                var file = await response.Content.ReadAsByteArrayAsync();
                if (response.IsSuccessStatusCode)
                    return file;
                throw new Exception(response.StatusCode.ToString());
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.ToString());
            }
        }
    }
}