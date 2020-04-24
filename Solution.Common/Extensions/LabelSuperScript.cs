using System;
using System.Linq.Expressions;
using System.Web.Mvc;
namespace Parichay.Common
{
    //ValueChain.Common\Extensions
    public static class LabelSuperScript
    {
        public static MvcHtmlString LabelSuperScriptFor<TModel, TField>(this HtmlHelper<TModel> html,
                                                      Expression<Func<TModel, TField>> property,
                                                      object innerHtml,
                                                      object superscriptinnerHtml,
                                                      object htmlAttributes)
        {
            // lazily based on TextAreaFor
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var name = ExpressionHelper.GetExpressionText(property);
            var metadata = ModelMetadata.FromLambdaExpression(property, html.ViewData);
            string fullName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(fullName, out modelState) && modelState.Errors.Count > 0)
            {
                if (!attrs.ContainsKey("class")) attrs["class"] = string.Empty;
                attrs["class"] += " " + HtmlHelper.ValidationInputCssClassName;
                attrs["class"] = attrs["class"].ToString().Trim();
            }
            if (innerHtml == null)
                innerHtml = name;
            //SuperScript
            var superScript = new TagBuilder("sup") { InnerHtml = superscriptinnerHtml.ToString() };
            var builder = new TagBuilder("label") { InnerHtml = innerHtml.ToString() + superScript.ToString() };
            builder.MergeAttributes(attrs);
            //return MvcHtmlString.Create(builder.ToString());
            return new MvcHtmlString(builder.ToString());
        }
    }
}
    
