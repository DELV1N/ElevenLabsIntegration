using System.Threading.Tasks;

public interface IModelService
{
    Task<Model[]> GetModels();
}