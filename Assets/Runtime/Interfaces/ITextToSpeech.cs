using System.Threading.Tasks;

public interface ITextToSpeech
{
    Task<byte[]> GenerateFile(string voiceId, string output_format, TextToSpeech data);
}