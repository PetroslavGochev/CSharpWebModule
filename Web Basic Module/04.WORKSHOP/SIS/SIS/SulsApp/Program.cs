using SIS.MvcFramework;
using System.Threading.Tasks;

namespace SulsApp
{
    public static class Program
    {
        static async Task Main()
        { 
                await WebHost.StartAsync(new Startup());          
        }
    }
}
