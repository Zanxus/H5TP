using System.ComponentModel.DataAnnotations;

namespace TPAPI.Domain.Entities
{
    public class Temperature
    {
        public Temperature(float value, DateTimeOffset timestamp)
        {
            Value = value;
            Timestamp = timestamp;
        }

        [Key]
        public Guid Id { get; set; }
        public float Value { get; set; }
        public Guid HeatingUnitId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}