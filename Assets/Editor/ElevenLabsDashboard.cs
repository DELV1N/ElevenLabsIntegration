using UnityEditor;
using UnityEngine;

public class ElevenLabsEditor : EditorWindow
{
	#region Свойства

	private const int TabWidth = 18;
	private const int EndWidth = 10;
	private const float WideColumnWidth = 128f;

	private static readonly GUIContent saveDirectoryContent = new GUIContent("Save Directory");
	private static readonly GUIContent guiTitleContent = new GUIContent("ElevenLabs Dashboard");
	private static readonly string[] tabTitles = { "Speech Synthesis", "Voice Lab", "History" };

	private Vector2 scrollPosition = Vector2.zero;

	private static string DefaultSaveDirectoryKey => $"{Application.productName}_ElevenLabs";
	private static string DefaultSaveDirectory => $"{Application.dataPath}/ElevenLabs";
	private static string editorDownloadDirectory = string.Empty;
	private static GUIStyle boldCenteredHeaderStyle;
	private static GUIStyle BoldCenteredHeaderLabel
	{
		get
		{
			boldCenteredHeaderStyle = new GUIStyle(EditorStyles.whiteLargeLabel)
			{
				alignment = TextAnchor.MiddleCenter,
				fontSize = 18,
				padding = new RectOffset(0, 0, -8, -8)
			};
			return boldCenteredHeaderStyle;
		}
	}

	private static readonly GUILayoutOption[] expandWidthOption =
	{
		GUILayout.ExpandWidth(true)
	};

	private static readonly GUILayoutOption[] wideColumnWidthOption =
	{
		GUILayout.Width(WideColumnWidth)
	};

	[SerializeField]
	private int tab;

	#endregion

	[MenuItem("Window/Dashboard/ElevenLabs")]

	#region Основные методы
	// Создание окна
	public static void OpenWindow()
	{
		var wnd = GetWindow<ElevenLabsEditor>();
		wnd.Show();
		wnd.titleContent = guiTitleContent;
	}

	// Окно активно
	private void OnEnable()
	{
		titleContent = guiTitleContent;
		minSize = new Vector2(WideColumnWidth * 5, WideColumnWidth * 4);
	}

	// Фокус падает на окно
	private void OnFocus()
	{
		
	}

	// Отображение визуала
	public void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, expandWidthOption);
		EditorGUILayout.BeginHorizontal();
		GUILayout.Space(TabWidth);
		EditorGUILayout.BeginVertical();
		{
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("ElevenLabs Dashboard", BoldCenteredHeaderLabel);

			EditorGUILayout.Space();
			EditorGUI.BeginChangeCheck();
			tab = GUILayout.Toolbar(tab, tabTitles, expandWidthOption);

			if (EditorGUI.EndChangeCheck())
				GUI.FocusControl(null);

			EditorGUILayout.LabelField(saveDirectoryContent);
			if (string.IsNullOrWhiteSpace(editorDownloadDirectory))
				editorDownloadDirectory = EditorPrefs.GetString(DefaultSaveDirectoryKey, DefaultSaveDirectory);

			EditorGUILayout.BeginHorizontal();
			{
				EditorGUILayout.TextField(editorDownloadDirectory, expandWidthOption);
				if (GUILayout.Button("Reset", wideColumnWidthOption))
				{
					editorDownloadDirectory = DefaultSaveDirectory;
					EditorPrefs.SetString(DefaultSaveDirectoryKey, editorDownloadDirectory);
				}
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			{
				if (GUILayout.Button("Change Save Directory", expandWidthOption))
				{
					EditorApplication.delayCall += () =>
					{
						var result = EditorUtility.OpenFolderPanel("Save Directory", editorDownloadDirectory, string.Empty);
						if (!string.IsNullOrWhiteSpace(result))
						{
							editorDownloadDirectory = result;
							EditorPrefs.SetString(DefaultSaveDirectory, editorDownloadDirectory);
						}
					};
				}
			}
			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndVertical();
		GUILayout.Space(EndWidth);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		EditorGUI.indentLevel++;

		switch (tab)
		{
			case 0:
				break;
			case 1:
				break; 
			case 2:
				break;
		}

		EditorGUI.indentLevel--;
		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndVertical();
	}

	#endregion

}
