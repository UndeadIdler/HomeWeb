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
    public partial class _register_step2 : JumboECMS.UI.FrontPassport
    {
        public string _UserName = "";
        public string _Email = "";
        public string _UserSign = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _UserName = q("username");
            _Email = q("email");
            _UserSign = q("usersign");

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
            if (JumboECMS.Utils.Session.Get("jcms_user_register") != "1")
            {
                Response.Write("请勿随便试这个功能");
                Response.End();
            }
        }

    }
}
