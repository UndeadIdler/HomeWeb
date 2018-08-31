/*
 * 程序中文名称: 将博内容管理系统企业版
 * 
 * 程序英文名称: JumboECMS
 * 
 * 程序版本: 1.4.x
 * 
 * 程序作者: 将博
 * 
 * 官方网站: http://www.jumboecms.net/
 * 
 */

using System;
using System.Text;
namespace JumboECMS.Utils
{
    public static class HtmlPager
    {
        /// <summary>
        /// 简单模式：数字+上下页
        /// </summary>
        /// <param name="stype"></param>
        /// <param name="stepNum"></param>
        /// <param name="pageRoot"></param>
        /// <param name="pageFoot"></param>
        /// <param name="pageCount"></param>
        /// <param name="countNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="Http1"></param>
        /// <param name="HttpN"></param>
        /// <returns></returns>
        private static string getbar1(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int countNum, int pageSize, int currentPage, string Http1, string HttpN)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='p_btns'>");
            if (countNum > pageSize)
            {
                if (currentPage != 1)//只要不是第一页
                    sb.Append("<a target='_self' href='" + GetPageUrl(currentPage - 1, Http1, HttpN) + "'>&laquo;</a>");
                if (stepNum > 0)
                {
                    for (int i = pageRoot; i <= pageFoot; i++)
                    {
                        if (i == currentPage)
                            sb.Append("<span class='currentpage'>" + i.ToString() + "</span>");
                        else
                            sb.Append("<a target='_self' href='" + GetPageUrl(i, Http1, HttpN) + "'>" + i.ToString() + "</a>");
                        if (i == pageCount)
                            break;
                    }
                }
                if (currentPage != pageCount)//只要不是最后一页
                    sb.Append("<a target='_self' href='" + GetPageUrl(currentPage + 1, Http1, HttpN) + "'>&raquo;</a>");
            }
            sb.Append("</div>");
            return sb.ToString();
        }
        /// <summary>
        /// 标准模式：数字+上下页+总记录信息
        /// </summary>
        /// <param name="stype"></param>
        /// <param name="stepNum"></param>
        /// <param name="pageRoot"></param>
        /// <param name="pageFoot"></param>
        /// <param name="pageCount"></param>
        /// <param name="countNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="Http1"></param>
        /// <param name="HttpN"></param>
        /// <returns></returns>
        private static string getbar2(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int countNum, int pageSize, int currentPage, string Http1, string HttpN)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='p_btns'>");
            //sb.Append("<span class='total_count'>共" + countNum.ToString() + "条记录，当前第" + currentPage.ToString() + "/" + pageCount.ToString() + "页&nbsp;&nbsp;</span>");
            sb.Append("<span class='total_count'>共" + countNum.ToString() + "条记录/" + pageCount.ToString() + "页&nbsp;</span>");
            if (countNum > pageSize)
            {
                if (currentPage != 1)//只要不是第一页
                    sb.Append("<a target='_self' href='" + GetPageUrl(currentPage - 1, Http1, HttpN) + "'>&laquo;</a>");
                if (stepNum > 0)
                {
                    for (int i = pageRoot; i <= pageFoot; i++)
                    {
                        if (i == currentPage)
                            sb.Append("<span class='currentpage'>" + i.ToString() + "</span>");
                        else
                            sb.Append("<a target='_self' href='" + GetPageUrl(i, Http1, HttpN) + "'>" + i.ToString() + "</a>");
                        if (i == pageCount)
                            break;
                    }
                }
                if (currentPage != pageCount)//只要不是最后一页
                    sb.Append("<a target='_self' href='" + GetPageUrl(currentPage + 1, Http1, HttpN) + "'>&raquo;</a>");
            }
            sb.Append("</div>");
            return sb.ToString();
        }
        /// <summary>
        /// 完整模式：数字+上下页+首末+总记录信息+指定页码翻转
        /// </summary>
        /// <param name="stype"></param>
        /// <param name="stepNum"></param>
        /// <param name="pageRoot"></param>
        /// <param name="pageFoot"></param>
        /// <param name="pageCount"></param>
        /// <param name="countNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="Http1"></param>
        /// <param name="HttpN"></param>
        /// <returns></returns>
        private static string getbar3(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int countNum, int pageSize, int currentPage, string Http1, string HttpN)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='p_btns'>");
            //sb.Append("<span class='total_count'>共" + countNum.ToString() + "条，当前第" + currentPage.ToString() + "/" + pageCount.ToString() + "页&nbsp;&nbsp;&nbsp;</span>");
            sb.Append("<span class='total_count'>共" + countNum.ToString() + "条记录/" + pageCount.ToString() + "页&nbsp;&nbsp;</span>");
            if (countNum > pageSize)
            {
                if (currentPage != 1)//只要不是第一页
                    sb.Append("<a target='_self' href='" + GetPageUrl(currentPage - 1, Http1, HttpN) + "'>&laquo;</a>");
                if (pageRoot > 1)
                {
                    sb.Append("<a target='_self' href='" + GetPageUrl(1, Http1, HttpN) + "'>1..</a>");
                }
                if (stepNum > 0)
                {
                    for (int i = pageRoot; i <= pageFoot; i++)
                    {
                        if (i == currentPage)
                            sb.Append("<span class='currentpage'>" + i.ToString() + "</span>");
                        else
                            sb.Append("<a target='_self' href='" + GetPageUrl(i, Http1, HttpN) + "'>" + i.ToString() + "</a>");
                        if (i == pageCount)
                            break;
                    }
                }
                if (pageFoot < pageCount)
                {
                    sb.Append("<a target='_self' href='" + GetPageUrl(pageCount, Http1, HttpN) + "'>.." + pageCount + "</a>");

                }
                if (currentPage != pageCount)//只要不是最后一页
                    sb.Append("<a target='_self' href='" + GetPageUrl(currentPage + 1, Http1, HttpN) + "'>&raquo;</a>");
                if (stype == "html")
                    sb.Append("<span class='jumppage'>转到第 <input type='text' name='custompage' size='2' onkeyup=\"this.value=this.value.replace(/\\D/g,'')\" onafterpaste=\"this.value=this.value.replace(/\\D/g,'')\" onkeydown=\"if(event.keyCode==13) {window.location='" + HttpN + "'.replace('*',this.value); return false;}\" /> 页</span>");
            }
            sb.Append("</div>");
            return sb.ToString();
        }
        /// <summary>
        /// 专用于前台中文版
        /// </summary>
        /// <param name="stype"></param>
        /// <param name="stepNum"></param>
        /// <param name="pageRoot"></param>
        /// <param name="pageFoot"></param>
        /// <param name="pageCount"></param>
        /// <param name="countNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="Http1"></param>
        /// <param name="HttpN"></param>
        /// <returns></returns>
        private static string getbar4(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int countNum, int pageSize, int currentPage, string Http1, string HttpN)
        {
            StringBuilder sb = new StringBuilder();
            if (countNum > pageSize)
            {
                sb.Append("<span class='total_count'>共" + countNum.ToString() + "条记录  当前页次" + currentPage.ToString() + "/" + pageCount.ToString() + "页&nbsp;&nbsp;&nbsp;&nbsp;</span>");

                if (currentPage != 1)
                {//只要不是第一页
                    sb.Append("<a target='_self' href='" + GetPageUrl(1, Http1, HttpN) + "'>&laquo;首页</a>");
                    sb.Append("<a target='_self' href='" + GetPageUrl(currentPage - 1, Http1, HttpN) + "'>&#8249;上一页</a>");
                }
                else
                {
                    sb.Append("<a class='disabled'>&laquo;首页</a>");
                    sb.Append("<a class='disabled'>&#8249;上一页</a>");
                }
                if (pageRoot > 1)
                {
                    sb.Append("<a target='_self' href='" + GetPageUrl(1, Http1, HttpN) + "'>1..</a>");
                }
                if (stepNum > 0)
                {
                    for (int i = pageRoot; i <= pageFoot; i++)
                    {
                        if (i == currentPage)
                            sb.Append("<span class='currentpage'>" + i.ToString() + "</span>");
                        else
                            sb.Append("<a target='_self' href='" + GetPageUrl(i, Http1, HttpN) + "'>" + i.ToString() + "</a>");
                        if (i == pageCount)
                            break;
                    }
                }
                if (pageFoot < pageCount)
                {
                    sb.Append("<a target='_self' href='" + GetPageUrl(pageCount, Http1, HttpN) + "'>.." + pageCount + "</a>");

                }
                if (currentPage != pageCount)
                {//只要不是最后一页
                    sb.Append("<a target='_self' href='" + GetPageUrl(currentPage + 1, Http1, HttpN) + "'>下一页&#8250;</a>");
                    sb.Append("<a target='_self' href='" + GetPageUrl(pageCount, Http1, HttpN) + "'>尾页&raquo;</a>");
                }
                else
                {
                    sb.Append("<a class='disabled'>下一页&#8250;</a>");
                    sb.Append("<a class='disabled'>尾页&raquo;</a>");
                }

                if (stype == "html")
                    sb.Append("<span class='jumppage'>转到第 <input type='text' name='custompage' size='2' onkeyup=\"this.value=this.value.replace(/\\D/g,'')\" onafterpaste=\"this.value=this.value.replace(/\\D/g,'')\" onkeydown=\"if(event.keyCode==13) {window.location='" + HttpN + "'.replace('*',this.value); return false;}\" /> 页</span>");
            }
            return sb.ToString();
        }
        /// <summary>
        /// 专用于前台英文版
        /// </summary>
        /// <param name="stype"></param>
        /// <param name="stepNum"></param>
        /// <param name="pageRoot"></param>
        /// <param name="pageFoot"></param>
        /// <param name="pageCount"></param>
        /// <param name="countNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="Http1"></param>
        /// <param name="HttpN"></param>
        /// <returns></returns>
        private static string getbar5(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int countNum, int pageSize, int currentPage, string Http1, string HttpN)
        {
            StringBuilder sb = new StringBuilder();
            if (countNum > pageSize)
            {
                sb.Append("<span class='total_count'>Totals " + countNum.ToString() + " Records/" + pageCount.ToString() + " Page&nbsp;&nbsp;&nbsp;&nbsp;</span>");

                if (currentPage != 1)
                {//只要不是第一页
                    sb.Append("<a target='_self' href='" + GetPageUrl(1, Http1, HttpN) + "'>&laquo;First</a>");
                    sb.Append("<a target='_self' href='" + GetPageUrl(currentPage - 1, Http1, HttpN) + "'>&#8249;Prev</a>");
                }
                else
                {
                    sb.Append("<a class='disabled'>&laquo;First</a>");
                    sb.Append("<a class='disabled'>&#8249;Prev</a>");
                }
                if (pageRoot > 1)
                {
                    sb.Append("<a target='_self' href='" + GetPageUrl(1, Http1, HttpN) + "'>1..</a>");
                }
                if (stepNum > 0)
                {
                    for (int i = pageRoot; i <= pageFoot; i++)
                    {
                        if (i == currentPage)
                            sb.Append("<span class='currentpage'>" + i.ToString() + "</span>");
                        else
                            sb.Append("<a target='_self' href='" + GetPageUrl(i, Http1, HttpN) + "'>" + i.ToString() + "</a>");
                        if (i == pageCount)
                            break;
                    }
                }
                if (pageFoot < pageCount)
                {
                    sb.Append("<a target='_self' href='" + GetPageUrl(pageCount, Http1, HttpN) + "'>.." + pageCount + "</a>");

                }
                if (currentPage != pageCount)
                {//只要不是最后一页
                    sb.Append("<a target='_self' href='" + GetPageUrl(currentPage + 1, Http1, HttpN) + "'>Next&#8250;</a>");
                    sb.Append("<a target='_self' href='" + GetPageUrl(pageCount, Http1, HttpN) + "'>Last&raquo;</a>");
                }
                else
                {
                    sb.Append("<a class='disabled'>Next&#8250;</a>");
                    sb.Append("<a class='disabled'>Last&raquo;</a>");
                }
                
            }
            return sb.ToString();
        }
        /// <summary>
        /// 分页导航
        /// </summary>
        /// <param name="mode">支持1=simple,2=normal,3=full</param>
        /// <param name="stype">html/js,只有当stype为html且mode为3的时候显示任意页的转向</param>
        /// <param name="stepNum">步数,如果步数为i，则每页的数字导航就有2i+1</param>
        /// <param name="countNum">记录总数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="Http1">第1页的链接地址模板，支持js</param>
        /// <param name="HttpN">第N页的链接地址模板，支持js</param>
        /// <param name="limitPage"></param>
        /// <returns></returns>
        public static string GetPageBar(int mode, string stype, int stepNum, int countNum, int pageSize, int currentPage, string HttpN)
        {
            return GetPageBar(mode, stype, stepNum, countNum, pageSize, currentPage, HttpN, HttpN);
        }
        public static string GetPageBar(int mode, string stype, int stepNum, int countNum, int pageSize, int currentPage, string Http1, string HttpN)
        {
            string _pagebar = "";
            //if (countNum > pageSize)
            //{
            int pageCount = countNum % pageSize == 0 ? countNum / pageSize : countNum / pageSize + 1;
            currentPage = currentPage > pageCount ? pageCount : currentPage;
            currentPage = currentPage < 1 ? 1 : currentPage;
            int stepageSize = stepNum * 2;
            int pageRoot = 1;
            int pageFoot = pageCount;
            pageCount = pageCount == 0 ? 1 : pageCount;
            if (pageCount - stepageSize < 1)//页数比较少
            {
                pageRoot = 1;
                pageFoot = pageCount;
            }
            else
            {
                pageRoot = currentPage - stepNum > 1 ? currentPage - stepNum : 1;
                pageFoot = pageRoot + stepageSize > pageCount ? pageCount : pageRoot + stepageSize;
                pageRoot = pageFoot - stepageSize < pageRoot ? pageFoot - stepageSize : pageRoot;
            }
            switch (mode)
            {
                case 1://1=simple：数字+上下页
                    _pagebar = getbar1(stype, stepNum, pageRoot, pageFoot, pageCount, countNum, pageSize, currentPage, Http1, HttpN);
                    break;
                case 2://2=normal：数字+上下页+总记录信息
                    _pagebar = getbar2(stype, stepNum, pageRoot, pageFoot, pageCount, countNum, pageSize, currentPage, Http1, HttpN);
                    break;
                case 3://3=full：数字+上下页+首末+总记录信息+指定页码翻转
                    _pagebar = getbar3(stype, stepNum, pageRoot, pageFoot, pageCount, countNum, pageSize, currentPage, Http1, HttpN);
                    break;
                case 5://专用于前台英文版
                    _pagebar = getbar5(stype, stepNum, pageRoot, pageFoot, pageCount, countNum, pageSize, currentPage, Http1, HttpN);
                    break;
                default://专用于前台中文版
                    _pagebar = getbar4(stype, stepNum, pageRoot, pageFoot, pageCount, countNum, pageSize, currentPage, Http1, HttpN);
                    break;
            }
            // }
            return _pagebar;
        }
        public static string GetPageUrl(int chkPage, string Http1, string HttpN)
        {
            string Http = string.Empty;
            if (chkPage == 1)
                Http = Http1;
            else
                Http = HttpN;
            return Http.Replace("*", chkPage.ToString());
        }
    }
}
