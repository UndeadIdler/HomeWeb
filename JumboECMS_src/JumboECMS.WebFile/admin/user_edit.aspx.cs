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
using System.Web.UI.WebControls;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Admin
{
    public partial class _user_edit : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            id = Str2Str(q("id"));
            JumboECMS.DBUtility.WebFormHandler wh = new JumboECMS.DBUtility.WebFormHandler(doh, "jcms_normal_user", btnSave);
            wh.AddBind(txtUserName, "UserName", true);
            this.txtUserName.ReadOnly = true;
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
            string _uName = this.txtUserName.Text;
            JumboECMS.DAL.Normal_UserDAL _User = new JumboECMS.DAL.Normal_UserDAL();
            JumboECMS.DAL.Normal_AdminlogsDAL _Adminlogs = new JumboECMS.DAL.Normal_AdminlogsDAL();
            _User.ChangePsd(id, JumboECMS.Utils.MD5.Lower32(this.txtUserPass.Text));
            _Adminlogs.SaveLog(AdminId, "修改了ID为" + id + "的用户的密码为:" + this.txtUserPass.Text);
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
