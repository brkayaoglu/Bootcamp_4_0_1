using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Task1.Interfaces;

namespace Task1.Controllers
{

    /*
     * 
     * INJECTION LIFE  CYCLE
     * 
     * Transient, scoped ve singleton
     * 
     * Trainsient olarak cagırılan nesne her seferinde yeniden olsuturulur
     * 
     * Scoped ile ayaga kaldırdıgımız nesne kopya olusuturp  biizm karsımıza cıkar, requeste gore degisir
     * 
     * Singleton da proje ayakta oldugu surece aynı degeri verir.
     * 
     * constructor injection ve method injectionda parametre olarak vereblioyz
     * 
     * property injectionda kendimiz yazmamız gerkeiyo service i.
     * 
     */

    [ApiController]
    [Route("[controller]")]
    public class InjectionController : ControllerBase
    {

        private readonly ITransientService _transientService1;
        private readonly ITransientService _transientService2;
        private readonly IScopedService _scopedService1;
        private readonly IScopedService _scopedService2;
        private readonly ISingletonService _singletonService1;
        private readonly ISingletonService _singletonService2;


        public InjectionController(ITransientService transientService1,
                                         ITransientService transientService2,
                                         IScopedService scopedService1,
                                         IScopedService scopedService2,
                                         ISingletonService singletonService1,
                                         ISingletonService singletonService2)
        {
            _transientService1 = transientService1;
            _transientService2 = transientService2;
            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;
            _singletonService1 = singletonService1;
            _singletonService2 = singletonService2;
        }

        //method injections da from services sayesinde parametre olarak alabiliyoruz instance'ı

        [HttpGet]
        [Route("GetActionInjection")]
        public string GetActionInjection([FromServices] IScopedService scopedService)
        {
            string result = $"ScopedService : {scopedService.GetId()}";
            return result;
        }

        //Property Injectionda services kısmını kendimiz yazmamız lazım otomatik olarak gelmiyor parametreyle.

        [HttpGet]
        [Route("GetPropertyInjection")]
        public string GetPropertyInjection()
        {

            var services = this.HttpContext.RequestServices;
            var scopedService = (IScopedService)services.GetService(typeof(IScopedService));
            string res = $"Scoped Propery : {scopedService.GetId()}";
            return res;

        }

        [HttpGet]
        public string Get()
        {
            string result = $"Transient1 : {_transientService1.GetId()} {Environment.NewLine}" +
                            $"Transient2 : {_transientService2.GetId()} {Environment.NewLine}" +
                            $"Scoped1    : {_scopedService1.GetId() } {Environment.NewLine}" +
                            $"Scoped2    : {_scopedService2.GetId()} {Environment.NewLine}" +
                            $"Singleton1  : {_singletonService1.GetId() } {Environment.NewLine}" +
                            $"singleton2    : {_singletonService2.GetId()} {Environment.NewLine}";

            return result;



        }
    }
}
