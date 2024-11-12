using Client;
using DNG.Library.Data;
using DNG.Library.Models;
using DNG.Library.Services.Base;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IDrawingNumber, DrawingNumberService>();
builder.Services.AddScoped<IDrawingRequest, DrawingRequest>();
builder.Services.AddScoped<IDrawingRequestProcessor, DrawingRequestProcessor>();
builder.Services.AddScoped<IPartNumberService, PartNumberService>();
builder.Services.AddSingleton<IBeltSeriesService, BeltSeriesThumnbnailService>();
builder.Services.AddSingleton<ICADLibraryService, CADLibraryService>();
builder.Services.AddScoped<IClipboardService, ClipboardService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ITitleBlockService, TitleBlockService>();
builder.Services.AddScoped<IImageGalleryService, ImageGalleryService>();
builder.Services.AddScoped<IRequiredBeltWidthService, RequiredBeltWidthService>();
builder.Services.AddScoped<IDrawingFileService, DrawingFileService>();

// reference drawing service and script needed in c#

await builder.Build().RunAsync();