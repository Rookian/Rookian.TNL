using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using FubuMVC.Core.UI;
using HtmlTags;
using HtmlTags.Extended.Attributes;
using WebGrease.Css.Extensions;

namespace Rookian.TNL.Infrastructure.FubuMVCTagHelper
{
    public static class FubuAspNetBlockExtensions
    {
        public static HtmlTag InputBlock<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression, Action<HtmlTag> inputModifier = null, Action<HtmlTag> validatorModifier = null)
            where T : class
        {
            inputModifier = inputModifier ?? (_ => { });
            validatorModifier = validatorModifier ?? (_ => { });
            var inputTag = helper.Input(expression);
            inputModifier(inputTag);
            var validatorTag = helper.Validator(expression);
            validatorModifier(validatorTag);

            var name = ExpressionHelper.GetExpressionText(expression);
            var fullHtmlFieldName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            var fullHtmlFieldId = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(name);

            inputTag.Id(fullHtmlFieldId);
            inputTag.Name(fullHtmlFieldName);

            var attributes = helper.GetUnobtrusiveValidationAttributes(name);
            attributes.ForEach(x => inputTag.Attr(x.Key, x.Value) );
            
            inputTag.Append(validatorTag);
            return inputTag;
        }
        public static HtmlTag FormBlock<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression, Action<HtmlTag> labelModifier = null, Action<HtmlTag> inputBlockModifier = null, Action<HtmlTag> inputModifier = null, Action<HtmlTag> validatorModifier = null)
            where T : class
        {
            labelModifier = labelModifier ?? (_ => { });
            inputBlockModifier = inputBlockModifier ?? (_ => { });
            var divTag = new HtmlTag("div");
            divTag.AddClass("form-group");
            var labelTag = helper.Label(expression);

            labelModifier(labelTag);
            var inputBlockTag = helper.InputBlock(expression, inputModifier, validatorModifier);

            inputBlockModifier(inputBlockTag);
            divTag.Append(labelTag);
            divTag.Append(inputBlockTag);

            return divTag;
        }

        public static HtmlTag SubmitBlock(this HtmlHelper helper, string text = "Submit")
        {
            var divTag = new HtmlTag("div").AddClass("form-group");
            divTag.Append(helper.Submit(text));
            return divTag;
        }

        public static HtmlTag ValidationSummaryBlock(this HtmlHelper helper, string message = "")
        {
            var divTag = new HtmlTag("div").AddClass("form-group");
            var columnTag = new HtmlTag("div").AddClasses("col-md-offset-2", "col-md-10");
            columnTag.AppendHtml(helper.ValidationErrorsSummary(message).ToString());
            divTag.Append(columnTag);
            return divTag;
        }
    }
}