using FubuMVC.Core.UI.Elements;
using HtmlTags;

namespace Rookian.TNL.Infrastructure.FubuMVCTagHelper
{
    public class SpanValidatorBuilder : IElementBuilder
    {
        public HtmlTag Build(ElementRequest request)
        {
            return new HtmlTag("span")
                .AddClass("field-validation-error")
                .AddClass("text-danger")
                .Data("valmsg-for", request.ElementId);
        }
    }
}