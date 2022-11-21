using HeatHomeApp.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatHomeApp.Services
{
    public class SignalRService
    {
        public static HubConnection Hub;
        public static event EventHandler<HeatingUnit> UpdateTemperatureEvent;
        public SignalRService()
        {
            Hub ??= new HubConnectionBuilder().WithUrl("http://192.168.1.100:45455/SignalR").Build();
            Init();
        }

        private void Init()
        {
            RegisterMethod<HeatingUnit>("UpdateHeatingUnit", UpdateTemperature);
        }

        private void UpdateTemperature(HeatingUnit heatingUnit)
        {
            UpdateTemperatureEvent.Invoke(this, heatingUnit);
        }

        public static async Task Start()
        {
            if (Hub.State == HubConnectionState.Disconnected)
            {
                await Hub.StartAsync();
            }
        }

       public void RegisterMethod<T>(string methodName, Action<T> method)
        {
            Hub.On(methodName, method);
        }
        
    }
}
