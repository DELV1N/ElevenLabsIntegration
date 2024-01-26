using Codice.CM.Common;
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

    private readonly IUserService _userService;
    private readonly IHistoryService _historyService;
    public ElevenLabsUI()
    {
        _userService = new UserService();
        _historyService = new HistoryService(_userService);
    }

    [MenuItem("Window/UI Toolkit/ElevenLabs")]
    public static void ShowExample()
    {
        ElevenLabsUI wnd = GetWindow<ElevenLabsUI>();
        wnd.titleContent = new GUIContent("ElevenLabs Dashboard");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        VisualElement Header = m_HeaderAsset.Instantiate();
        VisualElement SpeechSynthesis = m_SpeechSynthesisAsset.Instantiate();
		root.Add(Header);
        root.Add(SpeechSynthesis);
        SetupButtonHandler();
    }

    private void SetupButtonHandler()
    {
        VisualElement root = rootVisualElement;

        root.Q<ToolbarButton>("Synthesis").clicked += async () =>
        {            
            Debug.Log(await _userService.GetUserInfo());
        };
    }
}
