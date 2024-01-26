using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[Serializable]
public class UserService : IUserService
{
    public async Task<string> GetUserInfo()
    {
        var uri = $"{ElevenLabsConst.BaseUrl}user";
        var requset = new HttpRequestMessage(HttpMethod.Get, uri);
        requset.Headers.Add("xi-api-key", ElevenLabsConst.ApiKey);
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.SendAsync(requset);
            var json = await response.Content.ReadAsStringAsync();
            var text = JsonConvert.DeserializeObject<UserInfo>(json).XiApiKey;
            if (response.IsSuccessStatusCode)
                return text;
            throw new Exception(response.StatusCode.ToString());
        }
    }
}