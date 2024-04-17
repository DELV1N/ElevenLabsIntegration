using System.Threading.Tasks;

public interface IHistoryService
{
    Task<History> GetGeneratedItems();
    Task<History> GetGeneratedItems(int pageSize, string startAfterId);
}