using System.Collections.Generic;
using UnityEngine;

public static class ElevenLabsConst
{
    public static string apiKey = "3f948ac75cca970afb5f37e25a320128";
    public static string baseUrl = "https://api.elevenlabs.io/v1/";
    public static string savePath = Application.dataPath + "/ElevenLabs";
    public static List<string> outputFormats = new()
    { "mp3_22050_32", "mp3_44100_32", "mp3_44100_64",
      "mp3_44100_96", "mp3_44100_128"
    };
}