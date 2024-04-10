using System.Threading.Tasks;

public interface ITextToSpeechService
{
    Task<byte[]> GenerateFile(string voiceId, string output_format, TextToSpeech data);
}