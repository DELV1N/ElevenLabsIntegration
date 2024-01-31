using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[Serializable]
public class HistoryService : IHistoryService
{
    public async Task<string> GetGeneratedItem()
    {
        return string.Empty;
    }

    public async Task<string> GetHistoryItemById()
    {
        return string.Empty;
    }

    public async Task<string> DeleteHistoryItem(string id)
    {
        return id;
    }

    public async Task<string> GetAudioFromHistoryItem(string id)
    {
        return id;
    }

    public async Task<string[]> DownloadHistoryItems(string[] ids)
    {
        return ids;
    }
}