using System.Threading.Tasks;
using Abp.Application.Services;
using Mongos.Sessions.Dto;

namespace Mongos.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
