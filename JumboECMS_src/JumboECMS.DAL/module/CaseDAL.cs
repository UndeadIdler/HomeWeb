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
using System.Web.UI;
using JumboECMS.Utils;
using JumboECMS.DBUtility;

namespace JumboECMS.DAL
{
    public class Module_caseDAL : Module_newsDAL
    {
        public Module_caseDAL()
        {
        }
        public override void CreateContent(string _moduletype, string _contentid, int _page)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [FirstPage] FROM [jcms_module_" + _moduletype + "] WHERE [Id]=" + _contentid;
                DataTable dtContent = _doh.GetDataTable();
                string ContentFirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                dtContent.Clear();
                dtContent.Dispose();
                if (ContentFirstPage.Length == 0)
                {
                    _doh.Reset();
                    _doh.SqlCmd = "UPDATE [jcms_module_" + _moduletype + "] SET [FirstPage]='" + Go2View(_moduletype, _contentid, 1) + "' WHERE [IsPass]=1 and [Id]=" + _contentid;
                    _doh.ExecuteSqlNonQuery();
                }
                JumboECMS.Utils.DirFile.SaveFile(GetContent(_moduletype, _contentid, 1), Go2View(_moduletype, _contentid, 1));
            }
        }
        public override string GetContent(string _moduletype, string _contentid, int _page)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [CategoryId] FROM [jcms_module_" + _moduletype + "] WHERE [IsPass]=1 and [Id]=" + _contentid;
                DataTable dtSearch = _doh.GetDataTable();
                if (dtSearch.Rows.Count == 0)
                {
                    dtSearch.Clear();
                    dtSearch.Dispose();
                    return "内容错误";
                }
                string ClassId = dtSearch.Rows[0]["CategoryId"].ToString();
                dtSearch.Clear();
                dtSearch.Dispose();
                TemplateEngineDAL te = new TemplateEngineDAL();
                _doh.Reset();
                _doh.SqlCmd = "SELECT Id FROM [" + base.CategoryTable + "] WHERE [Id]=" + ClassId;
                if (_doh.GetDataTable().Rows.Count == 0)
                {
                    return "栏目错误";
                }
                string PageStr = string.Empty;
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_module_" + _moduletype + "] WHERE [IsPass]=1 and [Id]=" + _contentid;
                DataTable dtContent = _doh.GetDataTable();
                System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
                p__GetPage(te, dtContent, ref PageStr, ref ContentList, 0);
                te.ReplaceContentTag("case", ref PageStr, _contentid);
                te.ReplaceContentLoopTag(ref PageStr);//主要解决通过tags关联
                ContentList.Add(PageStr);
                p__replaceSingleCase(dtContent, ref PageStr, ref ContentList);
                dtContent.Clear();
                dtContent.Dispose();
                return ContentList[0].ToString();
            }
        }
        private void p__GetPage(TemplateEngineDAL te, DataTable dt, ref string PageStr, ref System.Collections.ArrayList ContentList, int i)
        {
            string TempId = string.Empty;
            string CurrentCategoryId = dt.Rows[i]["CategoryId"].ToString();
            string ParentCategoryId = string.Empty;
            string TopCategoryId = string.Empty;
            string LanguageCode = string.Empty;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "[Id]=" + dt.Rows[i]["CategoryId"].ToString();
                object[] value = _doh.GetFields(base.CategoryTable, "ContentTemp,ParentId,TopId,LanguageCode");
                TempId = value[0].ToString();
                ParentCategoryId = value[1].ToString();
                TopCategoryId = value[2].ToString();
                LanguageCode = value[3].ToString().ToLower();
            }
            //得到模板内容
            new JumboECMS.DAL.Normal_TemplateDAL().GetTemplateContent(TempId, ref PageStr);
            te.PageNav = te.GetCategoryNavigateHTML(CurrentCategoryId);
            te.PageTitle = dt.Rows[i]["Title"] + "_" + te.GetCategoryNavigateTitle(CurrentCategoryId);
            if (LanguageCode == "cn")
                te.PageKeywords = JumboECMS.Utils.WordSpliter.GetKeyword(dt.Rows[i]["Title"].ToString()) + "," + site.Keywords1;
            else
                te.PageKeywords = JumboECMS.Utils.WordSpliter.GetKeyword(dt.Rows[i]["Title"].ToString()) + "," + site.Keywords2;
            te.PageDescription = JumboECMS.Utils.Strings.SimpleLineSummary(dt.Rows[i]["Summary"].ToString());
            te.ReplacePublicTag(ref PageStr);
            PageStr = PageStr.Replace("{$CurrentCategoryId}", CurrentCategoryId);
            PageStr = PageStr.Replace("{$ParentCategoryId}", ParentCategoryId);
            PageStr = PageStr.Replace("{$TopCategoryId}", TopCategoryId);
            te.ReplaceCategoryLoopTag(ref PageStr);
            te.ReplaceCategoryTag(ref PageStr, CurrentCategoryId);
            //te.ReplaceContentLoopTag(ref PageStr);
        }
        private void p__replaceSingleCase(DataTable dt, ref string PageStr, ref System.Collections.ArrayList ContentList)
        {
        }
    }
}
