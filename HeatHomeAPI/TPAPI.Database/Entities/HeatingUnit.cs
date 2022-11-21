
namespace TPAPI.Domain.Entities
{
    public partial class HeatingUnit : HardwareUnit
    {
        
        public virtual Temperature? CurrentTemperature => Temperatures?.LastOrDefault();
        public float TargetTemperature { get; set; } = 0;
        public virtual GeoCoordinate? Coordinate { get; set; } = null;
        public bool? IsOn { get; set; } = false;


        public virtual ICollection<Temperature> Temperatures { get; set; }
    }

}
