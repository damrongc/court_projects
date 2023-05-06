using AspNetCoreHero.ToastNotification.Abstractions;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Web.ActionFilters;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace CourtJustice.Web.Controllers
{
    [ServiceFilter(typeof(RequestAuthenticationFilter))]
    public abstract class BaseController<T> : Controller
    {
        private IMapper? _mapperInstance;
        private INotyfService? _notifyInstance;
        private IViewRenderService? _viewRenderInstance;
        private IWebHostEnvironment? _hostEnvironmentInstance;
        protected INotyfService _notify => _notifyInstance ??= HttpContext.RequestServices.GetService<INotyfService>();
        protected IMapper _mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();

        protected IViewRenderService _viewRenderer => _viewRenderInstance ??= HttpContext.RequestServices.GetService<IViewRenderService>();
        protected IWebHostEnvironment _hostEnvironment => _hostEnvironmentInstance ??= HttpContext.RequestServices.GetService<IWebHostEnvironment>();
    }
}
