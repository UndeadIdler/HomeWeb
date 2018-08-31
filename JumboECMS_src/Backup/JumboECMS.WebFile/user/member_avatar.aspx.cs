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
using System.Web;
using System.Data;
using JumboECMS.Common;

namespace JumboECMS.WebFile.User
{
    public partial class _member_avatar : JumboECMS.UI.UserCenter
    {
        public string ServiceUrl = string.Empty;
        public string UserSign = string.Empty;
        public string FileFilter = "*.jpg;*.bmp;*.png;";
        public string MaxSize = string.Empty;
        public string FlashVars = string.Empty;
        private string _operType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            User_Load("", "html");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", UserId);
            doh.AddFieldItem("UserSign", UserPass);
            doh.Update("jcms_normal_user");
            ServiceUrl = ResolveUrl("ajax.aspx");
            UserSign = UserPass;
            MaxSize = "" + (1 * 1024) + "";
        }
    }
}
