using Microsoft.Extensions.DependencyInjection; // This should already be there
using Microsoft.Extensions.Http; // Add this line
using SBP_DWG_PDM_Winforms; // Replace with your actual namespace
using DNG.Library.Services;
using DNG.Library.Services.Base;
using DNG.Library.Models.Base; // Assuming your service is in this namespace

namespace SBP_DWG_PDM_Winforms
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<Form1>();
                Application.Run(form1);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDrawingFileService, DrawingFileService>();
            services.AddScoped<IDrawingNumberDecipherService, DrawingNumberDecipherService>();
            services.AddHttpClient(); // Add HttpClient if you're using it in your service.
            services.AddScoped<Form1>(); // Register your form
        }
    }
}