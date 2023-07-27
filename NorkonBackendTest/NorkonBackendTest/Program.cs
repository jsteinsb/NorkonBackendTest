using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("--------------------------");
Console.WriteLine("Norkon Connection Streamer");
Console.WriteLine("--------------------------");

var builder = new ConfigurationBuilder();
BuildConfig(builder);
builder.Build();

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<INorkonConnectionStreamer, NorkonConnectionStreamer>();
        services.AddSingleton<INorkonConnectionSource, WebSocketNorkonConnectionSource>();
    })
    .Build();

var streamer = host.Services.GetRequiredService<INorkonConnectionStreamer>();

await foreach (var update in streamer.StreamUpdates(default))
    Console.WriteLine(update);

static void BuildConfig(IConfigurationBuilder builder)
{
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .AddEnvironmentVariables();
}