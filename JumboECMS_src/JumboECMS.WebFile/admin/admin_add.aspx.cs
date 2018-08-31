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
    public partial class _admin_add : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            id = Str2Str(q("id"));
            if (id == "0")
            {
                int _uID = new JumboECMS.DAL.Normal_UserDAL().Register("(admin)" + GetRandomNumberString(10), GetRandomNumberString(16), false, 0, GetRandomNumberString(12) + "@126.com", System.DateTime.Now.ToShortTimeString(), GetRandomNumberString(32), "", "");
                Response.Redirect("admin_add.aspx?id=" + _uID);
                Response.End();
            }
            else
            {
                JumboECMS.DBUtility.WebFormHandler wh = new JumboECMS.DBUtility.WebFormHandler(doh, "jcms_normal_user", btnSave);
                wh.AddBind(lblUserName, "UserName", true);
                wh.AddBind(txtAdminName, "AdminName", true);
                wh.AddBind(rbtnAdminState, "SelectedValue", "AdminState", false);
                this.txtAdminName.ReadOnly = false;
                wh.ConditionExpress = "id=" + id;
                wh.Mode = JumboECMS.DBUtility.OperationType.Modify;
                wh.validator = chkForm;
                wh.ModifyOk += new EventHandler(save_ok);
            }
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
            doh.Reset();
            if (this.txtAdminPass1.Text.ToString() == "")
            {
                FinalMessage("请填写密码", "", 1);
                return false;
            }
            doh.SqlCmd = "SELECT AdminId FROM [jcms_normal_user] WHERE [AdminName]='" + txtAdminName.Text + "'";
            if (doh.GetDataTable().Rows.Count > 0)
            {
                FinalMessage("用户名重复", "", 1);
                return false;
            }
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            doh.Reset();
            doh.ConditionExpress = "id=" + id;
            if (this.txtAdminPass1.Text != "")
                doh.AddFieldItem("AdminPass", JumboECMS.Utils.MD5.Lower32(this.txtAdminPass1.Text));
            doh.AddFieldItem("Setting", ",,");
            doh.AddFieldItem("Group", site.AdminGroupId);
            doh.AddFieldItem("AdminId", id);
            doh.Update("jcms_normal_user");
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
