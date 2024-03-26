using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riotsharp.Account.Models;

public sealed record ActiveShardResponse(
    string Puuid,
    string Game, 
    string ActiveShard);
