using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPAPI.Domain.Entities;

namespace TPAPI.Domain.Interfaces.Services
{
    public interface ITemperatureService
    {
        IReadOnlyList<Temperature> GetAll();
        Temperature? Get(Guid id);
        void Post(Temperature temperature);
        void Put(Temperature temperature);
        void Delete(Temperature temperature);
        void Post(string macAddress, Temperature temperature);
    }
}
