using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TPAPI.Domain.Entities;

namespace TPAPI.Domain.Interfaces.Services
{
    public interface IHeatingUnitService
    {
        HeatingUnit? Get(Guid id);
        HeatingUnit? GetByMacAddress(string macAddress);
        IReadOnlyList<HeatingUnit> GetAll();
        void Post(HeatingUnit unit);
        void Put(HeatingUnit unit);
        void Delete(HeatingUnit unit);

        HttpStatusCode ToggleOnOff(string macAdress,bool shouldActivate);
    }
}
