using System.Collections.Generic;
using UnityEngine;

public static class ElevenLabsConst
{
    public static string apiKey = "9e24ab5f805316136620a22c04078ca9";
    public static string baseUrl = "https://api.elevenlabs.io/v1/";
    public static string savePath = Application.dataPath + "/ElevenLabs";
    public static List<string> outputFormats = new()
    { "mp3_22050_32", "mp3_44100_32", "mp3_44100_64",
      "mp3_44100_96", "mp3_44100_128"
    };
}