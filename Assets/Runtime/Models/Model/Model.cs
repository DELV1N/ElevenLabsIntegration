using Newtonsoft.Json;
using System.Collections.Generic;

public class Models
{
    public Model[] ModelInfo { get; set; }
}
public class Language
{
    [JsonProperty("language_id")]
    public string LanguageId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}

public class Model
{
    [JsonProperty("can_be_finetuned")]
    public bool CanBeFinetuned { get; set; }

    [JsonProperty("can_do_text_to_speech")]
    public bool CanDoTextToSpeech { get; set; }

    [JsonProperty("can_do_voice_conversion")]
    public bool CanDoVoiceConversion { get; set; }

    [JsonProperty("can_use_speaker_boost")]
    public bool CanUseSpeakerBoost { get; set; }

    [JsonProperty("can_use_style")]
    public bool CanUseStyle { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("languages")]
    public List<Language> Languages { get; set; }

    [JsonProperty("max_characters_request_free_user")]
    public int MaxCharactersRequestFreeUser { get; set; }

    [JsonProperty("max_characters_request_subscribed_user")]
    public int MaxCharactersRequestSubscribedUser { get; set; }

    [JsonProperty("model_id")]
    public string ModelId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("requires_alpha_access")]
    public bool RequiresAlphaAccess { get; set; }

    [JsonProperty("serves_pro_voices")]
    public bool ServesProVoices { get; set; }

    [JsonProperty("token_cost_factor")]
    public double TokenCostFactor { get; set; }
}
