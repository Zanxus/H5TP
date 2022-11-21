using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using HeatHomeApp.Models;

namespace HeatHomeApp.Services
{
    public class HeatingUnitService
    {
        HttpClient HttpClient;

        public HeatingUnitService()
        {
            HttpClient = new();
            HttpClient.BaseAddress = new("http://192.168.1.100:45455/");
        }
        List<HeatingUnit> heatingUnits = new();
        public async Task<List<HeatingUnit>> GetHeatingUnits()
        {
            if (heatingUnits.Count > 0)
            {
                return heatingUnits;
            }
            var url = "HeatingUnit/All";
            var response = await HttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                heatingUnits = await response.Content.ReadFromJsonAsync<List<HeatingUnit>>();
            }
            return heatingUnits;
        }
    }
}
