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
            var text = new ElevenLabsGetRequest("9e24ab5f805316136620a22c04078ca9", "https://api.elevenlabs.io/v1/");
            Debug.Log(await text.GetUserInfo());
        };
    }
}
