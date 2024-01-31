using Newtonsoft.Json;

public class HistoryItemById
{
	[JsonProperty("character_count_change_from")]
	public int CharacterCountChangeFrom { get; set; }

	[JsonProperty("character_count_change_to")]
	public int CharacterCountChangeTo { get; set; }

	[JsonProperty("content_type")]
	public string ContentType { get; set; }

	[JsonProperty("date_unix")]
	public int DateUnix { get; set; }

	[JsonProperty("feedback")]
	public Feedback Feedback { get; set; }

	[JsonProperty("history_item_id")]
	public string HistoryItemId { get; set; }

	[JsonProperty("model_id")]
	public string ModelId { get; set; }

	[JsonProperty("request_id")]
	public string RequestId { get; set; }

	[JsonProperty("settings")]
	public object Settings { get; set; }

	[JsonProperty("share_link_id")]
	public string ShareLinkId { get; set; }

	[JsonProperty("state")]
	public string State { get; set; }

	[JsonProperty("text")]
	public string Text { get; set; }

	[JsonProperty("voice_category")]
	public string VoiceCategory { get; set; }

	[JsonProperty("voice_id")]
	public string VoiceId { get; set; }

	[JsonProperty("voice_name")]
	public string VoiceName { get; set; }
}