using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPAPI.Domain.Interfaces.Repositories;

namespace TPAPI.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HomeHeatDbContext _dbContext;

        public UnitOfWork(HomeHeatDbContext dbContext , IHeatingUnitRepository heatingUnit, ITemperatureRepository temperature)
        {
            _dbContext = dbContext;
            
            HeatingUnit = heatingUnit;
            Temperature = temperature;
        }

        public IHeatingUnitRepository HeatingUnit { get; set; }
        public ITemperatureRepository Temperature { get; set; }
        

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
