using System.Threading.Tasks;

using LVK.AppCore.Console;

namespace WarframeTools
{
    static class Program
    {
        static Task Main() => ConsoleAppBootstrapper.RunCommandAsync<ServicesBootstrapper>();
    }
}