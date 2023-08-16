using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Globalization;
using ToDo.Client;
using ToDo.Client.Shared.Services;
using ToDo.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Api",
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/"));

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("Api"));
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<SessionStorageService>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<ToDoAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<ToDoAuthenticationStateProvider>());

builder.Services.AddMudServices();


var app = builder.Build();

using (var sp = app.Services.CreateScope())
{
    var localStorageService = sp.ServiceProvider.GetRequiredService<LocalStorageService>();

    var storedCulture = await localStorageService.GetValueAsync("Culture");
    if (storedCulture is not null)
    {
        var culture = new CultureInfo(storedCulture);
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}

await app.RunAsync();
