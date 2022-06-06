using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Models.SignalR
{
    public interface IHubClient
    {
        public Task BroadcastMessage();
    }
}
