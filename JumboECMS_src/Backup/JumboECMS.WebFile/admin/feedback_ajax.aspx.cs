﻿/*
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
using JumboECMS.Common;
using JumboECMS.Utils;
namespace JumboECMS.WebFile.Admin
{
    public partial class _feedback_ajax : JumboECMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Server.ScriptTimeout = 8;//脚本过期时间
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
                    break;
                default:
                    DefaultResponse();
                    break;
            }

            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = "{\"result\" :\"未知操作\"}";
        }
        private void ajaxGetList()
        {
            Admin_Load("master", "json");
            int PSize = Str2Int(q("pagesize"), 0) == 0 ? 15 : Str2Int(q("pagesize"), 0);
            int page = Int_ThisPage();
            string sqlStr = "";
            int countNum = 0;
            string whereStr = "1=1";
            doh.Reset();
            doh.ConditionExpress = whereStr;
            countNum = doh.Count("jcms_normal_feedback");
            sqlStr = JumboECMS.Utils.SqlHelp.GetSql0("*", "jcms_normal_feedback", "id", PSize, page, "desc", whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagerbar\" :\"" + JumboECMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboECMS.Utils.dtHelp.DT2JSON(dt) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        /// <summary>
        /// 执行批量操作
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="ids"></param>
        private void ajaxBatchOper()
        {
            Admin_Load("master", "json");
            string act = q("act");
            string ids = f("ids");
            string[] idValue;
            idValue = ids.Split(',');
            string ClassId = string.Empty;
            if (act == "pass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.AddFieldItem("IsPass", 1);
                    doh.Update("jcms_normal_feedback");
                }
            }
            else if (act == "nopass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.AddFieldItem("IsPass", 0);
                    doh.Update("jcms_normal_feedback");
                }
            }
            else//均为删除
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.Delete("jcms_normal_feedback");
                    doh.Reset();
                    doh.ConditionExpress = "ParentId=" + idValue[i];
                    doh.Delete("jcms_normal_feedback");
                }
            }
            this._response = JsonResult(1, "操作成功");
        }
    }
}
