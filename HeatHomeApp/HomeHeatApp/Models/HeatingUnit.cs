
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace HeatHomeApp.Models
{
    public partial class HeatingUnit : HardwareUnit
    {
        
        public Temperature? CurrentTemperature => Temperatures?.LastOrDefault();
        [ObservableProperty]
        private float targetTemperature;
        [ObservableProperty]
        private GeoCoordinate? coordinate;
        [ObservableProperty]
        private bool? isOn;

        [NotifyPropertyChangedFor(nameof(CurrentTemperature))]
        [ObservableProperty]
        private ObservableCollection<Temperature> temperatures;
    }

}
