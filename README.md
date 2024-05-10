![image](https://github.com/DELV1N/ElevenLabsIntegration/assets/71463243/d60925c5-fa00-44fb-a953-55226ecde1bd)# ElevenLabs Integration in Unity Game Engine

This is a RESTful Client for Untiy Game Engine which integrates the ElevenLabs Neural Network model for generating dialoge audio files.

## Insalling instruction
```
____
```
### Via Package Manager
1. Open your Unity Package Manager in your project
2. Click "+" button and select "Add package from git URL"
![image](https://github.com/DELV1N/ElevenLabsIntegration/assets/71463243/1f18ceb6-1f65-45c5-ac2b-a9fafb5a4ce4)
3. Insert repository URL with attribute .git#main "https://github.com/DELV1N/ElevenLabsIntegration.git#main"
![image](https://github.com/DELV1N/ElevenLabsIntegration/assets/71463243/b8439ebb-9fd1-46d7-add2-86cd57ecd8c8)
### Via manifest file
1. Open your Unity project folder in explorer
2. Select /Packages folder and open manifest.json file
3. Paste the code as shown below
```C#
{
  "dependencies": {
    "com.unity.eleven-labs": "https://github.com/DELV1N/ElevenLabsIntegration.git",
    //
  }
  //
}
```
```
____
```
## Documentation
```
____
```
### Authentication
To connect your API key, you need to do the following steps
1. In Unity Inspector window find package
![image](https://github.com/DELV1N/ElevenLabsIntegration/assets/71463243/44400b19-1e73-49b8-b090-ee09da7fd9eb)
2. Open ElevenLabsConst file in Runtime folder
3. Insert your API key in apiKey variable
```C#
public static string apiKey = "";
```

### Editor Dashboard
Package allows you to generate audio files and download them later
Open dashboard by clicking on "Window" button and select **Window/Dashboard/ElevenLabs**
![image](https://github.com/DELV1N/ElevenLabsIntegration/assets/71463243/ce792d9e-1cc1-4997-9774-ab3e43703b69)

## SpeechSynthesis
On this page you can generate audio files like on ElevenLabs website. Default voices and settings are available to choose from. Generated files are automatically saved to the selected folder (separate folder is created for each voice)
![image](https://github.com/DELV1N/ElevenLabsIntegration/assets/71463243/6dce699d-f49a-4ed1-8d57-b8f89ff42276)

## History
On this page you can download previously generated audio files
![image](https://github.com/DELV1N/ElevenLabsIntegration/assets/71463243/1d666c7c-2a9e-4b17-b105-a09384ed7523)
