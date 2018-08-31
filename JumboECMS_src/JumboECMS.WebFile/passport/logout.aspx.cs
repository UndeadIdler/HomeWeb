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
    public partial class _logout : JumboECMS.UI.UserCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (new JumboECMS.DAL.Normal_UserDAL().ChkUserLogout(q("userkey")))
            {
                if(q("refer") != "")
                    FinalMessage("已清除您的登录信息", q("refer"), 0);
                else
                    FinalMessage("已清除您的登录信息", Request.ServerVariables["HTTP_REFERER"].ToString(), 0);
            }
            else
                FinalMessage("无法确定您的身份", site.Dir, 0);
        }
    }
}
