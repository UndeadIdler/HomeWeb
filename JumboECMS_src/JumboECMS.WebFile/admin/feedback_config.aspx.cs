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
using System.IO;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Admin
{
    public partial class _feedback_config : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            if (!Page.IsPostBack)
            {
                string strXmlFile1 = HttpContext.Current.Server.MapPath("~/_data/config/feedback.config");
                JumboECMS.DBUtility.XmlControl XmlTool1 = new JumboECMS.DBUtility.XmlControl(strXmlFile1);
                this.txtPageSize.Text = XmlTool1.GetText("Root/PageSize");
                this.txtPostTimer.Text = XmlTool1.GetText("Root/PostTimer");
                this.rblGuestPost.SelectedValue = XmlTool1.GetText("Root/GuestPost");
                this.rblNeedCheck.SelectedValue = XmlTool1.GetText("Root/NeedCheck");
                XmlTool1.Dispose();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strXmlFile1 = HttpContext.Current.Server.MapPath("~/_data/config/feedback.config");
            JumboECMS.DBUtility.XmlControl XmlTool1 = new JumboECMS.DBUtility.XmlControl(strXmlFile1);
            XmlTool1.Update("Root/PageSize", Str2Str(this.txtPageSize.Text));
            XmlTool1.Update("Root/PostTimer", Str2Str(this.txtPostTimer.Text));
            XmlTool1.Update("Root/GuestPost", Str2Str(this.rblGuestPost.SelectedValue));
            XmlTool1.Update("Root/NeedCheck", Str2Str(this.rblNeedCheck.SelectedValue));
            XmlTool1.Save();
            XmlTool1.Dispose();
            FinalMessage("成功保存", site.Dir + "admin/close.htm", 0);
        }
    }
}
