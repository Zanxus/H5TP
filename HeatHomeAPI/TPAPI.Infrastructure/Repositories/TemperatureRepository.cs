using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPAPI.Domain.Entities;
using TPAPI.Domain.Interfaces.Repositories;

namespace TPAPI.Infrastructure.Repositories
{
    public class TemperatureRepository : BaseRepository<Temperature>, ITemperatureRepository
    {
        public TemperatureRepository(HomeHeatDbContext dbContext) : base(dbContext)
        {

        }
    }
}
