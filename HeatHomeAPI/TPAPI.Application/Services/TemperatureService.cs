using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TPAPI.Domain.Entities;
using TPAPI.Domain.Interfaces.Repositories;
using TPAPI.Domain.Interfaces.Services;

namespace TPAPI.Application.Services
{
    public class TemperatureService : ITemperatureService
    {
        private IUnitOfWork _unitOfWork;

        public TemperatureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Delete(Temperature temperature)
        {
            if (temperature != null)
            {
                _unitOfWork.Temperature.Delete(temperature);
            }
        }

        public Temperature? Get(Guid id)
        {
            return _unitOfWork.Temperature.Get(id);
        }

        public IReadOnlyList<Temperature> GetAll()
        {
            return _unitOfWork.Temperature.GetAll();
        }

        public void Post(Temperature temperature)
        {
            _unitOfWork.Temperature.Add(temperature);
            _unitOfWork.SaveChanges();
        }

        public void Put(Temperature temperature)
        {
            _unitOfWork.Temperature.Update(temperature);
            _unitOfWork.SaveChanges();
        }

        public void Post(string macAddress, Temperature temperature)
        {
            var heatingUnit = _unitOfWork.HeatingUnit.GetByMacAddress(macAddress);
            if (heatingUnit == null)
                return;
            temperature.HeatingUnitId = heatingUnit.Id;
            _unitOfWork.Temperature.Add(temperature);
            _unitOfWork.SaveChanges();
        }
    }
}
