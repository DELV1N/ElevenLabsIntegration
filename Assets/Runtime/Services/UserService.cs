using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[Serializable]
public class UserService : IUserService
{
    public async Task<UserInfo> GetUserInfo()
    {
        var uri = $"{ElevenLabsConst.baseUrl}user";
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Add("xi-api-key", ElevenLabsConst.apiKey);

        using (var httpClient = new HttpClient())
        {
            try
            {
                var response = await httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var text = JsonConvert.DeserializeObject<UserInfo>(json);
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

    public async Task<UserSubscriptionInfo> GetUserSubscriptionInfo()
    {
		var uri = $"{ElevenLabsConst.baseUrl}user/subscription";
		var requset = new HttpRequestMessage(HttpMethod.Get, uri);
		requset.Headers.Add("xi-api-key", ElevenLabsConst.apiKey);

		using (var httpClient = new HttpClient())
		{
            try
            {
                var response = await httpClient.SendAsync(requset);
                var json = await response.Content.ReadAsStringAsync();
                var text = JsonConvert.DeserializeObject<UserSubscriptionInfo>(json);
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