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
using System.Data;
using System.Web;
namespace JumboECMS.WebFile.Plus
{
    public partial class _attachment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CanDown())
            {
                Response.Write("无法查看或下载附件");
                Response.End();
            }
            else
            {
                string _file = q("file");
                if (_file != "")
                    Response.Redirect(_file);
                else
                {
                    Response.Write("参数有误");
                    Response.End();
                }
            }
        }
        private bool CanDown()
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
                return false;
            if ((HttpContext.Current.Request.UrlReferrer.Host) != (HttpContext.Current.Request.Url.Host))
                return false;
            return true;
        }
        /// <summary>
        /// 获取querystring
        /// </summary>
        /// <param name="s">参数名</param>
        /// <returns>返回值</returns>
        private string q(string s)
        {
            if (HttpContext.Current.Request.QueryString[s] != null && HttpContext.Current.Request.QueryString[s] != "")
            {
                return JumboECMS.Utils.Strings.SafetyStr(HttpContext.Current.Request.QueryString[s].ToString());
            }
            return string.Empty;
        }
    }
}
