using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mongos.Roles.Dto;
using Mongos.Users.Dto;

namespace Mongos.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
