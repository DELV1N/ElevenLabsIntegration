using Newtonsoft.Json;

public class Subscription
{
	[JsonProperty("allowed_to_extend_character_limit")]
	public bool AllowedToExtendCharacterLimit { get; set; }

	[JsonProperty("can_extend_character_limit")]
	public bool CanExtendCharacterLimit { get; set; }

	[JsonProperty("can_extend_voice_limit")]
	public bool CanExtendVoiceLimit { get; set; }

	[JsonProperty("can_use_instant_voice_cloning")]
	public bool CanUseInstantVoiceCloning { get; set; }

	[JsonProperty("can_use_professional_voice_cloning")]
	public bool CanUseProfessionalVoiceCloning { get; set; }

	[JsonProperty("character_count")]
	public int CharacterCount { get; set; }

	[JsonProperty("character_limit")]
	public int CharacterLimit { get; set; }

	[JsonProperty("currency")]
	public string Currency { get; set; }

	[JsonProperty("max_voice_add_edits")]
	public int MaxVoiceAddEdits { get; set; }

	[JsonProperty("next_character_count_reset_unix")]
	public int NextCharacterCountResetUnix { get; set; }

	[JsonProperty("professional_voice_limit")]
	public int ProfessionalVoiceLimit { get; set; }

	[JsonProperty("status")]
	public string Status { get; set; }

	[JsonProperty("tier")]
	public string Tier { get; set; }

	[JsonProperty("voice_add_edit_counter")]
	public int VoiceAddEditCounter { get; set; }

	[JsonProperty("voice_limit")]
	public int VoiceLimit { get; set; }
}

public class UserInfo
{
	[JsonProperty("can_use_delayed_payment_methods")]
	public bool CanUseDelayedPaymentMethods { get; set; }

	[JsonProperty("first_name")]
	public string FirstName { get; set; }

	[JsonProperty("is_new_user")]
	public bool IsNewUser { get; set; }

	[JsonProperty("is_onboarding_completed")]
	public bool IsOnboardingCompleted { get; set; }

	[JsonProperty("subscription")]
	public Subscription Subscription { get; set; }

	[JsonProperty("xi_api_key")]
	public string XiApiKey { get; set; }
}