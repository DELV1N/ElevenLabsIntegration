using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Scripting;

[Serializable]
public class ElevenLabsGetRequest
{
	public class UserInfo
	{
		[Preserve]
		[JsonConstructor]
		public UserInfo(
				[JsonProperty("is_new_user")] bool isNewUser,
				[JsonProperty("xi_api_key")] string xiApiKey)
		{
			IsNewUser = isNewUser;
			XiApiKey = xiApiKey;
		}

		[Preserve]
		[JsonProperty("is_new_user")]
		public bool IsNewUser { get; }

		[Preserve]
		[JsonProperty("xi_api_key")]
		public string XiApiKey { get; }
	}

	private string _apiKey;
	private string _baseUrl;
	private static HttpClient httpClient = new HttpClient();

	public ElevenLabsGetRequest(string apiKey, string baseUrl)
	{
		_apiKey = apiKey;
		_baseUrl = baseUrl;
	}

	public async Task<string> GetUserInfo()
	{
		var uri = $"{_baseUrl}user";
		var requset = new HttpRequestMessage(HttpMethod.Get, uri);
		requset.Headers.Add("xi-api-key", _apiKey);
		var response = await httpClient.SendAsync(requset);
		var json = await response.Content.ReadAsStringAsync();
		var text = JsonConvert.DeserializeObject<UserInfo>(json).XiApiKey;
		if (response.IsSuccessStatusCode)
			return text;
		throw new Exception(response.StatusCode.ToString());
	}
}