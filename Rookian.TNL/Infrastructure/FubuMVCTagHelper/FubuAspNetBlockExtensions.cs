using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using FubuMVC.Core.UI;
using HtmlTags;
using Rookian.TNL.Infrastructure.Extensions;
using Rookian.TNL.Infrastructure.Handler;
using WebGrease.Css.Extensions;

namespace Rookian.TNL.Infrastructure.FubuMVCTagHelper
{
    public static class FubuAspNetBlockExtensions
    {
        public static HtmlTag InputBlock<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression, Action<HtmlTag> inputModifier = null) where T : class
        {
            inputModifier = inputModifier ?? (_ => { });

            var inputTag = helper.Input(expression);
            inputModifier(inputTag);
          
            var name = ExpressionHelper.GetExpressionText(expression);
            var fullHtmlFieldName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            var fullHtmlFieldId = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(name);

            inputTag.Id(fullHtmlFieldId);
            inputTag.Name(fullHtmlFieldName);

            var attributes = helper.GetUnobtrusiveValidationAttributes(name);
            attributes.ForEach(x => inputTag.Attr(x.Key, x.Value) );

            return inputTag;
        }

        public static HtmlTag QueryDropDownFormBlock<T, TItem, TQuery>(this HtmlHelper<T> htmlHelper, Expression<Func<T, TItem>> expression, TQuery query, Func<TItem, string> displaySelector, Func<TItem, object> valueSelector, Action<HtmlTag> labelModifier = null, Action<HtmlTag> queryInputModifier = null)
            where TQuery : IQuery<IEnumerable<TItem>>
            where T : class
        {
            labelModifier = labelModifier ?? (_ => { });
            queryInputModifier = queryInputModifier ?? (_ => { });

            var divTag = new HtmlTag("div");
            divTag.AddClass("form-group");

            var expr = expression.ToUntypedPropertyExpression();

            var labelTag = htmlHelper.Label(expr);
            labelModifier(labelTag);

            var queryInput = htmlHelper.QueryDropDown(expr, query, displaySelector, valueSelector);
            queryInputModifier(queryInput);

            var validatorTag = htmlHelper.ValidationMessageFor(expression);

            divTag.Append(labelTag);
            divTag.Append(queryInput);
            divTag.AppendHtml(validatorTag.ToHtmlString());

            return divTag;
        }

        public static HtmlTag FormBlock<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression, Action<HtmlTag> labelModifier = null, Action<HtmlTag> inputBlockModifier = null, Action<HtmlTag> inputModifier = null) where T : class
        {
            labelModifier = labelModifier ?? (_ => { });
            inputBlockModifier = inputBlockModifier ?? (_ => { });

            var divTag = new HtmlTag("div");
            divTag.AddClass("form-group");
            
            var labelTag = helper.Label(expression);
            labelModifier(labelTag);

            var inputBlockTag = helper.InputBlock(expression, inputModifier);
            inputBlockModifier(inputBlockTag);

            var validatorTag = helper.ValidationMessageFor(expression);

           
            divTag.Append(labelTag);
            divTag.Append(inputBlockTag);
            divTag.AppendHtml(validatorTag.ToHtmlString());

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