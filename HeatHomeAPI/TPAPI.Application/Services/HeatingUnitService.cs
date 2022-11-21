using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using TPAPI.Domain.Entities;
using TPAPI.Domain.Interfaces.Repositories;
using TPAPI.Domain.Interfaces.Services;

namespace TPAPI.Application.Services
{
    public class HeatingUnitService : IHeatingUnitService
    {
        public readonly IUnitOfWork _unitOfWork;

        public HeatingUnitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Delete(HeatingUnit unit)
        {
            if (unit != null)
            {
                _unitOfWork.HeatingUnit.Delete(unit);
            }
        }

        public HeatingUnit? Get(Guid id)
        {
            return _unitOfWork.HeatingUnit.Get(id);
        }

        public IReadOnlyList<HeatingUnit> GetAll()
        {
            return _unitOfWork.HeatingUnit.GetAll();
        }

        public HeatingUnit? GetByMacAddress(string macAddress)
        {
            return _unitOfWork.HeatingUnit.GetByMacAddress(macAddress);
        }

        public void Post(HeatingUnit unit)
        {
            _unitOfWork.HeatingUnit.Add(unit);
            _unitOfWork.SaveChanges();
        }

        public void Put(HeatingUnit unit)
        {
            _unitOfWork.HeatingUnit.Update(unit);
            _unitOfWork.SaveChanges();
        }

        public HttpStatusCode ToggleOnOff(string macAddress, bool shouldActivate)
        {
            WebSocket webSocket;
            if (WebSocketService.TryGetWebSocket(macAddress, out webSocket) == false)
            {

                return HttpStatusCode.NotFound;
            }
            if (shouldActivate)
            {
                var bytes = new byte[] { 1 };
                WebSocketService.SendBinary(webSocket, bytes);
            }
            else
            {
                var bytes = new byte[] { 0 };
                WebSocketService.SendBinary(webSocket, bytes);
            }

            return HttpStatusCode.OK;
        }
    }
}
