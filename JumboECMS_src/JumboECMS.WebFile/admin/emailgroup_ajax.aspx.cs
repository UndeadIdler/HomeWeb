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
using JumboECMS.Utils;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Admin
{
    public partial class _emailgroup_ajax : JumboECMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Admin_Load("0001", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDelete":
                    ajaxDelete();
                    break;
                case "checkname":
                    ajaxCheckName();
                    break;
                case "ajaxEmailGroupCount":
                    ajaxEmailGroupCount();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void ajaxCheckName()
        {
            if (q("id") == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "name=@name";
                doh.AddConditionParameter("@name", q("txtEmailgroupName"));
                if (doh.Exist("jcms_normal_emailgroup"))
                    this._response = "{\"result\" :\"0\",\"returnval\" :\"不可添加\"}";
                else
                    this._response = "{\"result\" :\"1\",\"returnval\" :\"可以添加\"}";
            }
            else
                this._response = "{\"result\" :\"1\",\"returnval\" :\"可以修改\"}";
        }
        private void DefaultResponse()
        {
            this._response = "{\"result\" :\"0\",\"returnval\" :\"未知操作\"}";
        }
        private void ajaxGetList()
        {
            doh.Reset();
            doh.SqlCmd = "Select [ID],[GroupName],[EmailTotal] FROM [jcms_normal_emailgroup] ORDER BY id asc";
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + JumboECMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxDelete()
        {
            string cId = f("id");
            if (Convert.ToInt32(cId) < 5)
            {
                this._response = "{\"result\" :\"0\",\"returnval\" :\"默认邮箱组不允许删除\"}";
                return;
            }
            doh.Reset();
            doh.ConditionExpress = "groupid=@groupid";
            doh.AddConditionParameter("@groupid", cId);
            doh.AddFieldItem("GroupId", 1);
            doh.Update("jcms_normal_email");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", cId);
            doh.Delete("jcms_normal_emailgroup");
            this._response = "{\"result\" :\"1\",\"returnval\" :\"成功删除\"}";
        }
        private void ajaxEmailGroupCount()
        {
            EmailGroupCount("0");
            this._response = "{\"result\" :\"1\",\"returnval\" :\"成功统计\"}";
        }
    }
}