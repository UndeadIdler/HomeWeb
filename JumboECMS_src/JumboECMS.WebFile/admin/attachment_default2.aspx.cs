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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Admin.Attachment
{
    public partial class _default2 : JumboECMS.UI.AdminCenter
    {
        public string UploadFileType;
        public int UploadFileSizeLimit = 0;
        public string UploadPage;
        public string Args;
        protected void Page_Load(object sender, EventArgs e)
        {
            ModuleType = q("module").Trim();
            Admin_Load("", "html", ModuleType);

            //以下是通过flash将验证信息发送到地址栏
            //注意：Flash上传接收页在非IE的浏览器下获取不到Session和Cookies
            doh.Reset();
            doh.ConditionExpress = "adminid=@adminid";
            doh.AddConditionParameter("@adminid", AdminId);
            doh.AddFieldItem("AdminSign", AdminPass);
            doh.Update("jcms_normal_user");
            UploadPage = ResolveUrl("attachment_upfile.aspx") + "?adminsign=" + AdminPass + "&adminid=" + AdminId + "&module=" + MainModule.Type;
            UploadFileSizeLimit = MainModule.UploadSize;
            UploadFileType = MainModule.UploadType;
        }
    }
}
