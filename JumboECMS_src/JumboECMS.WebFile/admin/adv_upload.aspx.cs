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
namespace JumboECMS.WebFile.Admin
{
    public partial class _adv_upload : JumboECMS.UI.AdminCenter
    {
        private string _sAdminUploadType;
        private int _sAdminUploadSize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("", "html");
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/upload_admin.config");
            JumboECMS.DBUtility.XmlControl XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
            this._sAdminUploadType = XmlTool.GetText("Module/adv/type");
            this._sAdminUploadSize = Str2Int(XmlTool.GetText("Module/adv/size"), 1024);
            XmlTool.Dispose();
            //以下是通过flash将验证信息发送到地址栏
            //注意：Flash上传接收页在非IE的浏览器下获取不到Session和Cookies
            doh.Reset();
            doh.ConditionExpress = "adminid=@adminid";
            doh.AddConditionParameter("@adminid", AdminId);
            doh.AddFieldItem("AdminSign", AdminPass);
            doh.Update("jcms_normal_user");
            this.flashUpload.UploadPage = "adv_upfile.aspx";
            this.flashUpload.Args = "adminsign=" + AdminPass + ";adminid=" + AdminId + ";";
            this.flashUpload.UploadFileSizeLimit = this._sAdminUploadSize * 1024;
            this.flashUpload.FileTypeDescription = this._sAdminUploadType;
        }
    }
}
