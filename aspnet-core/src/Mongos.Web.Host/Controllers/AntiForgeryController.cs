using Microsoft.AspNetCore.Antiforgery;
using Mongos.Controllers;

namespace Mongos.Web.Host.Controllers
{
    public class AntiForgeryController : MongosControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
