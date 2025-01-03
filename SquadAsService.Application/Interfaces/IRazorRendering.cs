using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Interfaces
{
    public interface IRazorRendering
    {
        Task<string> RenderToStringAsync<TModel>(string viewPath, TModel model);
    }
}
