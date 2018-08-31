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
    public partial class _userorder_ajax : JumboECMS.UI.AdminCenter
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
                case "ajaxDelete":
                    ajaxDelete();
                    break;
                case "ajaxCheck":
                    ajaxCheck();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxGetList()
        {
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            string whereStr = "1=1";
            doh.Reset();
            doh.ConditionExpress = whereStr;
            string sqlStr = "";
            int _countnum = doh.Count("jcms_normal_user_order");
            sqlStr = JumboECMS.Utils.SqlHelp.GetSql0("*,(select username from [jcms_normal_user] where id =jcms_normal_user_order.userid) as username", "jcms_normal_user_order", "Id", PSize, page, "desc", whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagerbar\" :\"" + JumboECMS.Utils.HtmlPager.GetPageBar(3, "js", 2, _countnum, PSize, page, "javascript:ajaxList(*);") + "\"," +
                JumboECMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxCheck()
        {
            string orderNum = f("ordernum");
            doh.Reset();
            doh.ConditionExpress = "ordernum=@ordernum and State>=0";
            doh.AddConditionParameter("@ordernum", orderNum);
            doh.AddFieldItem("State", 2);
            if (doh.Update("jcms_normal_user_order") == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "ordernum=@ordernum and state>=0";
                doh.AddConditionParameter("@ordernum", orderNum);
                doh.AddFieldItem("State", 2);
                doh.Update("jcms_normal_user_goods");
                this._response = JsonResult(1, "设置成功");
            }
            else
                this._response = JsonResult(0, "设置失败");
        }
        private void ajaxDelete()
        {
            string orderNum = f("ordernum");
            doh.Reset();
            doh.ConditionExpress = "ordernum=@ordernum and State=0";
            doh.AddConditionParameter("@ordernum", orderNum);
            if (doh.Delete("jcms_normal_user_order") == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "ordernum=@ordernum and state=0";
                doh.AddConditionParameter("@ordernum", orderNum);
                doh.Delete("jcms_normal_user_goods");
                this._response = JsonResult(1, "成功作废");
            }
            else
                this._response = JsonResult(0, "只有未支付的订单才能作废");
        }
    }
}