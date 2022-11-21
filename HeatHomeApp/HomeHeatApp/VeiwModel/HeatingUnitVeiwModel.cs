using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeatHomeApp.Services;
using HeatHomeApp.Veiw;
using System.Collections.ObjectModel;
using HeatHomeApp.Models;

namespace HeatHomeApp.VeiwModel
{
    public partial class HeatingUnitVeiwModel : BaseVeiwModel
    {
        private readonly HeatingUnitService heatingUnitService;
        private readonly SignalRService signalRService;

        public ObservableCollection<HeatingUnit> HeatingUnits { get; set; } = new();
        [ObservableProperty]
        bool isRefreshing = false;
        public HeatingUnitVeiwModel(HeatingUnitService heatingUnitService)
        {
            Title = "Heating Units";
            this.heatingUnitService = heatingUnitService;
#if ANDROID
            signalRService = MauiApplication.Current.Services.GetService<SignalRService>();
#endif
            GetHeatingUnitsAsync();
            SignalRService.UpdateTemperatureEvent += SignalRService_UpdateTemperatureEvent;
        }

        private void SignalRService_UpdateTemperatureEvent(object sender, HeatingUnit e)
        {
            UpdateTemperature(e);
        }

        public void UpdateTemperature(HeatingUnit heatingUnit)
        {
            HeatingUnits[HeatingUnits.IndexOf(HeatingUnits.FirstOrDefault(h => h.id == heatingUnit.Id))] = heatingUnit;
        }
        [RelayCommand]
        async Task GetHeatingUnitsAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                var heatingunits = await heatingUnitService.GetHeatingUnits();
                foreach (var unit in heatingunits)
                {
                    HeatingUnits.Add(unit);
                }
                
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Unable to get heating units:{ex.Message}", "OK");
            }
            finally
            {
                IsRefreshing = false;
                IsBusy = false;
            }

        }
        [RelayCommand]
        async Task GoToHeatingUnit(HeatingUnit heatingUnit)
        {
            await Shell.Current.GoToAsync(nameof(HeatingUnitPage),true,new Dictionary<string, object>
            {
                ["HeatingUnit"] = heatingUnit
            });
            
        }


    }
}
