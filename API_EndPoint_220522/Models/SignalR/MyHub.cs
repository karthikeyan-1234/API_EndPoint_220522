using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Models.SignalR
{
    public class MyHub:Hub
    {
        public Task Join() => Groups.AddToGroupAsync(Context.ConnectionId, "group_name");
        public Task Leave() => Groups.RemoveFromGroupAsync(Context.ConnectionId, "group_name");
        public Task Message(string data) => Clients.Groups("group_name").SendAsync("groupMessage", data /*"{id: 1,name:Arjun}"*/);
    }
}
