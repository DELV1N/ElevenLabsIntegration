using System.Threading.Tasks;

public interface IUserService
{
    Task<UserInfo> GetUserInfo();
    Task<UserSubscriptionInfo> GetUserSubscriptionInfo();
}