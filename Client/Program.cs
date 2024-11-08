using Client;
using Client.Services;
using DNG.Library.Data;
using DNG.Library.Models;
using DNG.Library.Services.Base;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IDrawingNumber, DrawingNumber>();
builder.Services.AddScoped<IDrawingRequest, DrawingRequest>();
builder.Services.AddScoped<IDrawingRequestProcessor, DrawingRequestProcessor>();
builder.Services.AddScoped<IPartNumberService, PartNumberService>();
builder.Services.AddSingleton<IBeltSeriesService, BeltSeriesService>();
builder.Services.AddSingleton<ICADLibraryService, CADLibraryService>();
builder.Services.AddScoped<IClipboardService, ClipboardService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ITitleBlockService, TitleBlockService>();
builder.Services.AddScoped<IImageGalleryService, ImageGalleryService>();

// reference drawing service and script needed in c#

await builder.Build().RunAsync();