using System;
using FubuMVC.Core.UI.Elements;

namespace Rookian.TNL.Infrastructure.FubuMVCTagHelper
{
    public class AspNetMvcElementNamingConvention : IElementNamingConvention
    {
        public string GetName(Type modelType, FubuCore.Reflection.Accessor accessor)
        {
            var t = string.Join(".", accessor.PropertyNames).Replace(".[", "[");
            return t;
        }
    }
}