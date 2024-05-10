using System.IO;
using System.Threading.Tasks;

public interface IHistoryService
{
    Task<History> GetGeneratedItems();
    Task<History> GetGeneratedItems(int pageSize, string startAfterId);
    Task<byte[]> GetAudioFromHistoryItem(string historyItemId);
    Task<object> DeleteHistoryItem(string historyItemId);
}