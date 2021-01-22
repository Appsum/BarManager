using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;


const string hubUrl = "https://localhost:5011/tables";

// Give the SignalR server hub time to startup
Console.WriteLine("Will connect to Hub in 5 seconds ...");
Thread.Sleep(TimeSpan.FromSeconds(5));

// Start the connection
HubConnection hubConnection = await SetupSignalRHubAsync();

// Setup a listener on the method
hubConnection.On<Dictionary<string, int>>("Deliver", message => PrintDelivery(message));

Console.WriteLine("Connected to Hub");
Console.WriteLine("Press ESC to stop");

// Exit program is ESC is clicked
while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }


static async Task<HubConnection> SetupSignalRHubAsync()
{
    
    HubConnection hubConnection = new HubConnectionBuilder()
                                  .WithUrl(hubUrl)
                                  .Build();

    await hubConnection.StartAsync();
    return hubConnection;
}

static void PrintDelivery(Dictionary<string, int> dictionary)
{
    const int dashRepeatCount = 40;
    Console.WriteLine(new string('-', dashRepeatCount));
    Console.WriteLine("Delivering ordered drinks:");
    foreach ((string drinkName, int amount) in dictionary)
    {
        Console.WriteLine($"{drinkName} ({amount})");
    }

    Console.WriteLine(new string('-', dashRepeatCount));
}
