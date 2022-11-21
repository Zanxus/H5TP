using HeatHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_Test.Models
{
    public class ChartTemperature
    {
        public ChartTemperature(Temperature temperature)
        {
            Temperature = temperature.Value;
            Time = temperature.Timestamp.DateTime;
        }
        public ChartTemperature(double temperature, DateTime time)
        {
            Temperature = temperature;
            Time = time;
        }

        public double Temperature { get; set; }
        public DateTime Time { get; set; }
    }
}
