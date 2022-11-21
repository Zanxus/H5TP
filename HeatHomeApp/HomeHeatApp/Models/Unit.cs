using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatHomeApp.Models
{
    public partial class HardwareUnit : ObservableObject
    {
        [ObservableProperty]
        public Guid id;
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string macAddress;

        public virtual HardwareType HardwareType { get; set; }

    }
}
