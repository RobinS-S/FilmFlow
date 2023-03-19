using FilmFlow.Client.Auth;
using FilmFlow.Client.Auth.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

namespace FilmFlow.Client
{
    public class Program
    {
        public const string LanguageKeyName = "i18nextLng";

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddHttpClient<IAuthorizedHttpClient, AuthorizedHttpClient>("FilmFlow.AuthorizedClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddHttpClient<IAnonymousHttpClient, AnonymousHttpClient>("FilmFlow.Client", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            
            builder.Services.AddApiAuthorization();
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            var host = builder.Build();

            var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();
            var language = await jsRuntime.InvokeAsync<string?>("localStorage.getItem", LanguageKeyName);
            if (!string.IsNullOrEmpty(language))
            {
                var culture = new CultureInfo(language);
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }

            await host.RunAsync();
        }
    }
}