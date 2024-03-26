using Riotsharp.Account.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riotsharp.Account.Interfaces
{
    public interface IAccount
    {
        Task<AccountResponse> GetAccountByPUUID(string puuid);
        
        Task<AccountResponse> GetAccountByRiotID(
            string gameName, 
            string tagLine);
        
        Task<ActiveShardResponse> GetAccountActiveShards(
            string game, 
            string puuid);
    }
}
