using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Security.Principal;
using System.Text;
using TPAPI.Application.Services;
using TPAPI.Domain.Entities;
using TPAPI.Presentation.Controllers;

namespace TPAPI.Controllers
{
    public class WebsocketController : Controller
    {
        private readonly WebSocketService _webSocketService;

        public WebsocketController(WebSocketService webSocketService)
        {
            _webSocketService = webSocketService;
        }



        /// <summary>
        /// Entrypoint for NodeMcu Websockets 
        /// </summary>
        [HttpGet("/ws")]
        public async void WebsocketEntryPoint()
        {
            
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                return;
            }
            WebSocket webSocket = HttpContext.WebSockets.AcceptWebSocketAsync().Result;
            if (webSocket == null)
            {
                return;
            }
            
            _webSocketService.HandleWebsocketLoop(webSocket);

        }

        /// <summary>
        /// Returns the MacAdresses of connected websockets.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Connected")]
        public IActionResult ConnectedMacAddresses()
        {
            return Ok(WebSocketService.GetAllMacAdresses());
        }
    }
}
