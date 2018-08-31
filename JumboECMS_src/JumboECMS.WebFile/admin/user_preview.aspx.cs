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
namespace JumboECMS.WebFile.Admin
{
    public partial class _user_preview : JumboECMS.UI.AdminCenter
    {
        public string userid = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            userid= Str2Str(q("id"));
        }
    }
}
