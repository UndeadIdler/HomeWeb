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
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Web;
using JumboECMS.Utils;
using JumboECMS.Entity;
using JumboECMS.DBUtility;

namespace JumboECMS.DAL
{
    /// <summary>
    /// 生成html主文件
    /// </summary>
    public class TemplateEngineDAL : Common
    {
        public string MainModuleType = "";
        public string ThisModuleType = "";
        public TemplateEngineDAL()
        {
            base.SetupSystemDate();
        }
        public JumboECMS.Entity.Normal_Module ThisModule;//模块实体
        private string _pagetitle, _pagekeywords, _pagedescription, _pagenav;
        private bool m_isHtml;

        /// <summary>
        /// 页面默认标题
        /// </summary>
        public string PageTitle
        {
            get { return this._pagetitle; }
            set { this._pagetitle = value; }
        }
        /// <summary>
        /// 页面默认关键字
        /// </summary>
        public string PageKeywords
        {
            get { return this._pagekeywords; }
            set { this._pagekeywords = value; }
        }
        /// <summary>
        /// 页面默认简介
        /// </summary>
        public string PageDescription
        {
            get { return this._pagedescription; }
            set { this._pagedescription = value; }
        }
        /// <summary>
        /// 页面链接导航
        /// </summary>
        public string PageNav
        {
            get { return this._pagenav; }
            set { this._pagenav = value; }
        }
        /// <summary>
        /// 是否缓存页面
        /// </summary>
        public bool IsHtml
        {
            get { return this.m_isHtml; }
            set { this.m_isHtml = value; }
        }

        /// <summary>
        /// 判断最终页面是否静态
        /// </summary>
        /// <returns></returns>
        public bool PageIsHtml()
        {
            return true;
        }
        /// <summary>
        /// 获得栏目导航title
        /// </summary>
        /// <param name="_categoryid"></param>
        /// <returns></returns>
        public string GetCategoryNavigateTitle(string _categoryid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT ID,Title,ParentId,[Code],[IsTop] FROM [" + base.CategoryTable + "] WHERE [Id]=" + _categoryid;
                DataTable dtCategory = _doh.GetDataTable();
                string ParentId = dtCategory.Rows[0]["ParentId"].ToString();
                string CategoryName = dtCategory.Rows[0]["Title"].ToString();
                string CategoryIsTop = dtCategory.Rows[0]["IsTop"].ToString();
                dtCategory.Clear();
                dtCategory.Dispose();
                string MyCategoryTitle = "";
                if (CategoryIsTop == "0")
                    MyCategoryTitle = "";
                else
                    MyCategoryTitle = CategoryName;
                if (ParentId == "0")
                    return MyCategoryTitle;
                else
                    return MyCategoryTitle + "_" + GetCategoryNavigateTitle(ParentId);
            }
        }
        /// <summary>
        /// 获得栏目导航html
        /// </summary>
        /// <param name="_categoryid"></param>
        /// <returns></returns>
        public string GetCategoryNavigateHTML(string _categoryid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT ID,Title,ParentId,[Code],[IsTop],[LanguageCode] FROM [" + base.CategoryTable + "] WHERE [Id]=" + _categoryid;
                DataTable dtCategory = _doh.GetDataTable();
                string ParentId = dtCategory.Rows[0]["ParentId"].ToString();
                string CategoryName = dtCategory.Rows[0]["Title"].ToString();
                string CategoryIsTop = dtCategory.Rows[0]["IsTop"].ToString();
                string CategoryLanguageCode = dtCategory.Rows[0]["LanguageCode"].ToString();
                Dictionary<string, object> lng = new JumboECMS.DAL.LanguageDAL().GetEntity(CategoryLanguageCode);
                dtCategory.Clear();
                dtCategory.Dispose();
                string MyCategoryPath = "";
                if (CategoryIsTop == "0")
                    MyCategoryPath = "";
                else
                {
                    if (ParentId == "0")
                        MyCategoryPath = "<a href=\"" + Go2Category(_categoryid, 1) + "\">" + (string)lng["home"] + "</a>";
                    else
                        MyCategoryPath = "&nbsp;&raquo;&nbsp;<a href=\"" + Go2Category(_categoryid, 1) + "\">" + CategoryName + "</a>";
                }
                if (ParentId == "0")
                    return MyCategoryPath;
                else
                    return GetCategoryNavigateHTML(ParentId) + MyCategoryPath;
            }
        }
        /// <summary>
        /// 找上下文
        /// </summary>
        /// <param name="moduleType"></param>
        /// <param name="categoryId"></param>
        /// <param name="contentId"></param>
        /// <param name="type">0代表上文，1代表下文</param>
        /// <returns></returns>
        private string p__getNeightor(string moduleType, string categoryId, string contentId, int type)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                StringBuilder sb = new StringBuilder();
                _doh.Reset();
                if (type == 0)
                    _doh.SqlCmd = "SELECT TOP 1 [Id],[Title],[FirstPage] FROM [jcms_module_" + moduleType + "] WHERE [IsPass]=1 AND [CategoryId]=" + categoryId + " AND [Id]<" + contentId + " order By [Id] DESC";
                else
                    _doh.SqlCmd = "SELECT TOP 1 [Id],[Title],[FirstPage] FROM [jcms_module_" + moduleType + "] WHERE [IsPass]=1 AND [CategoryId]=" + categoryId + " AND [Id]>" + contentId + "  order By [Id] ASC";
                DataTable dtContent = _doh.GetDataTable();
                if (dtContent.Rows.Count > 0)
                    sb.Append("<a title=\"" + dtContent.Rows[0]["Title"].ToString() + "\" href=\"" + dtContent.Rows[0]["FirstPage"].ToString() + "\">" + dtContent.Rows[0]["Title"].ToString() + "</a>");
                else
                    sb.Append("");
                dtContent.Clear();
                dtContent.Dispose();
                return sb.ToString();
            }

        }
        /// <summary>
        /// 判断内容阅读权限(频道ID只能从外部传入，不支持跨频道)
        /// 假设内容ID和栏目ID都已经正确
        /// </summary>
        /// <param name="_contentid"></param>
        /// <param name="_classid"></param>
        /// <returns></returns>
        public bool CanReadContent(string _moduletype, string _contentid, string _categoryid)
        {
            if (Cookie.GetValue(site.CookiePrev + "admin") != null)//管理员直接可以看
                return true;
            int _usergroup = 0;
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
                _usergroup = Str2Int(Cookie.GetValue(site.CookiePrev + "user", "groupid"));
            int _ContentReadGroup, _CategoryReadGroup;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=" + _contentid;
                _ContentReadGroup = Str2Int(_doh.GetField("jcms_module_" + _moduletype, "ReadGroup").ToString());
                _doh.Reset();
                _doh.ConditionExpress = "id=" + _categoryid;
                _CategoryReadGroup = Str2Int(_doh.GetField(base.CategoryTable, "ReadGroup").ToString());
            }
            if (_ContentReadGroup > -1)//说明不是继承栏目
            {
                if (_ContentReadGroup > _usergroup)
                    return false;
                else
                    return true;
            }
            else
            {
                if (_CategoryReadGroup > _usergroup)
                    return false;
                else
                    return true;
            }
        }
        public void ReplaceLinkLoopTag(ref string _pagestr)
        {
            replaceTag_LinkLoop(ref _pagestr);
        }
        /// <summary>
        /// 替换栏目标签
        /// </summary>
        /// <param name="_pagestr"></param>
        public void ReplaceCategoryTag(ref string _pagestr, string _ClassId)
        {
            executeTag_Category(ref _pagestr, _ClassId);

        }
        /// <summary>
        /// 替换单页内容标签
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <param name="_pagestr"></param>
        /// <param name="_contentid"></param>
        public void ReplaceContentTag(string _moduletype, ref string _pagestr, string _contentid)
        {
            executeTag_Content(_moduletype, ref _pagestr, _contentid);
        }
        /// <summary>
        /// 解析栏目循环标签
        /// </summary>
        /// <param name="_pagestr">原始内容</param>
        /// <returns></returns>
        public void ReplaceCategoryLoopTag(ref string _pagestr)
        {
            replaceTag_CategoryLoop(ref _pagestr);
            replaceTag_Category2Loop(ref _pagestr);
            replaceTag_CategoryTree(ref _pagestr);//2012-04-05新增标签
        }
        /// <summary>
        /// 解析内容循环标签
        /// </summary>
        /// <param name="_pagestr">原始内容</param>
        /// <returns></returns>
        public void ReplaceContentLoopTag(ref string _pagestr)
        {
            replaceTag_ContentLoop(ref _pagestr);
        }
        /// <summary>
        /// 解析shtml标签
        /// </summary>
        /// <param name="_pagestr"></param>
        public void ReplaceShtmlTag(ref string _pagestr)
        {
            replaceTag_Shtml(ref _pagestr);
        }
        /// <summary>
        /// 解析站点信息
        /// </summary>
        /// <param name="_pagestr">原始内容</param>
        /// <returns></returns>
        public void ReplaceSiteTags(ref string _pagestr)
        {
            replaceTag_Include(ref _pagestr);
            replaceTag_SiteConfig(ref _pagestr);
            replaceTag_GetRemoteWeb(ref _pagestr);
            replaceTag_LinkLoop(ref _pagestr);
        }
        /// <summary>
        /// 解析公共标签
        /// </summary>
        /// <param name="_pagestr">原始内容</param>
        /// <param name="pId">模板组ID</param>
        /// <returns></returns>
        public void ReplacePublicTag(ref string _pagestr)
        {
            replaceTag_Include(ref _pagestr);
            replaceTag_SiteConfig(ref _pagestr);
            replaceTag_GetRemoteWeb(ref _pagestr);
            replaceTag_LinkLoop(ref _pagestr);
        }
        /// <summary>
        /// 替换html包含标签(解析次序：2)
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_Include(ref string _pagestr)
        {
            string RegexString = "<jcms:include (?<tagcontent>.*?) />";
            string[] _tagcontent = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                string _tagfile = string.Empty;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<jcms:include " + _tagcontent[i] + " />";
                    _tagfile = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "file");
                    if (!_tagfile.StartsWith("/") && !_tagfile.StartsWith("~/"))
                        _tagfile = site.Dir + "_data/html/" + _tagfile;
                    if (JumboECMS.Utils.DirFile.FileExists(_tagfile))
                        _replacestr = JumboECMS.Utils.DirFile.ReadFile(_tagfile);
                    else
                        _replacestr = "";
                    _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                }
            }
        }
        /// <summary>
        /// 替换shtml包含标签
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_Shtml(ref string _pagestr)
        {
            string RegexString = "<!--#include (?<tagcontent>.*?) -->";
            string[] _tagcontent = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                string _tagfile = string.Empty;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<!--#include " + _tagcontent[i] + " -->";
                    _tagfile = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "virtual");
                    if (JumboECMS.Utils.DirFile.FileExists(_tagfile))
                        _replacestr = JumboECMS.Utils.DirFile.ReadFile(_tagfile);
                    else
                        _replacestr = "";
                    _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                }
            }
        }
        /// <summary>
        /// 替换公共标签(解析次序：3)
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_SiteConfig(ref string _pagestr)
        {
            _pagestr = _pagestr.Replace("{site.Dir}", site.Dir);
            _pagestr = _pagestr.Replace("{site.Name1}", site.Name1);
            _pagestr = _pagestr.Replace("{site.Name2}", site.Name2);
            _pagestr = _pagestr.Replace("{site.Url}", site.Url);
            _pagestr = _pagestr.Replace("{site.ICP1}", site.ICP1);
            _pagestr = _pagestr.Replace("{site.ICP2}", site.ICP2);
            _pagestr = _pagestr.Replace("{site.Home}", Go2Site());
            _pagestr = _pagestr.Replace("{site.Version}", site.Version);
            _pagestr = _pagestr.Replace("{this.page.Nav}", this.PageNav);
            _pagestr = _pagestr.Replace("{this.page.Title}", this.PageTitle);
            _pagestr = _pagestr.Replace("{this.page.Keywords}", this.PageKeywords);
            _pagestr = _pagestr.Replace("{this.page.Description}", this.PageDescription);
            replaceTag_NoShow(ref _pagestr);
        }
        /// <summary>
        /// 替换远程网页内容(解析次序：3)
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_GetRemoteWeb(ref string _pagestr)
        {
            string RegexString = "<jcms:remoteweb (?<tagcontent>.*?) />";
            string[] _tagcontent = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                string _tagurl = string.Empty;
                string _tagcharset = string.Empty;
                System.Text.Encoding encodeType = System.Text.Encoding.Default;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<jcms:remoteweb " + _tagcontent[i] + " />";
                    _tagurl = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "url");
                    _tagcharset = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "charset").ToLower();
                    switch (_tagcharset)
                    {
                        case "unicode":
                            encodeType = System.Text.Encoding.Unicode;
                            break;
                        case "utf-8":
                            encodeType = System.Text.Encoding.UTF8;
                            break;
                        case "gb2312":
                            encodeType = System.Text.Encoding.GetEncoding("GB2312");
                            break;
                        case "gbk":
                            encodeType = System.Text.Encoding.GetEncoding("GB2312");
                            break;
                        default:
                            encodeType = System.Text.Encoding.Default;
                            break;
                    }
                    JumboECMS.Common.NewsCollection nc = new JumboECMS.Common.NewsCollection();
                    _replacestr = nc.GetHttpPage(_tagurl, 8000, encodeType);
                    _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                }
            }
        }
        /// <summary>
        /// 替换注释标签(解析次序：3)
        /// </summary>
        /// <param name="_pagestr">已取到的模板内容</param>
        private void replaceTag_NoShow(ref string _pagestr)
        {
            System.Collections.ArrayList TagArray = JumboECMS.Utils.Strings.GetHtmls(_pagestr, "<!--~", "~-->", false, false);
            if (TagArray.Count > 0)//标签存在
            {
                string TempStr = string.Empty;
                string ReplaceStr;
                for (int i = 0; i < TagArray.Count; i++)
                {
                    TempStr = "<!--~" + TagArray[i].ToString() + "~-->";
                    ReplaceStr = "";
                    _pagestr = _pagestr.Replace(TempStr, ReplaceStr);
                }
            }
        }
        /// <summary>
        /// 替换链接标签
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_LinkLoop(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:linkloop (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:linkloop>";
                string[] _tagcontent = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tagrepeatnum = string.Empty, _taglinktype = string.Empty, _tagwherestr = string.Empty;
                    for (int i = 0; i < _tagcontent.Length; i++)
                    {
                        _loopbody = "<jcms:linkloop " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:linkloop>";
                        _tagrepeatnum = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "repeatnum");
                        _taglinktype = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "linktype");
                        _tagwherestr = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "wherestr");
                        if (_tagrepeatnum == "") _tagrepeatnum = "0";
                        string pStr = " * FROM [jcms_normal_link] WHERE 1=1";
                        string oStr = " ORDER BY OrderNum desc";
                        _doh.Reset();
                        if (_tagrepeatnum != "0")
                            pStr = " top " + _tagrepeatnum + pStr;
                        if (_taglinktype != "" && _taglinktype != "0")
                            pStr += " AND [LinkType]=" + _taglinktype;
                        if (_tagwherestr != "")
                            pStr += " AND " + _tagwherestr.Replace("小于", "<").Replace("大于", ">").Replace("不等于", "<>");
                        _doh.SqlCmd = "select" + pStr + oStr;
                        DataTable _dt = _doh.GetDataTable();
                        StringBuilder sb = new StringBuilder();
                        List<Normal_Link> links = (new Normal_Links()).DT2List(_dt);
                        string _TemplateContent = _tempstr[i];
                        JumboECMS.TEngine.TemplateManager manager = JumboECMS.TEngine.TemplateManager.FromString(_TemplateContent);

                        manager.SetValue("links", links);
                        string _content = manager.Process();
                        _pagestr = _pagestr.Replace(_loopbody, _content);
                        _dt.Clear();
                        _dt.Dispose();
                    }
                }
            }

        }
        /// <summary>
        /// 替换频道栏目循环标签
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_CategoryLoop(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:categoryloop (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:categoryloop>";
                string[] _tagcontent = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tagrepeatnum, _tagselectids, _tagdepth, _tagparentid, _tagwherestr, _tagorderfield, _tagordertype, _hascontent = string.Empty;
                    for (int i = 0; i < _tagcontent.Length; i++)
                    {
                        _loopbody = "<jcms:categoryloop " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:categoryloop>";
                        _tagrepeatnum = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "repeatnum");
                        _tagselectids = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "selectids");
                        _tagdepth = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "depth");
                        _tagparentid = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "parentid");
                        _tagwherestr = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "wherestr");
                        _tagorderfield = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "orderfield");
                        if (_tagorderfield == "") _tagorderfield = "code";
                        _tagordertype = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "ordertype");
                        if (_tagordertype == "") _tagordertype = "asc";
                        if (_tagrepeatnum == "") _tagrepeatnum = "0";
                        _hascontent = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "hascontent");
                        if (_hascontent == "") _hascontent = "0";
                        if (_tagdepth == "") _tagdepth = "0";
                        string pStr = " [Id],[Title],[Info],[TopicNum],[Code] FROM [" + base.CategoryTable + "] WHERE 1=1";
                        string oStr = " ORDER BY code asc";
                        if (_tagorderfield.ToLower() != "code")
                            oStr = " ORDER BY " + _tagorderfield + " " + _tagordertype + ",code asc";
                        else
                            oStr = " ORDER BY " + _tagorderfield + " " + _tagordertype;
                        _doh.Reset();
                        if (_tagdepth != "-1" && _tagdepth != "0")
                            pStr += " AND Len(Code)=" + (Str2Int(_tagdepth, 0) * 4);
                        if (_tagrepeatnum != "0")
                            pStr = " top " + _tagrepeatnum + pStr;
                        if (_tagparentid != "" && _tagparentid != "0")
                            pStr += " AND [ParentId]=" + _tagparentid;
                        if (_hascontent == "1")
                            pStr += " AND [TopicNum]>0";
                        if (_tagwherestr != "")
                            pStr += " AND " + _tagwherestr.Replace("小于", "<").Replace("大于", ">").Replace("不等于", "<>");
                        if (_tagselectids != "")
                            pStr += " AND [id] IN (" + _tagselectids.Replace("|", ",") + ")";

                        _doh.SqlCmd = "select" + pStr + oStr;
                        DataTable _dt = _doh.GetDataTable();
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < _dt.Rows.Count; j++)
                        {
                            if (_tagdepth == "-1")
                            {
                                _doh.Reset();
                                _doh.ConditionExpress = " ParentId]=" + _dt.Rows[j]["Id"].ToString();
                                int countNum = _doh.Count(base.CategoryTable);
                                if (countNum > 1)//表示非末级栏目，直接跳过
                                    continue;
                            }
                            _viewstr = _tempstr[i];
                            _viewstr = _viewstr.Replace("{$CategoryNO}", (j + 1).ToString());
                            executeTag_Category(ref _viewstr, _dt.Rows[j]["Id"].ToString());
                            sb.Append(_viewstr);
                        }
                        _pagestr = _pagestr.Replace(_loopbody, sb.ToString());
                        _dt.Clear();
                        _dt.Dispose();
                    }
                }
            }

        }
        /// <summary>
        /// 替换频道栏目循环标签
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_Category2Loop(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:category2loop (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:category2loop>";
                string[] _tagcontent = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tagrepeatnum, _tagselectids, _tagdepth, _tagparentid, _tagwherestr, _tagorderfield, _tagordertype, _hascontent = string.Empty;
                    for (int i = 0; i < _tagcontent.Length; i++)
                    {
                        _loopbody = "<jcms:category2loop " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:category2loop>";
                        _tagrepeatnum = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "repeatnum");
                        _tagselectids = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "selectids");
                        _tagdepth = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "depth");
                        _tagparentid = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "parentid");
                        _tagwherestr = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "wherestr");
                        _tagorderfield = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "orderfield");
                        if (_tagorderfield == "") _tagorderfield = "code";
                        _tagordertype = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "ordertype");
                        if (_tagordertype == "") _tagordertype = "asc";
                        if (_tagrepeatnum == "") _tagrepeatnum = "0";
                        _hascontent = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "hascontent");
                        if (_hascontent == "") _hascontent = "0";
                        if (_tagdepth == "") _tagdepth = "0";
                        string pStr = " [Id],[Title],[Info],[TopicNum],[Code] FROM [" + base.CategoryTable + "] WHERE 1=1";
                        string oStr = " ORDER BY code asc";
                        if (_tagorderfield.ToLower() != "code")
                            oStr = " ORDER BY " + _tagorderfield + " " + _tagordertype + ",code asc";
                        else
                            oStr = " ORDER BY " + _tagorderfield + " " + _tagordertype;
                        _doh.Reset();
                        if (_tagdepth != "-1" && _tagdepth != "0")
                            pStr += " AND Len(Code)=" + (Str2Int(_tagdepth, 0) * 4);
                        if (_tagrepeatnum != "0")
                            pStr = " top " + _tagrepeatnum + pStr;
                        if (_tagparentid != "" && _tagparentid != "0")
                            pStr += " AND [ParentId]=" + _tagparentid;
                        if (_hascontent == "1")
                            pStr += " AND [TopicNum]>0";
                        if (_tagwherestr != "")
                            pStr += " AND " + _tagwherestr.Replace("小于", "<").Replace("大于", ">").Replace("不等于", "<>");
                        if (_tagselectids != "")
                            pStr += " AND [id] IN (" + _tagselectids.Replace("|", ",") + ")";

                        _doh.SqlCmd = "select" + pStr + oStr;
                        DataTable _dt = _doh.GetDataTable();
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < _dt.Rows.Count; j++)
                        {
                            if (_tagdepth == "-1")
                            {
                                _doh.Reset();
                                _doh.ConditionExpress = " [ParentId]=" + _dt.Rows[j]["Id"].ToString();
                                int countNum = _doh.Count(base.CategoryTable);
                                if (countNum > 1)//表示非末级栏目，直接跳过
                                    continue;
                            }
                            _viewstr = _tempstr[i];
                            _viewstr = _viewstr.Replace("{$Category2NO}", (j + 1).ToString());
                            executeTag_Class2(ref _viewstr, _dt.Rows[j]["Id"].ToString());
                            sb.Append(_viewstr);
                        }
                        _pagestr = _pagestr.Replace(_loopbody, sb.ToString());
                        _dt.Clear();
                        _dt.Dispose();
                    }
                }
            }

        }
        /// <summary>
        /// 解析栏目树标签(2012-04-05新增标签)
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_CategoryTree(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:categorytree (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:categorytree>";
                string[] _tagcontent = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tagcategoryid = string.Empty;
                    bool _tagincludechild = false;
                    for (int i = 0; i < _tagcontent.Length; i++)
                    {
                        _loopbody = "<jcms:categorytree " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:categorytree>";
                        _tagcategoryid = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "categoryid");
                        if (_tagcategoryid == "") _tagcategoryid = "0";
                        _tagincludechild = (JumboECMS.Utils.Strings.AttributeValue(_tagcontent[i], "includechild") != "0");

                        string _TemplateContent = _tempstr[i];
                        JumboECMS.TEngine.TemplateManager manager = JumboECMS.TEngine.TemplateManager.FromString(_TemplateContent);
                        manager.SetValue("tree", (new JumboECMS.DAL.Normal_CategoryDAL().GetCategoryTree(_tagcategoryid, _tagincludechild)));
                        _replacestr = manager.Process();
                        _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                    }
                }
            }

        }
        /// <summary>
        /// 替换内容循环标签
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_ContentLoop(ref string _pagestr)
        {
            string RegexString = "<jcms:contentloop (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:contentloop>";
            string[] _tagcontent = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            string[] _tempstr = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<jcms:contentloop " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:contentloop>";
                    _replacestr = getContentList_RL(_tagcontent[i], _tempstr[i]);
                    _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                }
            }
        }
        /// <summary>
        /// 提取列表供列表标签使用
        /// </summary>
        /// <param name="Parameter"></param>

        /// <returns></returns>
        private string getContentList_RL(string _tagcontent, string _tempstr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _viewstr = string.Empty;
                string _tagrepeatnum = JumboECMS.Utils.Strings.AttributeValue(_tagcontent, "repeatnum");
                if (_tagrepeatnum == "") _tagrepeatnum = "10";
                string _tagmodule = JumboECMS.Utils.Strings.AttributeValue(_tagcontent, "module");
                if (_tagmodule == "") _tagmodule = "news";
                string _tagcategoryid = JumboECMS.Utils.Strings.AttributeValue(_tagcontent, "categoryid");
                if (_tagcategoryid == "") _tagcategoryid = "0";
                string _tagfields = JumboECMS.Utils.Strings.AttributeValue(_tagcontent, "fields");
                string _tagorderfield = JumboECMS.Utils.Strings.AttributeValue(_tagcontent, "orderfield");
                if (_tagorderfield == "") _tagorderfield = "adddate";
                string _tagordertype = JumboECMS.Utils.Strings.AttributeValue(_tagcontent, "ordertype");
                if (_tagordertype == "") _tagordertype = "desc";
                string _tagistop = JumboECMS.Utils.Strings.AttributeValue(_tagcontent, "istop");
                if (_tagistop == "") _tagistop = "0";
                string _tagisfocus = JumboECMS.Utils.Strings.AttributeValue(_tagcontent, "isfocus");
                if (_tagisfocus == "") _tagisfocus = "0";
                string _tagisimg = JumboECMS.Utils.Strings.AttributeValue(_tagcontent, "isimg");
                if (_tagisimg == "") _tagisimg = "0";
                string _tagtimerange = JumboECMS.Utils.Strings.AttributeValue(_tagcontent, "timerange");
                string _tagexceptids = JumboECMS.Utils.Strings.AttributeValue(_tagcontent, "exceptids");
                string _tagwherestr = JumboECMS.Utils.Strings.AttributeValue(_tagcontent, "wherestr");
                string _tagislike = JumboECMS.Utils.Strings.AttributeValue(_tagcontent, "islike");
                string _tagkeywords = JumboECMS.Utils.Strings.AttributeValue(_tagcontent, "keywords");
                if (_tagcategoryid != "0")
                    executeTag_Category(ref _tempstr, _tagcategoryid);

                string sql = "SELECT TOP " + _tagrepeatnum + " [Id],[CategoryId],[FirstPage]," + _tagfields + " FROM [jcms_module_" + _tagmodule + "] WHERE ([IsPass]=1";

                if (_tagcategoryid != "0")
                    sql += " And [CategoryId] in (SELECT ID FROM [" + base.CategoryTable + "] WHERE [Code] Like (SELECT Code FROM [" + base.CategoryTable + "] WHERE [Id]=" + _tagcategoryid + ")+'%')";
                if (_tagistop == "1")
                    sql += " And [IsTop]=1";
                else if (_tagistop == "-1")
                    sql += " And [IsTop]=0";
                if (_tagisfocus == "1")
                    sql += " And [IsFocus]=1";
                else if (_tagisfocus == "-1")
                    sql += " And [IsFocus]=0";
                if (DBType == "0")
                {
                    switch (_tagtimerange)
                    {
                        case "1d":
                            sql += " AND datediff('d',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1w":
                            sql += " AND datediff('ww',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            sql += " AND datediff('m',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1y":
                            sql += " AND AddDate>=#" + (DateTime.Now.Year + "-1-1") + "#";
                            break;
                    }
                }
                else
                {
                    switch (_tagtimerange)
                    {
                        case "1d":
                            sql += " AND datediff(d,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1w":
                            sql += " AND datediff(ww,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            sql += " AND datediff(m,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1y":
                            sql += " AND AddDate>='" + (DateTime.Now.Year + "-1-1") + "'";
                            break;
                    }
                }
                if (_tagisimg == "1")
                    sql += " And [IsImg]=1 And (right(Img,4)='.jpg' Or right(Img,4)='.gif')";
                else if (_tagisimg == "-1")
                    sql += " And [IsImg]=0";
                if (_tagwherestr != "")
                    sql += " AND " + _tagwherestr.Replace("小于", "<").Replace("大于", ">").Replace("不等于", "<>");
                if (_tagexceptids != "")
                    sql += " AND id not in(" + _tagexceptids + ")";
                if (_tagislike == "1")
                {
                    if (_tagkeywords == "") _tagkeywords = "将博";
                    _tagkeywords = _tagkeywords.Replace(",", " ").Replace("、", " ");
                    string[] key = _tagkeywords.Split(new string[] { " " }, StringSplitOptions.None);
                    string _joinstr = " AND (";
                    for (int i = 0; i < key.Length; i++)
                    {
                        if (key[i].Length > 1)
                        {
                            if (i == 0)
                                _joinstr += "[Tags] LIKE '%" + key[i].Trim() + "%'";
                            else
                                _joinstr += " OR [Tags] LIKE '%" + key[i].Trim() + "%'";
                        }
                    }
                    _joinstr += ")";
                    sql += _joinstr;
                }
                if (_tagorderfield.ToLower() != "rnd")
                {
                    if (_tagorderfield.ToLower() != "adddate")
                        sql += ") ORDER BY " + _tagorderfield + " " + _tagordertype + ",adddate Desc,id Desc";
                    else
                        sql += ") ORDER BY " + _tagorderfield + " " + _tagordertype + ",id Desc"; ;
                }
                else
                {
                    sql += ")" + ORDER_BY_RND();
                }
                _doh.Reset();
                _doh.SqlCmd = sql;
                DataTable dt = _doh.GetDataTable();
                string ReplaceStr = operateContentTag(_tagmodule, dt, _tempstr);
                ReplaceStr = ReplaceStr.Replace("{$ContentCount}", dt.Rows.Count.ToString());
                dt.Clear();
                dt.Dispose();
                return ReplaceStr;

            }

        }
        /// <summary>
        /// 解析栏目标签1
        /// </summary>
        /// <param name="_pagestr"></param>
        /// <param name="_categoryid"></param>
        private void executeTag_Category(ref string _pagestr, string _categoryid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Id],[Title],[Info],[Img],[Keywords],[TopicNum],[Code],len(code) as len,[ParentId],[Content] FROM [" + base.CategoryTable + "] WHERE [Id]=" + _categoryid;
                DataTable _dt = _doh.GetDataTable();
                if (_dt.Rows.Count > 0)
                {
                    string _parentid = _dt.Rows[0]["ParentId"].ToString();
                    _pagestr = _pagestr.Replace("{$CategoryId}", _dt.Rows[0]["Id"].ToString());
                    _pagestr = _pagestr.Replace("{$CategoryName}", _dt.Rows[0]["Title"].ToString());
                    _pagestr = _pagestr.Replace("{$CategoryInfo}", _dt.Rows[0]["Info"].ToString());
                    _pagestr = _pagestr.Replace("{$CategoryKeywords}", _dt.Rows[0]["Keywords"].ToString());
                    _pagestr = _pagestr.Replace("{$CategoryImg}", _dt.Rows[0]["Img"].ToString());
                    _pagestr = _pagestr.Replace("{$CategoryTopicNum}", _dt.Rows[0]["TopicNum"].ToString());
                    _pagestr = _pagestr.Replace("{$CategoryLink}", Go2Category(_categoryid, 1));
                    _pagestr = _pagestr.Replace("{$CategoryCode}", _dt.Rows[0]["Code"].ToString());
                    _pagestr = _pagestr.Replace("{$CategoryDepth}", (Str2Int(_dt.Rows[0]["Len"].ToString()) / 4).ToString());
                    _pagestr = _pagestr.Replace("{$CategoryParentId}", _dt.Rows[0]["ParentId"].ToString());
                    _pagestr = _pagestr.Replace("{$CategoryContent}", _dt.Rows[0]["Content"].ToString());
                }
                _dt.Clear();
                _dt.Dispose();
            }
        }
        /// <summary>
        /// 解析栏目标签2
        /// </summary>
        /// <param name="_pagestr"></param>
        /// <param name="_categoryid"></param>
        private void executeTag_Class2(ref string _pagestr, string _categoryid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Id],[Title],[Info],[Img],[TopicNum],[Code],len(code) as len,[ParentId] FROM [" + base.CategoryTable + "] WHERE [Id]=" + _categoryid;
                DataTable _dt = _doh.GetDataTable();
                if (_dt.Rows.Count > 0)
                {
                    _pagestr = _pagestr.Replace("{$Category2Id}", _dt.Rows[0]["Id"].ToString());
                    _pagestr = _pagestr.Replace("{$Category2Name}", _dt.Rows[0]["Title"].ToString());
                    _pagestr = _pagestr.Replace("{$Category2Info}", _dt.Rows[0]["Info"].ToString());
                    _pagestr = _pagestr.Replace("{$Category2Img}", _dt.Rows[0]["Img"].ToString());
                    _pagestr = _pagestr.Replace("{$Category2TopicNum}", _dt.Rows[0]["TopicNum"].ToString());
                    _pagestr = _pagestr.Replace("{$Category2Link}", Go2Category(_dt.Rows[0]["Id"].ToString(), 1));
                    _pagestr = _pagestr.Replace("{$Category2Code}", _dt.Rows[0]["Code"].ToString());
                    _pagestr = _pagestr.Replace("{$Category2Depth}", (Str2Int(_dt.Rows[0]["Len"].ToString()) / 4).ToString());
                }
                _dt.Clear();
                _dt.Dispose();
            }
        }

        /// <summary>
        /// 解析单条内容标签
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <param name="_pagestr"></param>
        /// <param name="_contentid"></param>
        private void executeTag_Content(string _moduletype, ref string _pagestr, string _contentid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _randomstr = "1" + RandomStr(4);
                string _tempstr = string.Empty;
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM  [jcms_module_" + _moduletype + "] WHERE [Id]=" + _contentid;
                DataTable dtContent = _doh.GetDataTable();
                if (dtContent.Rows.Count > 0)
                {
                    _tempstr = p__getNeightor(_moduletype, dtContent.Rows[0]["CategoryId"].ToString(), _contentid, 0);
                    _pagestr = _pagestr.Replace("{$_getNeightor(0)}", _tempstr);
                    _tempstr = p__getNeightor(_moduletype, dtContent.Rows[0]["CategoryId"].ToString(), _contentid, 1);
                    _pagestr = _pagestr.Replace("{$_getNeightor(1)}", _tempstr);
                    for (int i = 0; i < dtContent.Columns.Count; i++)
                    {
                        if (dtContent.Rows[0]["IsImg"].ToString() == "0" || dtContent.Rows[0]["Img"].ToString().Length == 0)
                            _pagestr = _pagestr.Replace("{$_img}", site.Dir + "statics/common/nophoto.jpg");
                        else
                            _pagestr = _pagestr.Replace("{$_img}", dtContent.Rows[0]["Img"].ToString());
                        switch (dtContent.Columns[i].ColumnName.ToLower())
                        {
                            case "adddate":
                                _pagestr = _pagestr.Replace("{$_adddate}", Convert.ToDateTime(dtContent.Rows[0]["AddDate"]).ToString("yyyy-MM-dd"));
                                break;
                            case "viewnum":
                                _pagestr = _pagestr.Replace("{$_viewnum}", "<script src=\"" + site.Dir + "plus/viewcount.aspx?mType=" + _moduletype + "&id=" + _contentid + "&addit=1\"></script>");
                                break;
                            case "downnum":
                                _pagestr = _pagestr.Replace("{$_downnum}", "<script src=\"" + site.Dir + "plus/downcount.aspx?mType=" + _moduletype + "&id=" + _contentid + "\"></script>");
                                break;
                            default:
                                _pagestr = _pagestr.Replace("{$_" + dtContent.Columns[i].ColumnName.ToLower() + "}", dtContent.Rows[0][i].ToString());
                                break;
                        }
                    }
                }
                dtContent.Clear();
                dtContent.Dispose();
            }
        }
        #region 生成静态页面
        /// <summary>
        /// 生成首页文件
        /// </summary>

        public bool CreateDefaultFile()
        {
            string _pagestr = GetSiteDefaultPage();
            JumboECMS.Utils.DirFile.SaveFile(_pagestr, "~/" + "index.html");
            return true;
        }
        #endregion
        /// <summary>
        /// 获得首页内容
        /// </summary>
        /// <returns></returns>
        public string GetSiteDefaultPage()
        {
            string _pagestr = string.Empty;
            //得到首页的默认模板：方案组ID/主题ID/模板内容
            JumboECMS.DAL.Normal_TemplateDAL dal = new JumboECMS.DAL.Normal_TemplateDAL();
            dal.GetTemplateContent("0", ref _pagestr);
            this.IsHtml = site.IsHtml;
            this.PageNav = site.Name1;
            this.PageTitle = site.Name1 + " - " + site.Description1;
            this.PageKeywords = site.Keywords1;
            this.PageDescription = site.Description1;
            ReplacePublicTag(ref _pagestr);
            ReplaceCategoryLoopTag(ref _pagestr);
            ReplaceContentLoopTag(ref _pagestr);
            return _pagestr;
        }
        #region 获得栏目页内容
        /// <summary>
        /// 获得栏目页内容
        /// </summary>
        /// <param name="_categoryid"></param>
        /// <param name="_currentpage"></param>
        /// <returns></returns>
        public string GetSiteCategoryPage(string _categoryid, int _currentpage)
        {
            Normal_Category _category = new JumboECMS.DAL.Normal_CategoryDAL().GetEntity(_categoryid, "");
            System.Collections.ArrayList ContentList = getCategoryPage(_category, _currentpage);
            string _pagestr = ContentList[0].ToString();
            if (ContentList.Count > 3)
            {
                string ViewStr = ContentList[1].ToString();
                _pagestr = _pagestr.Replace(ViewStr, ContentList[2].ToString());
                _pagestr = _pagestr.Replace("{$_getPageBar()}", ContentList[3].ToString());
            }
            return _pagestr;
        }
        /// <summary>
        /// 获得栏目页的模板内容
        /// </summary>
        /// <param name="_categoryid"></param>
        /// <returns></returns>
        public string GetSiteCategoryTemplate(string _categoryid)
        {
            Normal_Category _category = new JumboECMS.DAL.Normal_CategoryDAL().GetEntity(_categoryid, "");
            System.Collections.ArrayList ContentList = getCategoryPage(_category, 1);
            return ContentList[0].ToString();
        }
        private System.Collections.ArrayList getCategoryPage(Normal_Category _category, int _currentpage)
        {
            string _pagestr = string.Empty;
            //得到模板内容
            new JumboECMS.DAL.Normal_TemplateDAL().GetTemplateContent(_category.TemplateId, ref _pagestr);
            this.PageNav = this.GetCategoryNavigateHTML(_category.Id);
            this.PageTitle = GetCategoryNavigateTitle(_category.Id);
            if (_category.Keywords == "")
                this.PageKeywords = JumboECMS.Utils.WordSpliter.GetKeyword(_category.Info);
            else
                this.PageKeywords = _category.Keywords;
            if (_category.LanguageCode.ToLower() == "cn")
                this.PageKeywords += "," + site.Keywords1;
            else
                this.PageKeywords += "," + site.Keywords2;
            this.PageDescription = JumboECMS.Utils.Strings.SimpleLineSummary(_category.Info);
            ReplacePublicTag(ref _pagestr);
            _pagestr = _pagestr.Replace("{$CurrentCategoryId}", _category.Id);
            _pagestr = _pagestr.Replace("{$ParentCategoryId}", _category.ParentId.ToString());
            _pagestr = _pagestr.Replace("{$TopCategoryId}", _category.TopId.ToString());
            ReplaceCategoryLoopTag(ref _pagestr);
            ReplaceCategoryTag(ref _pagestr, _category.Id);
            ReplaceContentLoopTag(ref _pagestr);
            System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
            ContentList.Add(_pagestr);
            getCategorySinglePageListBody(_category, ref ContentList, _currentpage);

            return ContentList;
        }
        private void getCategorySinglePageListBody(Normal_Category _category, ref System.Collections.ArrayList ContentList, int _currentpage)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _pagestr = ContentList[0].ToString();//原内容
                string whereStr = " [CategoryId] in (SELECT ID FROM [" + base.CategoryTable + "] WHERE [Code] LIKE '" + _category.Code + "%') AND [IsPass]=1";
                string RegexString = "<jcms:categorycontent (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:categorycontent>";
                string[] _tagcontent = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _loopbody = "<jcms:categorycontent " + _tagcontent[0] + ">" + _tempstr[0] + "</jcms:categorycontent>";
                    string _fields = JumboECMS.Utils.Strings.AttributeValue(_tagcontent[0], "fields");
                    if (!("," + _fields + ",").Contains(",adddate,")) _fields += ",adddate";
                    int _pagesize = Str2Int(JumboECMS.Utils.Strings.AttributeValue(_tagcontent[0], "pagesize"));
                    if (_pagesize == 0) _pagesize = 20;
                    ContentList.Add(_loopbody);
                    _doh.Reset();
                    _doh.ConditionExpress = whereStr;
                    int _totalcount = _doh.Count("jcms_module_" + _category.ModuleType);
                    int _pagecount = JumboECMS.Utils.Int.PageCount(_totalcount, _pagesize);
                    _currentpage = _currentpage == 0 ? 1 : _currentpage;
                    NameValueCollection orders = new NameValueCollection();
                    orders.Add("AddDate", "desc");
                    orders.Add("Id", "desc");
                    string wStr = JumboECMS.Utils.SqlHelp.GetSql1("Id,CategoryId,[IsPass],[FirstPage]," + _fields,
                        "jcms_module_" + _category.ModuleType, _totalcount,
                        _pagesize,
                        _currentpage,
                        orders,
                        whereStr);
                    _doh.Reset();
                    _doh.SqlCmd = wStr;
                    DataTable dtContent = _doh.GetDataTable();
                    ContentList.Add(operateContentTag(_category.ModuleType, dtContent, _tempstr[0]));
                    dtContent.Clear();
                    dtContent.Dispose();
                    //以下是分页代码
                    if (_category.LanguageCode == "en")
                        ContentList.Add(getPageBar(5, "js", 2, _totalcount, _pagesize, _currentpage, Go2Category(_category.Id, 1), Go2Category(_category.Id, -1)));
                    else
                        ContentList.Add(getPageBar(4, "js", 2, _totalcount, _pagesize, _currentpage, Go2Category(_category.Id, 1), Go2Category(_category.Id, -1)));
                }
            }

        }
        #endregion

        /// <summary>
        /// 处理内容标签(频道ID不固定，所以不能直接继承本类channel)
        /// </summary>
        /// <param name="_channeltype">唯一模型 要么article 要么soft</param>
        ///  <param name="_dt">获得的数据表</param>
        /// <param name="_tempstr">循环模版</param>
        /// <returns></returns>
        private string operateContentTag(string _channeltype, DataTable _dt, string _tempstr)
        {
            string _replacestr = _tempstr;
            _replacestr = _replacestr.Replace("$_{title}", "<#formattitle title=\"${field.title}\" />");
            _replacestr = _replacestr.Replace("$_{url}", "${field.firstpage}");
            _replacestr = _replacestr.Replace("$_{img}", "<#imgurl sitedir=\"" + site.Dir + "\"  isimg=\"${field.isimg}\" img=\"${field.img}\" />");
            _replacestr = _replacestr.Replace("$_{classname}", "<#classname moduletype=\"${field.moduletype}\" classid=\"${field.classid}\" />");
            _replacestr = _replacestr.Replace("$_{classlink}", "<#classlink moduletype=\"${field.moduletype}\" classid=\"${field.classid}\" />");
            _replacestr = _replacestr.Replace("$_{viewnum}", "<#viewnum sitedir=\"" + site.Dir + "\" moduletype=\"${field.moduletype}\" contentid=\"${field.id}\" />");

            string _TemplateContent = _replacestr;
            JumboECMS.TEngine.TemplateManager manager = JumboECMS.TEngine.TemplateManager.FromString(_TemplateContent);
            string _content = "";
            manager.RegisterCustomTag("formattitle", new TemplateTag_GetFormatTitle());
            manager.RegisterCustomTag("imgurl", new TemplateTag_GetImgurl());
            manager.RegisterCustomTag("classname", new TemplateTag_GetCategoryName());
            manager.RegisterCustomTag("classlink", new TemplateTag_GetClassLink());
            manager.RegisterCustomTag("cutstring", new TemplateTag_GetCutstring());
            manager.RegisterCustomTag("viewnum", new TemplateTag_GetViewnum());
            switch (_channeltype.ToLower())
            {
                case "photo":
                    manager.SetValue("contents", (new JumboECMS.Entity.Module_Photos()).DT2List(_dt));
                    break;
                case "product":
                    manager.SetValue("contents", (new JumboECMS.Entity.Module_Products()).DT2List(_dt));
                    break;
                case "soft":
                    manager.SetValue("contents", (new JumboECMS.Entity.Module_Downs()).DT2List(_dt));
                    break;
                case "video":
                    manager.SetValue("contents", (new JumboECMS.Entity.Module_Videos()).DT2List(_dt));
                    break;
                case "case":
                    manager.SetValue("contents", (new JumboECMS.Entity.Module_Cases()).DT2List(_dt));
                    break;
                case "job":
                    manager.SetValue("contents", (new JumboECMS.Entity.Module_Jobs()).DT2List(_dt));
                    break;
                default:
                    manager.SetValue("contents", (new JumboECMS.Entity.Module_Newss()).DT2List(_dt));
                    break;
            }
            manager.SetValue("site", site);
            _content = manager.Process();
            return _content;
        }
    }
}