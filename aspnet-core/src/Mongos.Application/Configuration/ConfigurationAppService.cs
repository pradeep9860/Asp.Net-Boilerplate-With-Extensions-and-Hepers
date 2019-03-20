using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Mongos.Configuration.Dto;

namespace Mongos.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : MongosAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
