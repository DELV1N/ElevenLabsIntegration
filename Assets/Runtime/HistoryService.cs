using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[Serializable]
public class HistoryService : IHistoryService
{
    private readonly IUserService _userService;

    public HistoryService(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<string> GetUserInfo()
    {
       await _userService.GetUserInfo();
    }
}