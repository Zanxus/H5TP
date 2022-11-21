using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeatHomeApp.Models
{
    public class Temperature : ObservableObject
    {
        public Temperature()
        {

        }
        public Temperature(float value, DateTimeOffset timestamp)
        {
            Value = value;
            Timestamp = timestamp;
        }

        //public Guid Id { get; set; }
        public float Value { get; set; }
        //public Guid HeatingUnitId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}