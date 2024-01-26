using System.Threading.Tasks;

public interface IUserService
{
    Task<string> GetUserInfo();
}