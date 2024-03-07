using MenuFlix.Web.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MudBlazor.Services;
using MudBlazor;
using MenuFlix.Web.Client.Layout;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("WeatherAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
       .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
       .CreateClient("WeatherAPI"));

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
}).AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();
builder.Services.AddMudBlazorDialog()
    .AddMudServices(mudConfig =>
    {
        mudConfig.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
        mudConfig.SnackbarConfiguration.PreventDuplicates = false;
        mudConfig.SnackbarConfiguration.NewestOnTop = true;
        mudConfig.SnackbarConfiguration.ShowCloseIcon = true;
        mudConfig.SnackbarConfiguration.VisibleStateDuration = 4000;
        mudConfig.SnackbarConfiguration.HideTransitionDuration = 500;
        mudConfig.SnackbarConfiguration.ShowTransitionDuration = 500;
        mudConfig.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
    });
builder.Services.AddMudServices();
await builder.Build().RunAsync();
