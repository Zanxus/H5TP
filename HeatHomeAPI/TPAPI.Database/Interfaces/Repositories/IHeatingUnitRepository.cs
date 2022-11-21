using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPAPI.Domain.Entities;

namespace TPAPI.Domain.Interfaces.Repositories
{
    public interface IHeatingUnitRepository : IRepositoryBase<HeatingUnit>
    {
        public HeatingUnit? GetByMacAddress(string macAddress);
    }
}
