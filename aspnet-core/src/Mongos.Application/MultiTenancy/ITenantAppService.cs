using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mongos.MultiTenancy.Dto;

namespace Mongos.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

