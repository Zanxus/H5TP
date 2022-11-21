using Microsoft.AspNetCore.SignalR;
using System.Net.WebSockets;
using TPAPI.Application.Services;
using TPAPI.Domain.Interfaces.Services;

namespace TPAPI.Application.Hubs
{
    public class TPHub : Hub
    {
        private readonly IHeatingUnitService _heatingUnitService;

        public TPHub(IHeatingUnitService heatingUnitService)
        {
            _heatingUnitService = heatingUnitService;
        }
        public async Task ActivateHeatingUnit(string macAddress, bool shouldActivate)
        {
             _heatingUnitService.ToggleOnOff(macAddress, shouldActivate);
        }

        public async void UpdateCurrentTempurature(string macAddress)
        {
            var heatingUnit = _heatingUnitService.GetByMacAddress(macAddress);
            if (heatingUnit == null) return;
            await Clients.All.SendAsync("UpdateHeatingUnit", heatingUnit);
        }

        public async Task UpdateTargetTempurature(string macAddress, float target)
        {
            var heatingUnit = _heatingUnitService.GetByMacAddress(macAddress);
            if (heatingUnit == null) return;
            heatingUnit.TargetTemperature = target;
            _heatingUnitService.Put(heatingUnit);

        }
    }
}
