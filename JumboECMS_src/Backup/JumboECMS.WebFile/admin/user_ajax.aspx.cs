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
using JumboECMS.Utils;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Admin
{
    public partial class _user_ajax : JumboECMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Admin_Load("master", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxUserInfo":
                    GetUserInfo();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
                    break;
                case "ajaxDelete":
                    ajaxDelete();
                    break;
                case "checkname":
                    ajaxCheckName();
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
                doh.ConditionExpress = "username=@username";
                doh.AddConditionParameter("@username", q("txtUserName"));
                if (doh.Exist("jcms_normal_user"))
                    this._response = JsonResult(0, "不可添加");
                else
                    this._response = JsonResult(1, "可以添加");
            }
            else
                this._response = JsonResult(1, "可以修改");
        }
        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxGetList()
        {
            string keys = q("keys");
            int gId = Str2Int(q("gId"), 0);
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[Group]=B.Id";
            string whereStr1 = "A.AdminId=0";//外围条件(带A.)
            string whereStr2 = "AdminId=0";//分页条件(不带A.)
            if (keys.Trim().Length > 0)
            {
                whereStr1 += " and A.UserName LIKE '%" + keys + "%'";
                whereStr2 += " and UserName LIKE '%" + keys + "%'";
            }
            if (gId > 0)
            {
                whereStr1 += " and a.[Group]=" + gId.ToString();
                whereStr2 += " and [Group]=" + gId.ToString();
            }
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_user");
            sqlStr = JumboECMS.Utils.SqlHelp.GetSql0("A.id as id,A.UserName as UserName,B.GroupName as GroupName,A.Email as email,A.state as state,A.TrueName as truename,A.AdminId as adminid,A.ServiceId as serviceid", "jcms_normal_user", "jcms_normal_usergroup", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagerbar\" :\"" + JumboECMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(*);") + "\"," +
                JumboECMS.Utils.dtHelp.DT2JSON(dt) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        private void GetUserInfo()
        {
            string _userid = Str2Str(q("id"));
            int page = 1;
            int PSize = 1;
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[Group]=B.Id";
            string whereStr1 = "A.Id=" + _userid;
            string whereStr2 = "Id=" + _userid;
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_user");
            sqlStr = JumboECMS.Utils.SqlHelp.GetSql0("A.*,B.GroupName", "jcms_normal_user", "jcms_normal_usergroup", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"" + countNum + "\"," + JumboECMS.Utils.dtHelp.DT2JSON(dt) + "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxDelete()
        {
            string uId = f("id");
            if (JumboECMS.Utils.Cookie.GetValue(site.CookiePrev + "user", "id") == uId) //不能删除自己
                this._response = JsonResult(0, "不能删除自己");
            else
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id and adminid=0";
                doh.AddConditionParameter("@id", uId);
                int _delCount = doh.Delete("jcms_normal_user");
                UserGroupCount("0");
                if (_delCount > 0)
                    this._response = JsonResult(1, "成功删除");
                else
                    this._response = JsonResult(0, "管理人员不能直接删除");
            }
        }
        /// <summary>
        /// 执行批量操作
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="ids"></param>
        private void ajaxBatchOper()
        {
            string act = q("act");
            string togid = f("togid");
            string ids = f("ids");
            BatchUser(act, togid, ids, "json");
            UserGroupCount("0");
            this._response = JsonResult(1, "操作成功");
        }
        /// <summary>
        /// 执行用户的审核,用户组转移等操作
        /// </summary>
        /// <param name="_act">操作类型{pass=审核,nopass=未审,move2group=转移用户组}</param>
        /// <param name="_ids">id字符串,以","串联起来</param>
        /// <param name="pageType">页面分为html和json</param>
        public void BatchUser(string _act, string _togid, string _ids, string pageType)
        {
            string[] idValue;
            idValue = _ids.Split(',');
            if (_act == "pass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("State", 1);
                    doh.Update("jcms_normal_user");
                }
                return;
            }
            if (_act == "nopass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("State", 0);
                    doh.Update("jcms_normal_user");
                }
                return;
            }
            if (_act == "move2group")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("Group", _togid);
                    doh.Update("jcms_normal_user");
                }
                return;
            }
        }
    }
}