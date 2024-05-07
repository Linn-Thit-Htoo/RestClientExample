using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RestClientExample.BlazorWasm;
using RestSharp;
using MudBlazor.Services;
using RestClientExample.BlazorWasm.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped(n =>
{
    return new RestClient("https://localhost:7248");
});

builder.Services.AddScoped<InjectService>();

await builder.Build().RunAsync();
