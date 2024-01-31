using System.Threading.Tasks;

public interface IVoiceService
{
	Task<VoicesInfo> GetVoices();
}