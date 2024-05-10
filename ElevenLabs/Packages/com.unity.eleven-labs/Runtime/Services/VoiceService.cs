using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[Serializable]
public class VoiceService : IVoiceService
{
	public async Task<VoicesInfo> GetVoices()
	{
		var uri = $"{ElevenLabsConst.baseUrl}voices";
		var request = new HttpRequestMessage(HttpMethod.Get, uri);
		request.Headers.Add("xi-api-key", ElevenLabsConst.apiKey);

		using (var httpClient = new HttpClient())
		{
			try
			{
				var response = await httpClient.SendAsync(request);
				var json = await response.Content.ReadAsStringAsync();
				var text = JsonConvert.DeserializeObject<VoicesInfo>(json);
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