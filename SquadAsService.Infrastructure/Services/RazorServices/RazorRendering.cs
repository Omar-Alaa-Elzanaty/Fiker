using Fiker.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Fiker.Infrastructure.Services.RazorServices
{
    public class RazorRendering:IRazorRendering
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITempDataProvider _tempDataProvider;

        public RazorRendering(
            IRazorViewEngine razorViewEngine,
            IServiceProvider serviceProvider,
            ITempDataProvider tempDataProvider)
        {
            _razorViewEngine = razorViewEngine;
            _serviceProvider = serviceProvider;
            _tempDataProvider = tempDataProvider;
        }

        public async Task<string> RenderToStringAsync<TModel>(string viewPath, TModel model)
        {
            var actionContext = new ActionContext
            {
                HttpContext = new DefaultHttpContext { RequestServices = _serviceProvider },
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            using (var stringWriter = new StringWriter())
            {
                var viewResult = _razorViewEngine.GetView(null, viewPath, false);
                if (!viewResult.Success)
                    throw new FileNotFoundException($"View '{viewPath}' not found.");

                var view = viewResult.View;
                var viewData = new ViewDataDictionary<TModel>(
                    new EmptyModelMetadataProvider(),
                    new ModelStateDictionary())
                {
                    Model = model
                };

                var tempData = new TempDataDictionary(actionContext.HttpContext, _tempDataProvider);
                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    viewData,
                    tempData,
                    stringWriter,
                    new HtmlHelperOptions()
                );

                await view.RenderAsync(viewContext);
                return stringWriter.ToString();
            }
        }
    }
}
