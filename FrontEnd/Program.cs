using GrainInterfaces.State;
using Grains.DocumentData;
using Grains.State;
using Microsoft.Extensions.FileProviders;
using Orleans.Configuration;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var path = Path.GetFullPath("./files");
var documentStore = new JsonDocumentStore(new PhysicalFileWriter(new PhysicalFileProvider(path)));

var siloHost = await StartSiloAsync(documentStore);
var client = siloHost.Services.GetRequiredService<IClusterClient>();


var result = client.GetGrain<IPostGrain>(Guid.NewGuid()).GetContent().Result;
// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IClusterClient>(client);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();

static async Task<IHost> StartSiloAsync(IDocumentStore storage)
{    
    var builder = new HostBuilder();    
    builder.UseOrleans(c =>
        {
            var cluster = c.UseLocalhostClustering();
            cluster.Services.AddSingleton(storage);
            cluster.Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "HelloWorldApp";
                })
                .Configure<EndpointOptions>(
                    options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureLogging(logging => logging.AddConsole());
        });

    var host = builder.Build();
    await host.StartAsync();

    return host;
}
