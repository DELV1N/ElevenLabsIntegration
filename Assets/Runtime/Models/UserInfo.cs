using Newtonsoft.Json;
using UnityEngine.Scripting;

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