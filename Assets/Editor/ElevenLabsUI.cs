using Codice.CM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using Codice.Client.Common;

public class ElevenLabsUI : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_HeaderAsset
        = default;

	[SerializeField]
	private VisualTreeAsset m_SpeechSynthesisAsset = default;

    [SerializeField]
    private VisualTreeAsset m_VoiceLabAsset = default;

    [SerializeField]
    private VisualTreeAsset m_HistoryAsset = default;

    [SerializeField]
    private VisualTreeAsset m_DirectoryAsset = default;

    private readonly float defStability = 0.5f;
    private readonly float defSimilarity = 0.75f;
    private readonly float defExaggeration = 0.0f;
    private readonly bool defSpeakerBoost = true;

    private DropdownField voices = null;
    private DropdownField models = null;
    private Button refresh = null;
    private Button reset = null;
    private Button generate = null;
    private TextField text = null;
    private Slider stability = null;
    private Slider similarity = null;
    private Slider exaggeration = null;
    private Toggle speakerBoost = null;

    private VoicesItem[] voiceItems = null;
    private Model[] modelItems = null;

    private readonly IVoiceService _voiceService;
    private readonly IModelService _modelService;
    private readonly ITextToSpeech _textToSpeechService;
    public ElevenLabsUI()
    {
        _voiceService = new VoiceService();
        _modelService = new ModelService();
        _textToSpeechService = new TextToSpeechService();
    }

    private static Vector2 windowMinSize = new(700, 500);

    [MenuItem("Window/Dashboard/ElevenLabs")]
    public static void RenderMainWindow()
    {
        ElevenLabsUI wnd = GetWindow<ElevenLabsUI>();
        wnd.titleContent = new GUIContent("ElevenLabs Dashboard");
        wnd.minSize = windowMinSize;
    }

#pragma warning disable CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
    public async void CreateGUI()
#pragma warning restore CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
    {
        // Instantiate UXML
        VisualElement Header = m_HeaderAsset.Instantiate();
        VisualElement SpeechSynthesis = m_SpeechSynthesisAsset.Instantiate();
        VisualElement Direcotry = m_DirectoryAsset.Instantiate();

        rootVisualElement.Add(Header);
        rootVisualElement.Add(SpeechSynthesis);

        RenderSpeechSynthesis();

        rootVisualElement.Add(Direcotry);
        Direcotry.Q<TextField>("SavePath").value = ElevenLabsConst.savePath;

        rootVisualElement.Q<ToolbarButton>("Synthesis").clicked += () =>
        {
            RenderSpeechSynthesis();
        };

        rootVisualElement.Q<ToolbarButton>("VoiceLab").clicked += () =>
        {
            RenderVoiceLab();
        };

        rootVisualElement.Q<ToolbarButton>("History").clicked += () =>
        {
            RenderHistory();
        };
    }

    public void OnGUI()
    {
        if (text.value == "")
            generate.SetEnabled(false);
        else
            generate.SetEnabled(true);
    }

    private async void RenderSpeechSynthesis()
    {
		var SpeechSynthesis = m_SpeechSynthesisAsset.Instantiate();

        rootVisualElement.RemoveAt(1);
        rootVisualElement.Add(SpeechSynthesis);

		voices = SpeechSynthesis.Q<DropdownField>("Voices");
        models = SpeechSynthesis.Q<DropdownField>("Models");
        refresh = SpeechSynthesis.Q<Button>("Refresh");
        reset = SpeechSynthesis.Q<Button>("Reset");
        text = SpeechSynthesis.Q<TextField>("Text");
        generate = SpeechSynthesis.Q<Button>("Generate");
        stability = SpeechSynthesis.Q<Slider>("Stability");
        similarity = SpeechSynthesis.Q<Slider>("Similarity");
        exaggeration = SpeechSynthesis.Q<Slider>("Exaggeration");
        speakerBoost = SpeechSynthesis.Q<Toggle>("SpeakerBoost");

        reset.clicked -= ResetButton_clicked;
        reset.clicked += ResetButton_clicked;
        refresh.clicked -= RefreshButton_clicked;
        refresh.clicked += RefreshButton_clicked;
        generate.clicked -= GenerateButton_clicked;
        generate.clicked += GenerateButton_clicked;

        if (voiceItems == null || modelItems == null)
        {
            await GetData();
        }
    }

    private void RenderVoiceLab()
    {
		rootVisualElement.RemoveAt(1);
		rootVisualElement.Add(m_VoiceLabAsset.Instantiate());
	}

    private void RenderHistory()
    {
		rootVisualElement.RemoveAt(1);
		rootVisualElement.Add(m_HistoryAsset.Instantiate());
	}

    private void ResetButton_clicked()
    {
        if (voiceItems != null && modelItems != null)
        {
            voices.value = voiceItems.FirstOrDefault().Name;
            models.value = modelItems.FirstOrDefault().Name;
            stability.value = defStability;
            similarity.value = defSimilarity;
            exaggeration.value = defExaggeration;
            speakerBoost.value = defSpeakerBoost;
        }
    }

    private async void RefreshButton_clicked()
    {
        await GetData();
    }

    private async void GenerateButton_clicked()
    {
        var byteArr = await Generate();
        File.WriteAllBytes(Application.dataPath + "/name.mp3", byteArr);
    }

    private async Task<byte[]> Generate()
    {
        TextToSpeech Data = new()
        {
            ModelId = modelItems.Where(x => x.Name == models.value).FirstOrDefault().ModelId,
            Text = text.value,
            VoiceSettings = new VoiceSettings
            {
                SimilarityBoost = similarity.value,
                Stability = stability.value,
                Style = exaggeration.value,
                UseSpeakerBoost = speakerBoost.value
            }
        };

        return (await _textToSpeechService.GenerateFile(voiceItems.Where(x => x.Name == voices.value).FirstOrDefault().VoiceId, ElevenLabsConst.outputFormats[4], Data));
    }

    private async Task GetData()
    {
        voices.SetEnabled(false);
        models.SetEnabled(false);
        refresh.SetEnabled(false);
        reset.SetEnabled(false);
        text.SetEnabled(false);
        generate.SetEnabled(false);
        
        modelItems = await _modelService.GetModels();
        voiceItems = (await _voiceService.GetVoices()).Voices;

        voices.value = voiceItems.FirstOrDefault().Name;
        models.value = modelItems.FirstOrDefault().Name;

        voices.SetEnabled(voiceItems.Any());
        models.SetEnabled(modelItems.Any());
        refresh.SetEnabled(true);
        reset.SetEnabled(true);
        text.SetEnabled(true);
        generate.SetEnabled(true);

        voices.choices = voiceItems.Select(x => x.Name).ToList();
        models.choices = modelItems.Select(x => x.Name).ToList();
    }
}
