using Demo;
using Demo.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSingleton(new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Register the IConfiguration instance which appsettings.json binds against.
builder.Configuration.Bind("Demo", new Hoang());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<GraphHelper>();

await builder.Build().RunAsync();

public class Hoang
{
    public string TenantID { get; set; }
    public string ClientID { get; set; }
    public string ClientSecret { get; set; }
}
