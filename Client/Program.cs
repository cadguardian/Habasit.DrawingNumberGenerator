using Client;
using Client.Data;
using Client.Services;
using DNG.Library.Data;
using DNG.Library.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IDrawingNumber, DrawingNumber>();
builder.Services.AddScoped<IDrawingRequest, DrawingRequest>();
builder.Services.AddScoped<IDrawingRequestProcessor, DrawingRequestProcessor>();
builder.Services.AddScoped<IPartNumberService, PartNumberService>();

await builder.Build().RunAsync();