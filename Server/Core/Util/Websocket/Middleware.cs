using System.Collections.Concurrent;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;

namespace Core.Util.Websocket;

public class WebSocketMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ConcurrentDictionary<string, WebSocket> _sockets;

    public WebSocketMiddleware(RequestDelegate next)
    {
        _next = next;
        _sockets = new ConcurrentDictionary<string, WebSocket>();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            string connectionId = Guid.NewGuid().ToString();
            _sockets.TryAdd(connectionId, webSocket);
            await HandleWebSocketAsync(connectionId, webSocket);
            return;
        }

        await _next(context);
    }

    private async Task HandleWebSocketAsync(string connectionId, WebSocket webSocket)
    {
        byte[] buffer = new byte[1024 * 4];

        while (webSocket.State == WebSocketState.Open)
        {
            WebSocketReceiveResult result =
                await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                _sockets.TryRemove(connectionId, out webSocket);
                await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription,
                    CancellationToken.None);
                return;
            }

            foreach (WebSocket socket in _sockets.Values)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType,
                        result.EndOfMessage, CancellationToken.None);
                }
            }
        }
    }
}