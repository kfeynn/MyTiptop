using System;
using System.Text;

namespace MyTiptop.Web.Framework
{
    /// <summary>
    /// 后台分页类
    /// </summary>
    public class BootstrapPager : Pager
    {
        public BootstrapPager(PageModel pageModel): base(pageModel)
        {
        }

        public sealed override string ToString()
        {
            if (_pagemodel.TotalCount == 0 || _pagemodel.TotalCount <= _pagemodel.PageSize)
                return null;

            StringBuilder html = new StringBuilder();

            html.Append("<ul class=\"pagination pull-left\">");

            if (_showsummary)
            {
                html.Append(string.Format("<span class=\"summary\">当前{2}/{1}页&nbsp;共{0}条记录</span>", _pagemodel.TotalCount, _pagemodel.TotalPages, _pagemodel.PageNumber));
                html.Append("&nbsp;");
            }
            if (_showpagesize)
            {
                html.AppendFormat("每页:<input type=\"text\" value=\"{0}\" id=\"pageSize\" name=\"pageSize\" size=\"1\"/>", _pagemodel.PageSize);
            }
            if (_showfirst)
            {
                if (_pagemodel.IsFirstPage)
                    html.Append("<li class=\"disabled\"><a href=\"#\">首页</a></li>");
                else
                    html.Append("<li><a href=\"#\" page=\"1\" >首页</a></li>");
            }
            if (_showpre)
            {
                if (_pagemodel.HasPrePage)
                    html.AppendFormat("<li><a href=\"#\" page=\"{0}\" >上一页</a></li>", _pagemodel.PageNumber - 1);
                else
                    html.Append("<li class=\"disabled\"><a href=\"#\">上一页</a></li>");
            }
            if (_showitems)
            {
                int startPageNumber = GetStartPageNumber();
                int endPageNumber = GetEndPageNumber();
                for (int i = startPageNumber; i <= endPageNumber; i++)
                {
                    if (_pagemodel.PageNumber != i)
                        html.AppendFormat("<li><a href=\"#\" page=\"{0}\" >{0}</a></li>", i);
                    else
                        html.AppendFormat("<li class=\"active\"><a href=\"\" >{0}</a></li>", i);
                }
            }
            if (_shownext)
            {
                if (_pagemodel.HasNextPage)
                    html.AppendFormat("<li><a href=\"#\" page=\"{0}\" >下一页</a></li>", _pagemodel.PageNumber + 1);
                else
                    html.Append("<li class=\"disabled\"><a href=\"#\">下一页</a></li>");
            }
            if (_showlast)
            {
                if (_pagemodel.IsLastPage)
                    html.Append("<li class=\"disabled\"><a href=\"#\">末页</a></li>");
                else
                    html.AppendFormat("<li><a href=\"#\" page=\"{0}\">末页</a>", _pagemodel.TotalPages);
            }

            if (_showgopage)
            {
                html.AppendFormat("跳转到:<input type=\"text\" value=\"{0}\" id=\"pageNumber\" totalPages=\"{1}\" name=\"pageNumber\" size=\"1\"/>页", _pagemodel.PageNumber, _pagemodel.TotalPages);
            }
            html.Append("</ul>");

            return html.ToString();
        }
    }
}
