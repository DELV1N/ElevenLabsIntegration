using Newtonsoft.Json;

public class VoiceSettings
{
    [JsonProperty("similarity_boost")]
    public double SimilarityBoost { get; set; }

    [JsonProperty("stability")]
    public double Stability { get; set; }

    [JsonProperty("style")]
    public double Style { get; set; }

    [JsonProperty("use_speaker_boost")]
    public bool UseSpeakerBoost { get; set; }
}

public class TextToSpeech
{
    [JsonProperty("model_id")]
    public string ModelId { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("voice_settings")]
    public VoiceSettings VoiceSettings { get; set; }
}
