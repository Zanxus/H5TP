using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPAPI.Domain.Interfaces.Services;

namespace TPAPI.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IHeatingUnitRepository HeatingUnit { get; set; }
        public ITemperatureRepository Temperature { get; set; }
        Task SaveChangesAsync();
        void SaveChanges();
    }
}
