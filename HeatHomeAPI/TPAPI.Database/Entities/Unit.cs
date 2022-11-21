using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPAPI.Domain.Entities
{
    public abstract class HardwareUnit
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string MacAddress { get; set; }

        public virtual HardwareType HardwareType { get; set; }

    }
}
