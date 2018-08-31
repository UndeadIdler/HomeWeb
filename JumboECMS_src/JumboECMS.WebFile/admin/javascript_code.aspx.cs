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
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Admin
{
    public partial class _javascriptcode : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("js-mng", "html");
            id = Str2Str(q("id"));
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", id);
                string _code = doh.GetField("jcms_normal_javascript", "Code").ToString();
                this.ltlCode.Text = this.txtCode.Text = "<script charset=\"utf-8\" language=\"javascript\" type=\"text/javascript\" src=\"" + site.Url + site.Dir + "plus/javascript.aspx?code=" + _code + "\"></script>";
            }
        }
    }
}
