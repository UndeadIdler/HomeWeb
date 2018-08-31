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
    public class Module_newsDAL : Common, IModule
    {
        public Module_newsDAL()
        {
            base.SetupSystemDate();
        }
        public virtual void CreateContent(string _moduletype, string _contentid, int _page)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Content],[FirstPage] FROM [jcms_module_" + _moduletype + "] WHERE [Id]=" + _contentid;
                DataTable dtContent = _doh.GetDataTable();
                string ArticleContent = dtContent.Rows[0]["Content"].ToString();
                string ContentFirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                dtContent.Clear();
                dtContent.Dispose();
                if (ArticleContent != "")
                {
                    if (ContentFirstPage.Length == 0)
                    {
                        _doh.Reset();
                        _doh.SqlCmd = "UPDATE [jcms_module_" + _moduletype + "] SET [FirstPage]='" + Go2View(_moduletype, _contentid, 1) + "' WHERE [IsPass]=1 and [Id]=" + _contentid;
                        _doh.ExecuteSqlNonQuery();
                    }
                    JumboECMS.Utils.DirFile.SaveFile(GetContent(_moduletype, _contentid, 1), Go2View(_moduletype, _contentid, 1));
                }
            }
        }
        public virtual string GetContent(string _moduletype, string _contentid, int _page)
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
                string _FirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
                p__GetPage(te, dtContent, ref PageStr, ref ContentList, 0);
                te.ReplaceContentTag("news", ref PageStr, _contentid);
                te.ReplaceContentLoopTag(ref PageStr);//主要解决通过tags关联
                ContentList.Add(PageStr);
                p__replaceSingleArticle(dtContent, ref _page, ref PageStr, ref ContentList);

                dtContent.Clear();
                dtContent.Dispose();

                return ContentList[0].ToString();
            }
        }
        /// <summary>
        /// 得到内容页地址
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <param name="_contentid"></param>
        /// <param name="_page"></param>
        /// <returns></returns>
        public string GetContentLink(string _moduletype, string _contentid, int _page)
        {
            object[] _value = new ModuleContentDAL().GetSome(_moduletype, _contentid);
            string _date = _value[0].ToString();
            string _firstpage = _value[1].ToString();
            string _aliaspage = _value[2].ToString();
            string _categoryfilepath = _value[3].ToString();
            string TempUrl = JumboECMS.Common.PageFormat.View(site.Dir, _moduletype, _page);
            if (_aliaspage.Length > 10 && _page == 1)
                return _aliaspage;
            TempUrl = TempUrl.Replace("<#SiteDir#>", site.Dir);
            TempUrl = TempUrl.Replace("<#ModuleType#>", _moduletype);
            TempUrl = TempUrl.Replace("<#CategoryFilePath#>", _categoryfilepath);
            TempUrl = TempUrl.Replace("<#id#>", _contentid);
            if (_date != "")
            {
                TempUrl = TempUrl.Replace("<#year#>", DateTime.Parse(_date).ToString("yyyy"));
                TempUrl = TempUrl.Replace("<#month#>", DateTime.Parse(_date).ToString("MM"));
                TempUrl = TempUrl.Replace("<#day#>", DateTime.Parse(_date).ToString("dd"));
            }
            if (_page > 0) TempUrl = TempUrl.Replace("*", _page.ToString());
            return TempUrl;
        }
        /// <summary>
        /// 删除内容页
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <param name="_contentid"></param>
        public void DeleteContent(string _moduletype, string _contentid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "[Id]=" + _contentid;
                object[] _value = _doh.GetFields("jcms_module_" + _moduletype, "AddDate,FirstPage");
                string _date = _value[0].ToString();
                string _firstpage = _value[1].ToString();
                if (_firstpage.Length > 0)
                {
                    string _folderName = String.Format("/{0}{1}",
                        DateTime.Parse(_date).ToString("yyyy"),
                        DateTime.Parse(_date).ToString("MM"),
                        DateTime.Parse(_date).ToString("dd")
                        );
                    if (System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(site.Dir + _moduletype + _folderName)))
                    {
                        string htmFile = HttpContext.Current.Server.MapPath(Go2View(_moduletype, _contentid, 1));
                        if (System.IO.File.Exists(htmFile))
                            System.IO.File.Delete(htmFile);
                        string[] htmFiles = System.IO.Directory.GetFiles(HttpContext.Current.Server.MapPath(site.Dir + _moduletype + _folderName), _contentid + "_*.html");
                        foreach (string fileName in htmFiles)
                        {
                            if (System.IO.File.Exists(fileName))
                                System.IO.File.Delete(fileName);
                        }
                    }
                    /*
                    _doh.Reset();
                    _doh.SqlCmd = "UPDATE [jcms_module_" + _moduletype + "] SET [FirstPage]='' WHERE [Id]=" + _contentid;
                    _doh.ExecuteSqlNonQuery();
                     * */
                }
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
            //te.ReplaceContentLoopTag(ref PageStr);//先不要解析
        }
        private void p__replaceSingleArticle(DataTable dt, ref int _CurrentPage, ref string PageStr, ref System.Collections.ArrayList ContentList)
        {
        }
    }
}
