using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Core.Util.Websocket;

public class WebSocketContainer
{
    private readonly ConcurrentDictionary<string, WebSocket> _sockets;

    public WebSocketContainer()
    {
        _sockets = new ConcurrentDictionary<string, WebSocket>();
    }

    public bool AddSocket(string connectionId, WebSocket webSocket)
    {
        return _sockets.TryAdd(connectionId, webSocket);
    }

    public async Task BroadcastMessageAsync(byte[] buffer, WebSocketMessageType messageType, CancellationToken cancellationToken)
    {
        foreach (WebSocket socket in _sockets.Values)
        {
            if (socket.State == WebSocketState.Open)
            {
                await socket.SendAsync(new ArraySegment<byte>(buffer), messageType, true, cancellationToken);
            }
        }
    }

    public async Task RemoveSocketAsync(string connectionId, WebSocketCloseStatus closeStatus, string closeStatusDescription, CancellationToken cancellationToken)
    {
        if (_sockets.TryRemove(connectionId, out WebSocket webSocket))
        {
            await webSocket.CloseAsync(closeStatus, closeStatusDescription, cancellationToken);
        }
    }
}