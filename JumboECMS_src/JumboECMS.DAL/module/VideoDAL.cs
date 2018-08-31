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
using System.Text;
using System.Web;
using System.Web.UI;
using JumboECMS.DBUtility;
using JumboECMS.Utils;

namespace JumboECMS.DAL
{
    public class Module_videoDAL : Module_newsDAL
    {
        public Module_videoDAL()
        {
        }
        public override void CreateContent(string _moduletype, string _contentid, int _page)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [VideoUrl],[FirstPage] FROM [jcms_module_" + _moduletype + "] WHERE [Id]=" + _contentid;
                DataTable dtContent = _doh.GetDataTable();
                //视频地址分割处理
                string VideoUrl = dtContent.Rows[0]["VideoUrl"].ToString().Replace("\r\n", "\r");
                string ContentFirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                dtContent.Clear();
                dtContent.Dispose();
                if (VideoUrl != "")
                {
                    string[] VideoUrlArr = VideoUrl.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);
                    int pageCount = VideoUrlArr.Length;
                    if (ContentFirstPage.Length == 0)
                    {
                        _doh.Reset();
                        _doh.SqlCmd = "UPDATE [jcms_module_" + _moduletype + "] SET [FirstPage]='" + Go2View(_moduletype, _contentid, 1) + "' WHERE [IsPass]=1 and [Id]=" + _contentid;
                        _doh.ExecuteSqlNonQuery();
                    }
                    for (int j = 1; j < (pageCount + 1); j++)
                    {
                        JumboECMS.Utils.DirFile.SaveFile(GetContent(_moduletype, _contentid, j), Go2View(_moduletype, _contentid, j));
                    }
                }
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
                string _FirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
                p__GetPage(te, dtContent, ref PageStr, ref ContentList, 0);
                te.ReplaceContentTag("video", ref PageStr, _contentid);
                te.ReplaceContentLoopTag(ref PageStr);//主要解决通过tags关联
                ContentList.Add(PageStr);
                p__replaceSingleVideo(dtContent, ref _page, ref PageStr, ref ContentList);
                int _TotalPage = Convert.ToInt16(ContentList[1].ToString());//总页数
                dtContent.Clear();
                dtContent.Dispose();

                string _PrevLink = _page == 1 ? "#" : Go2View(_moduletype, _contentid, _page - 1);
                string _NextLink = _page == _TotalPage ? "#" : Go2View(_moduletype, _contentid, _page + 1);
                string _html = ContentList[0].ToString();
                string[] ThisVideoUrl = ContentList[2].ToString().Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                //string CurrentVideoUrl = ThisVideoUrl[ThisVideoUrl.Length - 1];
                string CurrentVideoUrl = site.Dir + "plus/video_play.aspx?Id=" + _contentid + "&NO=" + (_page - 1);
                string CurrentVideoTitle = ThisVideoUrl.Length == 1 ? "" : ThisVideoUrl[0];
                return _html
                    .Replace("{$CurrentPage}", _page.ToString())
                    .Replace("{$TotalPage}", ContentList[1].ToString())
                    .Replace("{$CurrentVideoUrl}", CurrentVideoUrl)
                    .Replace("{$CurrentVideoTitle}", CurrentVideoTitle)
                    .Replace("{$SlideJSON}", ContentList[3].ToString())
                    .Replace("{$PrevLink}", _PrevLink)
                    .Replace("{$NextLink}", _NextLink);
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

        private void p__replaceSingleVideo(DataTable dt, ref int _CurrentPage, ref string PageStr, ref System.Collections.ArrayList ContentList)
        {
            //视频地址分割处理
            string VideoUrl = dt.Rows[0]["VideoUrl"].ToString().Replace("\r\n", "\r");
            string[] VideoUrlArr = VideoUrl.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);
            ContentList.Add(VideoUrlArr.Length);
            if (_CurrentPage < 1 || _CurrentPage > VideoUrlArr.Length)
                _CurrentPage = 1;
            ContentList.Add(VideoUrlArr[_CurrentPage - 1]);
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < VideoUrlArr.Length; i++)
            {
                string[] ThisVideoUrl = VideoUrlArr[i].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                string vFileName = ThisVideoUrl[ThisVideoUrl.Length - 1];
                string thumbnailImage = vFileName.Substring(0, vFileName.Length - 4) + "_thumbs.jpg";
                string title = ThisVideoUrl.Length == 1 ? "" : ThisVideoUrl[0];
                if (i > 0)
                    jsonBuilder.Append(",");
                jsonBuilder.Append("{");
                jsonBuilder.Append("no:" + (i + 1) + ",");
                jsonBuilder.Append("img: '" + thumbnailImage + "',");
                jsonBuilder.Append("link: '" + dt.Rows[0]["FirstPage"].ToString() + "',");
                jsonBuilder.Append("title: '" + title + "'");
                jsonBuilder.Append("}");
            }
            jsonBuilder.Append("]");
            ContentList.Add(jsonBuilder.ToString());
        }
    }
}
