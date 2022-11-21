using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.WebSockets;
using TPAPI.Application.Services;
using TPAPI.Domain.Entities;
using TPAPI.Domain.Interfaces.Services;

namespace TPAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeatingUnitController : ControllerBase
    {

        //private readonly ILogger<HeatingUnitController> _logger;
        private readonly IHeatingUnitService _heatingUnitService;

        public HeatingUnitController(IHeatingUnitService heatingUnitService)
        {
            //_logger = logger;
            _heatingUnitService = heatingUnitService;
            

        }
        [HttpGet("All")]
        public IActionResult GetAll()
        {
            return Ok(_heatingUnitService.GetAll());
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HeatingUnit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [HttpGet]
        public IActionResult Get(Guid guid)
        {
            var unit = _heatingUnitService.Get(guid);
            
            return unit == null ? NotFound() : Ok(unit);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HeatingUnit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [HttpGet("GetByMacAddress")]
        public IActionResult GetByMacAddress(string macAddress)
        {
            var unit = _heatingUnitService.GetByMacAddress(macAddress);

            return unit == null ? NotFound() : Ok(unit);
        }
        [HttpPost]
        public IActionResult Post([FromBody] HeatingUnit heatingUnit)
        {
            if (ModelState.IsValid)
            {
                _heatingUnitService.Post(heatingUnit);
                return CreatedAtAction(nameof(Get), new { id = heatingUnit.Id }, heatingUnit);

            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult Put([FromBody] HeatingUnit heatingUnit)
        {
            if (ModelState.IsValid)
            {
                _heatingUnitService.Put(heatingUnit);
                return Ok(heatingUnit);

            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var unit = _heatingUnitService.Get(guid);
            if (unit != null)
            {
                _heatingUnitService.Delete(unit);
            }

            return unit == null ? NotFound() : Ok(unit);
        }


        [HttpGet("/Activate")]
        public IActionResult ToggleOnOff(string macAddress = "Test", bool shouldActivate = true)
        {
            return _heatingUnitService.ToggleOnOff(macAddress, shouldActivate) == HttpStatusCode.OK ? Ok() : NotFound($"Could not find a connection HeatingUnit with MacAddress : {macAddress}");
        }
    }
}