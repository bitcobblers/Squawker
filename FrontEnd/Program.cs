using Grains.DocumentData;
using Microsoft.Extensions.FileProviders;
using Orleans.Configuration;
using System.Net;

var path = Path.GetFullPath("../Content");
var storage = new PhysicalFileWriter(new PhysicalFileProvider(path));
await Host.CreateDefaultBuilder(args)
    .UseOrleans(c =>
    {
        c.UseDashboard(x => x.HostSelf = true);

        var cluster = c.UseLocalhostClustering();
        cluster.AddFileGrainStorage("Document", opt =>
        {
            opt.RootDirectory = storage;
        });
        cluster.AddMemoryGrainStorage("Relational");
        cluster.Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "dev";
            options.ServiceId = "HelloWorldApp";
        });
        cluster.Configure<EndpointOptions>(options =>
        {
            options.AdvertisedIPAddress = IPAddress.Loopback;
        });

        cluster.ConfigureLogging(logging => logging.AddConsole());
    })
    .ConfigureWebHostDefaults(builder =>
    {
        builder.ConfigureServices(services => services.AddControllersWithViews());
        builder.Configure((cxt, app) =>
        {
            // Configure the HTTP request pipeline.
            if (!cxt.HostingEnvironment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.Map("/dashboard", x => x.UseOrleansDashboard());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "api/{controller}/{action=Index}/{id?}");

                endpoints.MapFallbackToFile("index.html");
            });
        });
    })
    .RunConsoleAsync();