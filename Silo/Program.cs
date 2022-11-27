// See https://aka.ms/new-console-template for more information
using System.Net;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Orleans.Configuration;


var host = await StartSiloAsync();
Console.WriteLine("\n\n Press Enter to terminate...\n\n");
Console.ReadLine();

await host.StopAsync();

return 0;


