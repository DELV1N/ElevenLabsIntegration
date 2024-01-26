using System.Threading.Tasks;

public interface IHistoryService
{
    Task<string> GetGeneratedItem();
    Task<string> GetHistoryItemById();
    Task<string> DeleteHistoryItem(string id);
    Task<string> GetAudioFromHistoryItem(string id);
    Task<string> DownloadHistoryItems(string[] ids);
}