using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.WebSockets;
using the_key___technology_GmbH.BusinessLogic;

namespace the_key___technology_GmbH.Controllers
{
    public class WebsocketController : Controller
    {
        [Route("/ws")]
        public async Task OpenWebsocket()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await WordCount_Continus(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }

        }

        private static async Task<string> GetWordCount(string path)
        {
            string post = await ContentReader.GetContent(path);
            var theMap = WordCount.GetWordCount(post);
            var res = JsonConvert.SerializeObject(theMap);

            return res;
        }

        private static async Task WordCount_Continus(WebSocket webSocket)
        {
            bool timerShouldStop = false;
            var inBuffer = new byte[1024 * 4];

            WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(inBuffer), CancellationToken.None);

            var aTimer = new System.Timers.Timer();
            aTimer.Interval = 3500;
            aTimer.Elapsed += async (sender, e) =>
            {
                aTimer.Enabled = !timerShouldStop;
                if (timerShouldStop)
                {
                    return;
                }

                string path = System.Text.Encoding.Default.GetString(inBuffer, 0, receiveResult.Count);
                string res = await GetWordCount(path);
                var outBuffer = System.Text.Encoding.UTF8.GetBytes(res);

                try
                {
                    await webSocket.SendAsync(
                        new ArraySegment<byte>(outBuffer, 0, outBuffer.Length),
                        WebSocketMessageType.Text,
                        true,
                        CancellationToken.None);
                }
                catch { }   // Probably socket was closed while downloading and processing the blog data

            };
            aTimer.AutoReset = true;
            aTimer.Enabled = true;



            do      // Just check if the connection should be closed.
            {
                var buffer = new byte[1024 * 4];
                receiveResult = await webSocket.ReceiveAsync(
                   new ArraySegment<byte>(buffer), CancellationToken.None);
                timerShouldStop = receiveResult.CloseStatus.HasValue;
            }
            while (!timerShouldStop);

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }
    }
}
