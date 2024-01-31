using Codice.CM.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ElevenLabsUI : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_HeaderAsset = default;

	[SerializeField]
	private VisualTreeAsset m_SpeechSynthesisAsset = default;

    [SerializeField]
    private VisualTreeAsset m_VoiceLabAsset = default;

    [SerializeField]
    private VisualTreeAsset m_HistoryAsset = default;

    private List<string> voiceList;
    private VoicesInfo voices;

    private readonly IUserService _userService;
    private readonly IHistoryService _historyService;
    private readonly IVoiceService _voiceService;
    public ElevenLabsUI()
    {
        _userService = new UserService();
        _historyService = new HistoryService();
        _voiceService = new VoiceService();
    }

    [MenuItem("Window/Dashboard/ElevenLabs")]
    public static void RenderMainWindow()
    {
        ElevenLabsUI wnd = GetWindow<ElevenLabsUI>();
        wnd.titleContent = new GUIContent("ElevenLabs Dashboard");
    }

	public async void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        VisualElement Header = m_HeaderAsset.Instantiate();
        VisualElement SpeechSynthesis = m_SpeechSynthesisAsset.Instantiate();

		voices = await _voiceService.GetVoices();
		voiceList = new List<VoicesItem>(voices.Voices).ConvertAll(x => x.VoiceId);

		root.Add(Header);
        root.Add(SpeechSynthesis);

        root.Q<ToolbarButton>("Synthesis").clicked += () =>
        {
            RenderSpeechSynthesis();
        };

        root.Q<ToolbarButton>("VoiceLab").clicked += () =>
        {
            RenderVoiceLab();
        };

        root.Q<ToolbarButton>("History").clicked += () =>
        {
            RenderHistory();
        };
    }

    private void RenderSpeechSynthesis()
    {
        VisualElement root = rootVisualElement;
		VisualElement SpeechSynthesis = m_SpeechSynthesisAsset.Instantiate();

		root.RemoveAt(1);
		root.Add(SpeechSynthesis);
		var test = SpeechSynthesis.Q<DropdownField>("Voices");
        test.choices = voiceList;
    }

    private void RenderVoiceLab()
    {
		VisualElement root = rootVisualElement;
		root.RemoveAt(1);
		root.Add(m_VoiceLabAsset.Instantiate());
	}

    private void RenderHistory()
    {
		VisualElement root = rootVisualElement;
		root.RemoveAt(1);
		root.Add(m_HistoryAsset.Instantiate());
	}
}
