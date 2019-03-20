using System.Threading.Tasks;
using Mongos.Configuration.Dto;

namespace Mongos.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
