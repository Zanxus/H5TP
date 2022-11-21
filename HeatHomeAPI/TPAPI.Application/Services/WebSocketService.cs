using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.WebSockets;
using System.Text;
using TPAPI.Domain.Entities;
using TPAPI.Domain.Interfaces.Services;
using TPAPI.Application.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace TPAPI.Application.Services
{
    public class WebSocketService
    {
        private static Dictionary<string, WebSocket> _websocketLookup = new Dictionary<string, WebSocket>();
        private object locker = new();
        private IHeatingUnitService _heatingUnitService;
        private readonly ITemperatureService _temperatureService;
        private readonly IHubContext<TPHub> _hubContext;
        private readonly TPHub _hub;

        public WebSocketService(ITemperatureService temperatureService, IHeatingUnitService heatingUnitService, IHubContext<TPHub> hubContext)
        {
            _temperatureService = temperatureService;
            _heatingUnitService = heatingUnitService;
            _hubContext = hubContext;
        }


        /// <summary>
        /// Tries to safely add a websocket and if unsucessful it removes the old reference and adds the new connection to ensure the most updated list.
        /// </summary>
        /// <param name="macAdress"></param>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        public bool TryAddWebsocketWithMacAdress(string macAdress, WebSocket webSocket)
        {
            lock (locker)
            {
                if (_websocketLookup.TryAdd(macAdress, webSocket) == false)
                {
                    _websocketLookup.Remove(macAdress);
                    return _websocketLookup.TryAdd(macAdress, webSocket);
                }
                return true;
            }
        }
        /// <summary>
        /// Tries to safely remove Websocket by MacAddress.
        /// </summary>
        /// <param name="macAdress"></param>
        /// <returns></returns>
        public bool RemoveWebsocketByMacAddress(string macAdress)
        {
            lock (locker)
            {
                return _websocketLookup.Remove(macAdress);
            }
        }
        /// <summary>
        /// Gets a list of all connected MacAdresses.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllMacAdresses()
        {
            var MacAdresses = new List<string>();
            foreach (var item in _websocketLookup)
            {
                if (item.Value.State == WebSocketState.Open)
                {
                    MacAdresses.Add(item.Key);
                }
            }
            return MacAdresses;
        }
        /// <summary>
        /// Gets a single Websocket by MacAddress.
        /// </summary>
        /// <param name="macAdress"></param>
        /// <returns></returns>
        public static bool TryGetWebSocket(string macAdress, out WebSocket webSocket)
        {
            return _websocketLookup.TryGetValue(macAdress, out webSocket);
        }
        /// <summary>
        /// Handles the loop that keeps websockets alive, aswell as reads incoming messages from the websocket.
        /// </summary>
        /// <param name="webSocket"></param>
        public async void HandleWebsocketLoop(WebSocket webSocket)
        {
            string? macAddress = null;
            ArraySegment<byte> buffer = WebSocket.CreateServerBuffer(1024);
            while (webSocket.State == WebSocketState.Open)
            {
                try
                {
                    var websocketResult = webSocket.ReceiveAsync(buffer, CancellationToken.None).Result;
                    var message = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine(message);
                    if (message.StartsWith('C'))
                    {
                        macAddress = message.Split(": ")[1].Trim('\0');
                        TryAddWebsocketWithMacAdress(macAddress, webSocket);
                    }
                    else
                    {
                        float.TryParse(message, NumberStyles.Number, CultureInfo.InvariantCulture, out float temperature);
                        if (macAddress != null)
                        {
                            _temperatureService.Post(macAddress, new Temperature(temperature, DateTimeOffset.UtcNow));
                            var heatingUnit = _heatingUnitService.GetByMacAddress(macAddress);
                            if (heatingUnit != null)
                            _hubContext.Clients.All.SendAsync("UpdateHeatingUnit", heatingUnit);
                        }
                    }
                    buffer = WebSocket.CreateServerBuffer(1024);
                }

                catch (Exception)
                {
                    for (int i = _websocketLookup.Count - 1; i > 0; i--)
                    {
                        var websocketPair = _websocketLookup.ElementAt(i);
                        if (websocketPair.Value.State != WebSocketState.Open)
                        {
                            _websocketLookup.Remove(websocketPair.Key);
                        }
                    }
                }
            }


        }
        /// <summary>
        /// Send a string as binary data by Websocket Connection.
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="message"></param>
        public static async void SendBinary(WebSocket webSocket, string message)
        {

            Console.WriteLine();
            var buffer = WebSocket.CreateServerBuffer(1024);

            buffer = Encoding.UTF8.GetBytes(message);
            Console.WriteLine($"Sending: {message} as binary");

            await webSocket.SendAsync(buffer, WebSocketMessageType.Binary, true, CancellationToken.None);
            buffer = WebSocket.CreateServerBuffer(1024);
        }
        /// <summary>
        /// Sends an array of bytes by Websocket Connection.
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="bytes"></param>
        public static async void SendBinary(WebSocket webSocket, byte[] bytes)
        {

            Console.WriteLine();
            var buffer = WebSocket.CreateServerBuffer(1024);

            buffer = bytes;

            Console.WriteLine($"Sending: {buffer} as binary");

            await webSocket.SendAsync(buffer, WebSocketMessageType.Binary, true, CancellationToken.None);
            buffer = WebSocket.CreateServerBuffer(1024);
        }
    }
}
