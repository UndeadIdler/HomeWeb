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
    public partial class module_job_ajax : JumboECMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Str2Str(q("id"));
            Admin_Load("", "json", "job");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
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

        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxCheckName()
        {
            doh.Reset();
            doh.ConditionExpress = "title=@title and id<>" + Str2Str(q("id"));
            doh.AddConditionParameter("@title", q("txtTitle"));
            if (doh.Exist("jcms_module_" + this.MainModule.Type))
                this._response = JsonResult(0, "不可录入");
            else
                this._response = JsonResult(1, "可以录入");
        }
        private void ajaxGetList()
        {
            Admin_Load(this.MainModule.Type + "-manage", "json");
            string categoryid = Str2Str(q("categoryid"));
            string _k = q("k");
            string _f = q("f");
            string _s = q("s");
            string _p = q("p");
            string _t = q("t");
            string _d = q("d");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            this._response = GetContentList(categoryid, _f, _k, _d, _s, Str2Str(q("isimg")), Str2Str(q("istop")), Str2Str(q("isfocus")), PSize, page);
        }
        private void ajaxDelete()
        {
            Admin_Load(this.MainModule.Type + "-delete", "json");
            string lId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=" + lId;
            doh.Delete("jcms_module_" + this.MainModule.Type);
            this._response = JsonResult(1, "成功删除");
        }
        /// <summary>
        /// 执行批量操作
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="ids"></param>
        private void ajaxBatchOper()
        {
            string act = q("act");
            string ids = f("ids");
            BatchContent(act, ids, "json");
            this._response = JsonResult(1, "操作成功");
        }
    }
}