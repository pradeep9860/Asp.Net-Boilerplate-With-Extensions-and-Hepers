using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Mongos.Controllers
{
    public abstract class MongosControllerBase: AbpController
    {
        protected MongosControllerBase()
        {
            LocalizationSourceName = MongosConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
