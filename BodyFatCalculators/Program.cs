using BodyFatCalculators.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BodyFatCalculators
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton<DataService>();
            builder.Services.AddSingleton<LanguageService>();
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            var host = builder.Build();

            await host.Services.GetRequiredService<LanguageService>().Init().ConfigureAwait(true);

            await host.RunAsync().ConfigureAwait(true);
        }
    }
}
