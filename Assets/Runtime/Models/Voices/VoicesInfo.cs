using Newtonsoft.Json;

public class VoicesInfo
{
	[JsonProperty("voices")]
	public VoicesItem[] Voices { get; set; }
}

public class VoicesItem
{ 
	[JsonProperty("voice_id")]
	public string VoiceId { get; set; }
}