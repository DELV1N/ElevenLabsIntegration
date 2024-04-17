using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ElevenLabsUI : EditorWindow
{
    #region Assets

    [SerializeField]
    private VisualTreeAsset m_HeaderAsset = default;

	[SerializeField]
	private VisualTreeAsset m_SpeechSynthesisAsset = default;

    [SerializeField]
    private VisualTreeAsset m_VoiceLabAsset = default;

    [SerializeField]
    private VisualTreeAsset m_HistoryAsset = default;

    [SerializeField]
    private VisualTreeAsset m_HistoryItemAsset = default;

    [SerializeField]
    private VisualTreeAsset m_SaveFolderAsset = default;

    #endregion

    #region UX Content

    private readonly float defStability = 0.5f;
    private readonly float defSimilarity = 0.75f;
    private readonly float defExaggeration = 0.0f;
    private readonly bool defSpeakerBoost = true;
    private readonly int defaultPageSize = 12;

    private DropdownField voices = null;
    private DropdownField models = null;
    private DropdownField formats = null;
    private Button refresh = null;
    private Button reset = null;
    private Button generate = null;
    private Button change = null;
    private Button prevPage = null;
    private Button nextPage = null;
    private Button selectAll = null;
    private Button deselectAll = null;
    private Button downloadItems = null;
    private Button download = null;
    private Button delete = null;
    private TextField text = null;
    private TextField soundText = null;
    private IntegerField pageSize = null;
    private ObjectField soundObject = null;
    private Slider stability = null;
    private Slider similarity = null;
    private Slider exaggeration = null;
    private Toggle speakerBoost = null;
    private Toggle soundData = null;
    private Stack<string> prevPageIds = new();

    private ScrollView scrollView = null;

    private VoicesItem[] voiceItems = null;
    private Model[] modelItems = null;
    private History historyItems = null;
    private List<HistoryItem[]> historyItemsPages = null;

    private static Vector2 windowMinSize = new(700, 550);

    #endregion

    private readonly IVoiceService _voiceService;
    private readonly IModelService _modelService;
    private readonly ITextToSpeechService _textToSpeechService;
    private readonly IHistoryService _historyService;
    public ElevenLabsUI()
    {
        _voiceService = new VoiceService();
        _modelService = new ModelService();
        _textToSpeechService = new TextToSpeechService();
        _historyService = new HistoryService();
    }

    [MenuItem("Window/Dashboard/ElevenLabs")]
    public static void RenderMainWindow()
    {
        ElevenLabsUI wnd = GetWindow<ElevenLabsUI>();
        wnd.titleContent = new GUIContent("ElevenLabs Dashboard");
        wnd.minSize = windowMinSize;
    }

#pragma warning disable CS1998 
    public async void CreateGUI()
#pragma warning restore CS1998 
    {
        // Instantiate UXML
        VisualElement Header = m_HeaderAsset.Instantiate();
        VisualElement SpeechSynthesis = m_SpeechSynthesisAsset.Instantiate();
        VisualElement SaveFolder = m_SaveFolderAsset.Instantiate();
        scrollView = new();

        scrollView.Add(Header);
        scrollView.Add(SaveFolder);
        scrollView.Add(SpeechSynthesis);
        rootVisualElement.Add(scrollView);

        RenderSpeechSynthesis();

        SaveFolder.Q<TextField>("SavePath").value = ElevenLabsConst.savePath;

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
        if (text.value == "" || !text.enabledSelf)
            generate.SetEnabled(false);
        else
            generate.SetEnabled(true);

        rootVisualElement.Q<Label>("MaxSymb").text = text.value.Length.ToString();
    }

    private async void RenderSpeechSynthesis()
    {
		var SpeechSynthesis = m_SpeechSynthesisAsset.Instantiate();

        scrollView.RemoveAt(2);
        scrollView.Add(SpeechSynthesis);

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
        formats = SpeechSynthesis.Q<DropdownField>("Formats");
        change = rootVisualElement.Q<Button>("Change");

        formats.choices = ElevenLabsConst.outputFormats;
        formats.value = ElevenLabsConst.outputFormats[4];

        reset.clicked -= ResetButton_clicked;
        reset.clicked += ResetButton_clicked;
        refresh.clicked -= RefreshButton_clicked;
        refresh.clicked += RefreshButton_clicked;
        generate.clicked -= GenerateButton_clicked;
        generate.clicked += GenerateButton_clicked;
        change.clicked -= ChangeButton_clicked;
        change.clicked += ChangeButton_clicked;

        if (voiceItems == null || modelItems == null)
        {
            await GetVoicesModels();
        }

        voices.choices = voiceItems.Select(x => x.Name).ToList();
        models.choices = modelItems.Select(x => x.Name).ToList();
        voices.value = voiceItems.FirstOrDefault().Name;
        models.value = modelItems.FirstOrDefault().Name;
    }

    private void RenderVoiceLab()
    {
        scrollView.RemoveAt(2);
        scrollView.Add(m_VoiceLabAsset.Instantiate());
	}

    private void RenderHistory()
    {
        var History = m_HistoryAsset.Instantiate();

        scrollView.RemoveAt(2);
        scrollView.Add(History);

        prevPage = History.Q<Button>("PrevPage");
        nextPage = History.Q<Button>("NextPage");
        refresh = History.Q<Button>("Refresh");
        selectAll = History.Q<Button>("SelectAll");
        deselectAll = History.Q<Button>("DeselectAll");
        downloadItems = History.Q<Button>("DownloadItems");
        pageSize = History.Q<IntegerField>("PageSize");

        pageSize.value = defaultPageSize;

        GetHistoryItems();

        refresh.clicked += () => GetHistoryItems();
        nextPage.clicked += () => GetHistoryItems(historyItems.LastHistoryItemId);
        prevPage.clicked += () =>
        {
            if (prevPageIds.TryPeek(out var prevPageId))
                GetHistoryItems(prevPageId);
        };
	}

    private async void GetHistoryItems(string historyId = null)
    {
        var scrlView = scrollView.Q<ScrollView>("ScrollView");

        if (scrlView.childCount != 0)
        {
            scrlView.Clear();
        }
        prevPage.visible = false;
        nextPage.visible = false;
        deselectAll.visible = false;
        refresh.SetEnabled(false);
        selectAll.SetEnabled(false);
        downloadItems.SetEnabled(false);

        if (string.IsNullOrWhiteSpace(historyId) && prevPageIds.Count > 0 && prevPageIds.TryPeek(out var prevPageId))
        {
            historyId = prevPageId;
        }
        else
        {
            if (prevPageIds.TryPeek(out prevPageId) && prevPageId == historyId)
            {
                prevPageIds.Pop();
                historyId = prevPageIds.Count > 0 && prevPageIds.TryPeek(out prevPageId) ? prevPageId : null;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(historyId))
                {
                    prevPageIds.Push(historyId);
                }
            }
        }

        historyItems = null;
        var list = await _historyService.GetGeneratedItems(pageSize.value, historyId);
        historyItems = list;

        var historyItemsCount = historyItems.HistoryItems.Count();

        for (var i = 0; i < historyItemsCount; i++)
        {
            scrlView.Add(m_HistoryItemAsset.Instantiate());
        }

        prevPage.visible = true;
        nextPage.visible = true;
        deselectAll.visible = true;
        refresh.SetEnabled(true);
        selectAll.SetEnabled(true);
        downloadItems.SetEnabled(true);
    }

    #region SpeechSynthesis methods
    private void ChangeButton_clicked()
    {
        ElevenLabsConst.savePath = EditorUtility.SaveFolderPanel("Select save folder path", "", "");
        rootVisualElement.Q<TextField>("SavePath").value = ElevenLabsConst.savePath;
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
            formats.value = ElevenLabsConst.outputFormats[4];
        }
    }

    private async void RefreshButton_clicked()
    {
        await GetVoicesModels();
    }

    private async void GenerateButton_clicked()
    {
        text.SetEnabled(false);
        generate.SetEnabled(false);

        var byteArr = await Generate();
        var historyItem = (await _historyService.GetGeneratedItems(1, null)).HistoryItems.ToList().FirstOrDefault();
        var filePath = ElevenLabsConst.savePath + "/" + historyItem.VoiceName + "/";

        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);
        File.WriteAllBytes(filePath + $"{historyItem.HistoryItemId}.mp3", byteArr);

        text.SetEnabled(true);
        generate.SetEnabled(true);
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

        return (await _textToSpeechService.GenerateFile(voiceItems.Where(x => x.Name == voices.value).FirstOrDefault().VoiceId, formats.value, Data));
    }

    private async Task GetVoicesModels()
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
    }
    #endregion
}