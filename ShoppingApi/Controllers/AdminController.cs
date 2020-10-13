using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ShoppingApi.Profiles;
using System.Linq;

namespace ShoppingApi.Controllers
{
    public class AdminController : ControllerBase
    {
        private readonly IOptions<ConfigurationForMapper> _options;
        IConfigurationRoot _configRoot;

        // IConfigurationRoot does not have providers which is why we inject Configuration and cast
        public AdminController(IConfiguration config, IOptions<ConfigurationForMapper> options)
        {
            _configRoot = (IConfigurationRoot)config;
            _options = options;
        }

        [HttpGet("admin/config")]
        public ActionResult GetConfig()
        {
            return Ok(_configRoot.Providers.Select(p => p.ToString()).ToList());
        }

        [HttpGet("admin/markup")]
        public ActionResult GetMarkeup()
        {
            return Ok($"The Markup is {_options.Value.markUp:P}");
        }
    }
}
