using Microsoft.Extensions.DependencyInjection;
using Producer;
using Producer.Interfaces;

var host = HostConfigure.CreateHost();

try
{
    try
    {
        var executor = host.Services.GetService<IExecutor>();
        await executor.ExecuteAsync(args);
    }
    catch (NullReferenceException ex)
    {
        throw new Exception("Не реализован исполняемый сервис", ex);
    }
}
catch (Exception e)
{
    Console.WriteLine(e);
    Console.ReadLine();
}
