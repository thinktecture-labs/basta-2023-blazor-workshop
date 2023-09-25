using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WorkshopClient;
using WorkshopClient.Features.Authentication.Handler;
using WorkshopClient.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CustomAuthorizationHeaderHandler>();
builder.Services.AddHttpClient("WebApi", client => client.BaseAddress = new Uri("https://localhost:7218/api/"))
    .AddHttpMessageHandler<CustomAuthorizationHeaderHandler>();

builder.Services.AddScoped(sp =>
{   
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("WebApi");
});

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Oidc", options.ProviderOptions);
});

builder.Services.AddScoped<RealTimeService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
