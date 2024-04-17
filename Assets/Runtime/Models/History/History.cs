using Newtonsoft.Json;
using System.Collections.Generic;

public class Feedback
{
    [JsonProperty("audio_quality")]
    public bool AudioQuality { get; set; }

    [JsonProperty("emotions")]
    public bool Emotions { get; set; }

    [JsonProperty("feedback")]
    public string FeedbackText { get; set; }

    [JsonProperty("glitches")]
    public bool Glitches { get; set; }

    [JsonProperty("inaccurate_clone")]
    public bool InaccurateClone { get; set; }

    [JsonProperty("other")]
    public bool Other { get; set; }

    [JsonProperty("review_status")]
    public string ReviewStatus { get; set; }

    [JsonProperty("thumbs_up")]
    public bool ThumbsUp { get; set; }
}

public class HistoryItem
{
    [JsonProperty("character_count_change_from")]
    public double CharacterCountChangeFrom { get; set; }

    [JsonProperty("character_count_change_to")]
    public double CharacterCountChangeTo { get; set; }

    [JsonProperty("content_type")]
    public string ContentType { get; set; }

    [JsonProperty("date_unix")]
    public double DateUnix { get; set; }

    [JsonProperty("feedback")]
    public Feedback Feedback { get; set; }

    [JsonProperty("history_item_id")]
    public string HistoryItemId { get; set; }

    [JsonProperty("model_id")]
    public string ModelId { get; set; }

    [JsonProperty("request_id")]
    public string RequestId { get; set; }

    [JsonProperty("settings")]
    public VoiceSettings Settings { get; set; }

    [JsonProperty("share_link_id")]
    public string ShareLinkId { get; set; }

    [JsonProperty("source")]
    public string Source { get; set; }

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

public class History
{
    [JsonProperty("has_more")]
    public bool HasMore { get; set; }

    [JsonProperty("history")]
    public HistoryItem[] HistoryItems { get; set; }

    [JsonProperty("last_history_item_id")]
    public string LastHistoryItemId { get; set; }
}
