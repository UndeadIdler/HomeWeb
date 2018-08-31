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
    public partial class _admin_edit : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            id = Str2Str(q("id"));
            JumboECMS.DBUtility.WebFormHandler wh = new JumboECMS.DBUtility.WebFormHandler(doh, "jcms_normal_user", btnSave);
            wh.AddBind(txtAdminName, "AdminName", true);
            wh.AddBind(rbtnAdminState, "SelectedValue", "AdminState", false);
            this.txtAdminName.ReadOnly = true;
            wh.ConditionExpress = "id=" + id;
            wh.Mode = JumboECMS.DBUtility.OperationType.Modify;
            wh.validator = chkForm;
            wh.ModifyOk += new EventHandler(save_ok);
        }
        protected void bind_ok(object sender, EventArgs e)
        {
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            if (this.txtAdminPass.Text.Length > 0)
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", id);
                doh.AddFieldItem("AdminPass", JumboECMS.Utils.MD5.Lower32(this.txtAdminPass.Text));
                doh.Update("jcms_normal_user");
            }
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
