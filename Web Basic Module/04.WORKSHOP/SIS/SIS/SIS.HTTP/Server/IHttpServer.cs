using System.Threading.Tasks;

namespace SIS.HTTP.Server
{
    public interface IHttpServer
    {
        Task StartAsync();

        Task ResetAsync();

        void Stop();
    }
}
