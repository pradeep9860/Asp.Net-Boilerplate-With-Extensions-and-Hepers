using System.Threading.Tasks;
using Abp.Application.Services;
using Mongos.Authorization.Accounts.Dto;

namespace Mongos.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
