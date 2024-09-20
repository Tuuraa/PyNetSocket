using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    static async Task Main()
    {
        using (ClientWebSocket client = new ClientWebSocket())
        {
            try
            {
                Uri serverUri = new Uri("ws://localhost:8000");
                await client.ConnectAsync(serverUri, CancellationToken.None);

                var buffer = new byte[1024];

                _ = Task.Run(async () =>
                {
                    while (client.State == WebSocketState.Open)
                    {
                        var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        string response = Encoding.UTF8.GetString(buffer, 0, result.Count);

                        Console.WriteLine(response);
                    }
                });

                Console.WriteLine("Нажмите любую клавишу для выхода...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
