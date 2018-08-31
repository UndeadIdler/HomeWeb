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
using JumboECMS.Utils;
using JumboECMS.Common;

namespace JumboECMS.WebFile.Passport
{
    public partial class _login : JumboECMS.UI.FrontPassport
    {
        public string Referer = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                FinalMessage("请先注销当前用户再进行登录!", site.Dir, 0);
                Response.End();
            }
            Referer = site.Dir + "cn/index.html";
            if (q("refer") != "")
                Referer = q("refer");
            else
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    if (!Request.ServerVariables["HTTP_REFERER"].ToString().Contains("register") && !Request.ServerVariables["HTTP_REFERER"].ToString().Contains("logout"))
                        if (Request.Url.ToString() != Request.ServerVariables["HTTP_REFERER"].ToString())
                            Referer = Request.ServerVariables["HTTP_REFERER"].ToString();
                }
            }
        }
    }
}
