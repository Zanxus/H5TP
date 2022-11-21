using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPAPI.Domain.Entities;
using TPAPI.Domain.Interfaces.Repositories;

namespace TPAPI.Infrastructure.Repositories
{
    public class HeatingUnitRepository : BaseRepository<HeatingUnit>, IHeatingUnitRepository
    {
        public HeatingUnitRepository(HomeHeatDbContext dbContext) : base(dbContext)
        {

        }

        public HeatingUnit? GetByMacAddress(string macAddress)
        {
            var test = _dbSet.FirstOrDefault(h => h.MacAddress == macAddress);
            return test;
        }
    }
}
