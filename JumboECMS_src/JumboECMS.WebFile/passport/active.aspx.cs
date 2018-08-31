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
using JumboECMS.Common;

namespace JumboECMS.WebFile.Passport
{
    public partial class _active : JumboECMS.UI.BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uUserName = q("username");
            string uEmail = q("email");
            string uUserSign = q("usersign");
            doh.Reset();
            doh.ConditionExpress = "username=@username and usersign=@usersign";
            doh.AddConditionParameter("@username", uUserName);
            doh.AddConditionParameter("@usersign", uUserSign);
            doh.AddFieldItem("State", 1);
            doh.AddFieldItem("UserSign", "");
            if (doh.Update("jcms_normal_user") == 1)
                Response.Write("<script>alert('您的帐号已激活成功');window.location.href='" + site.Dir + "passport/login.aspx';</script>");
            else
                Response.Write("<script>alert('参数失败');window.close();</script>");
        }
    }
}
