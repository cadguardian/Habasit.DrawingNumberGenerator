using Client.Data;
using Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Register DbContext with SQLite
builder.Services.AddDbContext<PartNumberDbContext>(options =>
    options.UseSqlite("Data Source=partnumbers.db"));

builder.Services.AddScoped<IPartNumberService, PartNumberService>();

await builder.Build().RunAsync();