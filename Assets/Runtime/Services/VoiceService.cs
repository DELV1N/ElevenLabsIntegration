using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[Serializable]
public class VoiceService : IVoiceService
{
	public async Task<VoicesInfo> GetVoices()
	{
		var uri = $"{ElevenLabsConst.BaseUrl}voices";
		var requset = new HttpRequestMessage(HttpMethod.Get, uri);
		requset.Headers.Add("xi-api-key", ElevenLabsConst.ApiKey);
		using (var httpClient = new HttpClient())
		{
			var response = await httpClient.SendAsync(requset);
			var json = await response.Content.ReadAsStringAsync();
			var text = JsonConvert.DeserializeObject<VoicesInfo>(json);
			if (response.IsSuccessStatusCode)
				return text;
			throw new Exception(response.StatusCode.ToString());
		}
	}
}