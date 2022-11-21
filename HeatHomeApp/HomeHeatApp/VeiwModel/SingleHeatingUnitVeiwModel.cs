using CommunityToolkit.Mvvm.ComponentModel;
using HeatHomeApp.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using HeatHomeApp.Services;


namespace HeatHomeApp.VeiwModel
{
    [QueryProperty(nameof(HeatingUnit),nameof(HeatingUnit))]
    public partial class SingleHeatingUnitVeiwModel : BaseVeiwModel
    {
        private readonly SignalRService signalRService;
        [ObservableProperty]
        private HeatingUnit heatingUnit;
        [ObservableProperty]
        private float? currentTemp = 0;


        public SingleHeatingUnitVeiwModel()
        {
            #region 

            #endregion
#if ANDROID
            signalRService = MauiApplication.Current.Services.GetService<SignalRService>();
#endif
            SignalRService.UpdateTemperatureEvent += SignalRService_UpdateTemperatureEvent;
            CurrentTemp = heatingUnit?.CurrentTemperature.Value;
        }
        [RelayCommand]
        private async void CheckDistanceToHeatingUnit()
        {
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.High);
            Location PhoneLocation = await Geolocation.Default.GetLocationAsync(request);

            if (PhoneLocation != null && PhoneLocation.IsFromMockProvider)
            {
                // location is from a mock provider
            }
            
            Location heatingUnitLocation = new Location(heatingUnit.Coordinate.Latitude, heatingUnit.Coordinate.Longitude);

            double distanceInKm = Location.CalculateDistance(heatingUnitLocation, PhoneLocation, DistanceUnits.Kilometers);
        }

        private void SignalRService_UpdateTemperatureEvent(object sender, HeatingUnit e)
        {
            UpdateTemperature(e);
        }

        private void UpdateTemperature(HeatingUnit obj)
        {
            CurrentTemp = obj.CurrentTemperature.Value;
        }

        public async void ToggleIsOn()
        {
           await SignalRService.Start();
           await SignalRService.Hub.InvokeAsync("ActivateHeatingUnit", heatingUnit.macAddress, heatingUnit.IsOn, CancellationToken.None);
        }
        

        [RelayCommand]
        public async void UpdateTargetTemperature()
        {
            await SignalRService.Start();
            await SignalRService.Hub.InvokeAsync("UpdateTargetTempurature", heatingUnit.macAddress, heatingUnit.TargetTemperature, CancellationToken.None);
        }
    }
}
