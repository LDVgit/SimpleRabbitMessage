using Consumer;
using Consumer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

try
{
    var host = HostConfigure.CreateHost();
    await host.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine("Ошибка запуска {0}", ex.Message);
}
