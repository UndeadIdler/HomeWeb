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
    public partial class _config_index : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("config-mng", "stop");
            if (!Page.IsPostBack)
            {
                string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/site.config");
                JumboECMS.DBUtility.XmlControl XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
                this.txtName1.Text = XmlTool.GetText("Root/Name1");
                this.txtUrl.Text = XmlTool.GetText("Root/Url");
                this.txtICP1.Text = XmlTool.GetText("Root/ICP1");
                this.txtKeywords1.Text = XmlTool.GetText("Root/Keywords1");
                this.txtDescription1.Text = XmlTool.GetText("Root/Description1");
                this.txtName2.Text = XmlTool.GetText("Root/Name2");
                this.txtICP2.Text = XmlTool.GetText("Root/ICP2");
                this.txtKeywords2.Text = XmlTool.GetText("Root/Keywords2");
                this.txtDescription2.Text = XmlTool.GetText("Root/Description2");
                this.rblAllowReg.Items.FindByValue(XmlTool.GetText("Root/AllowReg")).Selected = true;
                this.rblCheckReg.Items.FindByValue(XmlTool.GetText("Root/CheckReg")).Selected = true;
                XmlTool.Dispose();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/site.config");
            JumboECMS.DBUtility.XmlControl XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
            XmlTool.Update("Root/Name1", this.txtName1.Text);
            XmlTool.Update("Root/Url", this.txtUrl.Text);
            XmlTool.Update("Root/ICP1", this.txtICP1.Text);
            XmlTool.Update("Root/Keywords1", this.txtKeywords1.Text);
            XmlTool.Update("Root/Description1", this.txtDescription1.Text);
            XmlTool.Update("Root/Name2", this.txtName2.Text);
            XmlTool.Update("Root/ICP2", this.txtICP2.Text);
            XmlTool.Update("Root/Keywords2", this.txtKeywords2.Text);
            XmlTool.Update("Root/Description2", this.txtDescription2.Text);
            XmlTool.Update("Root/AllowReg", this.rblAllowReg.SelectedItem.Value);
            XmlTool.Update("Root/CheckReg", this.rblCheckReg.SelectedItem.Value);
            XmlTool.Save();
            XmlTool.Dispose();
            doh.Reset();
            doh.ConditionExpress = "id=1";
            doh.AddFieldItem("title", this.txtName1.Text);
            doh.Update(base.CategoryTable);
            doh.Reset();
            doh.ConditionExpress = "id=2";
            doh.AddFieldItem("title", this.txtName2.Text);
            doh.Update(base.CategoryTable);
            new JumboECMS.DAL.SiteDAL().CreateSiteFiles();
            SetupSystemDate();
            new JumboECMS.DAL.Normal_AdminlogsDAL().SaveLog(AdminId, "修改了网站参数");
            FinalMessage("保存成功,已更新缓存!", "configset_default.aspx", 0);
        }
    }
}
