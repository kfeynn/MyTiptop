using System;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace MyTiptop.Web.Framework
{
    /// <summary>
    /// 分页Html扩展
    /// </summary>
    public static class PagerHtmlExtension
    {
        /// <summary>
        /// 商城前台分页
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageModel">分页对象</param>
        /// <returns></returns>
        public static WebPager WebPager(this HtmlHelper helper, PageModel pageModel)
        {
            return new WebPager(pageModel, helper.ViewContext);
        } 


        /// <summary>
        /// 商城后台分页
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageModel">分页对象</param>
        /// <returns></returns>
        public static MallAdminPager MallAdminPager(this HtmlHelper helper, PageModel pageModel)
        {
            return new MallAdminPager(pageModel);
        }

        /// <summary>
        /// Bootstrap样式分页条
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageModel">分页对象</param>
        /// <returns></returns>
        public static BootstrapPager BootstrapPager(this HtmlHelper helper, PageModel pageModel)
        {
            return new BootstrapPager(pageModel);
        }

    }



    

    /// <summary>
    /// 日期控件扩展
    /// </summary>
    public static class CalendarExtensions
    {
        private static string defaultFormat = "yyyy-MM-dd";

        /// <summary>
        /// 使用特定的名称生成控件
        /// </summary>
        /// <param name="helper">HtmlHelper对象</param>
        /// <param name="name">控件名称</param>
        /// <returns>Html文本</returns>
        public static MvcHtmlString Calendar(this HtmlHelper helper, string name)
        {
            return Calendar(helper, name, defaultFormat);
        }

        public static MvcHtmlString CalendarLab(this HtmlHelper helper, string name)
        {
            //Text显示
            return CalendarLab(helper, name, defaultFormat);
        }

        /// <summary>
        /// 使用特定的名称生成控件
        /// </summary>
        /// <param name="helper">HtmlHelper对象</param>
        /// <param name="name">控件名称</param>
        /// <param name="format">显示格式</param>
        /// <returns>Html文本</returns>
        public static MvcHtmlString Calendar(this HtmlHelper helper, string name, string format)
        {
            return GenerateHtml(name, null, format);
        }

        public static MvcHtmlString CalendarLab(this HtmlHelper helper, string name, string format)
        {
             
            return GenerateHtmlLab(name, null, format);
        }

        /// <summary>
        /// 使用特定的名称和初始值生成控件
        /// </summary>
        /// <param name="helper">HtmlHelper对象</param>
        /// <param name="name">控件名称</param>
        /// <param name="date">要显示的日期时间</param>
        /// <returns>Html文本</returns>
        public static MvcHtmlString Calendar(this HtmlHelper helper, string name, DateTime? date)
        {
            return Calendar(helper, name, date, defaultFormat);
        }

        public static MvcHtmlString CalendarLab(this HtmlHelper helper, string name, DateTime? date)
        {
            return CalendarLab(helper, name, date, defaultFormat);
        }



        /// <summary>
        /// 使用特定的名称和初始值生成控件
        /// </summary>
        /// <param name="helper">HtmlHelper对象</param>
        /// <param name="name">控件名称</param>
        /// <param name="date">要显示的日期时间</param>
        /// <param name="format">显示格式</param>
        /// <returns>Html文本</returns>
        public static MvcHtmlString Calendar(this HtmlHelper helper, string name, DateTime? date, string format)
        {
            return GenerateHtml(name, date, format);
        }

        public static MvcHtmlString CalendarLab(this HtmlHelper helper, string name, DateTime? date, string format)
        {
            return GenerateHtmlLab(name, date, format);
        }


        /// <summary>
        /// 通过lambda表达式生成控件
        /// </summary>
        /// <param name="helper">HtmlHelper对象</param>
        /// <param name="expression">lambda表达式，指定要显示的属性及其所属对象</param>
        /// <returns>Html文本</returns>
        public static MvcHtmlString CalendarFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return CalendarFor(helper, expression, defaultFormat);
        }

        public static MvcHtmlString CalendarLabFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {

            return CalendarLabFor(helper, expression, defaultFormat);
        }

        /// <summary>
        /// 通过lambda表达式生成控件
        /// </summary>
        /// <param name="helper">HtmlHelper对象</param>
        /// <param name="expression">lambda表达式，指定要显示的属性及其所属对象</param>
        /// <param name="format">显示格式</param>
        /// <returns>Html文本</returns>
        public static MvcHtmlString CalendarFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string format)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            DateTime value;

            object data = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, helper.ViewData).Model;
            if (data != null && DateTime.TryParse(data.ToString(), out value))
            {
                return GenerateHtml(name, value, format);
            }
            else
            {
                return GenerateHtml(name, null, format);
            }
        }

        public static MvcHtmlString CalendarLabFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string format)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            DateTime value;

            object data = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, helper.ViewData).Model;
            if (data != null && DateTime.TryParse(data.ToString(), out value))
            {
                return GenerateHtml(name, value, format);
            }
            else
            {
                return GenerateHtml(name, null, format);
            }
        }

        /// <summary>
        /// 通过lambda表达式获取要显示的日期时间
        /// </summary>
        /// <param name="helper">HtmlHelper对象</param>
        /// <param name="expression">lambda表达式，指定要显示的属性及其所属对象</param>
        /// <param name="format">显示格式</param>
        /// <returns>Html文本</returns>
        public static MvcHtmlString CalendarDisplayFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string format)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            DateTime value;
            string Htmlstr = "";
            object data = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, helper.ViewData).Model;
            if (data != null && DateTime.TryParse(data.ToString(), out value))
            {
                Htmlstr = value.ToString(format);
            }
            else
            {
                Htmlstr = string.Empty;
            }
            return MvcHtmlString.Create(Htmlstr);
        }

        /// <summary>
        /// 通过lambda表达式获取要显示的日期时间
        /// </summary>
        /// <param name="helper">HtmlHelper对象</param>
        /// <param name="expression">lambda表达式，指定要显示的属性及其所属对象</param>
        /// <returns>Html文本</returns>
        public static MvcHtmlString CalendarDisplayFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return CalendarDisplayFor(helper, expression, defaultFormat);
        }

        /// <summary>
        /// 生成输入框的Html
        /// </summary>
        /// <param name="name">calendar的名称</param>
        /// <param name="date">calendar的值</param>
        /// <returns>html文本</returns>
        private static MvcHtmlString GenerateHtml(string name, DateTime? date, string format)
        {
            string Htmlstr = "";
            if (date != null)
            {
                Htmlstr = "<input type=\"text\" id=\"" + name + "\" name=\"" + name + "\" onfocus=\"WdatePicker({dateFmt:'" + format + "'})\" class=\"Wdate\" value=\"" + date.Value.ToString(format) + "\" />";
            }
            else
            {
                Htmlstr = "<input type=\"text\" id=\"" + name + "\" name=\"" + name + "\" onfocus=\"WdatePicker({dateFmt:'" + format + "'})\" class=\"Wdate\" value=\"\" />";
            }
            return MvcHtmlString.Create(Htmlstr);
        }

        private static MvcHtmlString GenerateHtmlLab(string name, DateTime? date, string format)
        {
            string Htmlstr = "";
            if (date != null)
            {
                Htmlstr = "<lable for=\"" + name + "\"> "+date.Value.ToString(format)+"</lable>";
            }
            else
            {
                Htmlstr = "<lable for=\"" + name + "\"></lable>";
            }
            return MvcHtmlString.Create(Htmlstr);
        }

    }





    



}
