using DILab.Interfaces;
using DILab.Services;
using Microsoft.AspNetCore.Mvc;

namespace DILab.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DILifeCircleController : Controller
    {

        private readonly ITransient _transient;
        private readonly IScoped _scoped;
        private readonly ISingleton _singleton;
        private readonly SampleService _sampleService;

        public DILifeCircleController(ITransient transient, IScoped scoped, ISingleton singleton, SampleService sampleService)
        {
            _transient = transient;
            _scoped = scoped;
            _singleton = singleton;
            _sampleService = sampleService;
        }

        /// <summary>
        /// Transient will be different each time.
        /// Scoped will be same in the same request. And be different in different request. 
        /// Singleton will always be the same one, in the app life time.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IDictionary<string,string>> Get()
        {
            var ServiceHashCodes = _sampleService.GetServiceHashCode();
            var ControllerHashCodes = $"Transient: {_transient.GetHashCode()}, " +
                                    $"Scoped: {_scoped.GetHashCode()}, " +
                                    $"Singleton: {_singleton.GetHashCode()}";
            return new Dictionary<string, string> {
                { "ServiceHashCodes", ServiceHashCodes },
                { "ControllerHashCodes", ControllerHashCodes }
            };
        }
    }
}
