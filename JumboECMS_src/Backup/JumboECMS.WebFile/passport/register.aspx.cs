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
    public partial class _register : JumboECMS.UI.FrontPassport
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                FinalMessage("请先注销当前用户再进行注册!", site.Dir, 0);
                Response.End();
            }
            if (!site.AllowReg)
            {
                FinalMessage("对不起，本站暂停注册!", site.Dir, 0);
                Response.End();
            }
        }
    }
}
