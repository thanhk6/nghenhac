using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VSW.Lib.Global
{
    [HubName("signalR")]
    public class HubData<T> : Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public static void SendData(T item)
        {
            GlobalHost.ConnectionManager.GetHubContext<HubData<T>>().Clients.All.sendData(item, item.GetType().ToString());
        }

        public static void SendData(List<T> list)
        {
            GlobalHost.ConnectionManager.GetHubContext<HubData<T>>().Clients.All.sendData(list, list.GetType().ToString());
        }
    }
}