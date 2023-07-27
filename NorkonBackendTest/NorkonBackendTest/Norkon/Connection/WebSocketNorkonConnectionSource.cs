using System.Net.WebSockets;

namespace NorkonBackendTest.Norkon.Connection
{
    public class WebSocketNorkonConnectionSource : INorkonConnectionSource
    {
        private readonly IConfiguration _config;

        public WebSocketNorkonConnectionSource(IConfiguration config)
        {
            _config = config;
        }

        public async IAsyncEnumerable<int> GetConnectionCount([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using var webSocket = new ClientWebSocket();
            var connection = _config.GetConnectionString("NorkonServer") ?? string.Empty;
            await webSocket.ConnectAsync(new Uri(connection), cancellationToken);

            string json = @"{""seq"":1000,""d"":{""_t"":""connected""}}";
            var sendBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(json));
            await webSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, cancellationToken);

            byte[] buffer = new byte[256];
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(buffer, cancellationToken);
                if (result.EndOfMessage)
                {
                    var text = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    if (int.TryParse(text, out int count))
                        yield return count;
                }
            }
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
        }
    }
}
