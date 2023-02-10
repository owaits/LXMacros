using LXMacros;
using LXProtocols.AvolitesWebAPI.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAvolitesTitan();

var host = builder.Build();

var titan = host.Services.GetService<IAvolitesTitan>();
if(titan != null)
{
    titan.Connect("localhost", 4431);
}


await host.RunAsync();
