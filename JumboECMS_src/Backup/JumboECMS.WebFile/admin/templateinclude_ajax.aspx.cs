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
    public partial class _templateinclude_ajax : JumboECMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        public string pId, tpPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                case "updatefore":
                    ajaxUpdateFore();
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
            if (q("id") == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "title=@title";
                doh.AddConditionParameter("@title", q("txtTitle"));
                if (doh.Exist("jcms_normal_templateinclude"))
                    this._response = JsonResult(0, "不可添加");
                else
                    this._response = JsonResult(1, "可以添加");
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "title=@title and id<>" + q("id");
                doh.AddConditionParameter("@title", q("txtTitle"));
                if (doh.Exist("jcms_normal_templateinclude"))
                    this._response = JsonResult(0, "不可修改");
                else
                    this._response = JsonResult(1, "可以修改");
            }
        }
        private void ajaxGetList()
        {
            int _NeedBuild = Str2Int(q("needbuild"));
            Admin_Load("", "json");
            doh.Reset();
            if (_NeedBuild == 1)
                doh.SqlCmd = "SELECT id,title,info,Sort,source,needbuild FROM [jcms_normal_templateinclude] where needbuild=1";
            else
                doh.SqlCmd = "SELECT id,title,info,Sort,source,needbuild FROM [jcms_normal_templateinclude]";

            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + JumboECMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxDelete()
        {
            Admin_Load("9999", "json");
            string lId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=" + lId;
            doh.Delete("jcms_normal_templateinclude");
            this._response = JsonResult(1, "成功删除");
        }
        private void ajaxUpdateFore()
        {
            Admin_Load("", "json");
            CreateIncludeFiles();
            this._response = JsonResult(1, "更新完成,前台页面需要刷新");
        }
        /// <summary>
        /// 生成包含文件
        /// </summary>
        private void CreateIncludeFiles()
        {
            string _source = q("source");
            doh.Reset();
            if (_source == "")
                doh.SqlCmd = "SELECT * FROM [jcms_normal_templateinclude] ORDER BY [Sort]";
            else
                doh.SqlCmd = "SELECT * FROM [jcms_normal_templateinclude] where [Source]='" + _source + "'";
            DataTable dt = doh.GetDataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string PageStr = JumboECMS.Utils.DirFile.ReadFile(site.Dir + "templates/" + tpPath + "/include/" + dt.Rows[i]["Source"].ToString());
                    if (dt.Rows[i]["NeedBuild"].ToString() == "1")
                    {
                        JumboECMS.DAL.TemplateEngineDAL teDAL = new JumboECMS.DAL.TemplateEngineDAL();
                        teDAL.IsHtml = true;
                        teDAL.ReplacePublicTag(ref PageStr);
                        teDAL.ReplaceCategoryLoopTag(ref PageStr);
                        teDAL.ReplaceContentLoopTag(ref PageStr);
                    }
                    JumboECMS.Utils.DirFile.SaveFile(PageStr, "~/_data/shtm/" + dt.Rows[i]["Source"].ToString(), true);//shtm引用
                    JumboECMS.Utils.DirFile.SaveFile(PageStr, "~/_data/html/" + dt.Rows[i]["Source"].ToString(), false);//aspx引用
                }
            }
            dt.Clear();
            dt.Dispose();
        }
    }
}