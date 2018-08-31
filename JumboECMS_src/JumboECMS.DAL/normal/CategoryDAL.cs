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
using System.Collections.Generic;
using System.Data;
using System.Web;
using JumboECMS.Utils;
using JumboECMS.DBUtility;

namespace JumboECMS.DAL
{
    /// <summary>
    /// 栏目表信息
    /// </summary>
    public class Normal_CategoryDAL : Common
    {
        public Normal_CategoryDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 获得栏目的单条记录实体
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_wherestr"></param>
        /// <returns></returns>
        public JumboECMS.Entity.Normal_Category GetEntity(string _categoryid, string _wherestr)
        {
            JumboECMS.Entity.Normal_Category _class = new JumboECMS.Entity.Normal_Category();
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [" + base.CategoryTable + "] WHERE [Id]=" + _categoryid;
                if (_wherestr != "") _doh.SqlCmd += " AND " + _wherestr;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    _class.Id = dt.Rows[0]["Id"].ToString();
                    _class.ParentId = Validator.StrToInt(dt.Rows[0]["ParentId"].ToString(), 0);
                    _class.TopId = Validator.StrToInt(dt.Rows[0]["TopId"].ToString(), 0);
                    _class.Title = dt.Rows[0]["Title"].ToString();
                    _class.Info = dt.Rows[0]["Info"].ToString();
                    _class.Keywords = dt.Rows[0]["Keywords"].ToString();
                    _class.Img = dt.Rows[0]["Img"].ToString();
                    _class.FilePath = dt.Rows[0]["FilePath"].ToString();
                    _class.Code = dt.Rows[0]["Code"].ToString();
                    _class.IsTop = Validator.StrToInt(dt.Rows[0]["IsTop"].ToString(), 0) == 1;
                    _class.TopicNum = Validator.StrToInt(dt.Rows[0]["TopicNum"].ToString(), 0);
                    _class.TemplateId = Str2Str(dt.Rows[0]["TemplateId"].ToString());
                    _class.TypeId = Str2Str(dt.Rows[0]["TypeId"].ToString());
                    _class.ContentTemp = Str2Str(dt.Rows[0]["ContentTemp"].ToString());
                    _class.FirstPage = dt.Rows[0]["FirstPage"].ToString();
                    _class.AliasPage = dt.Rows[0]["AliasPage"].ToString();
                    _class.ReadGroup = Validator.StrToInt(dt.Rows[0]["ReadGroup"].ToString(), 0);
                    _class.ModuleType = dt.Rows[0]["ModuleType"].ToString();
                    _class.Content = dt.Rows[0]["Content"].ToString();
                    _class.LanguageCode = dt.Rows[0]["LanguageCode"].ToString();
                }
            }
            return _class;
        }

        /// <summary>
        /// 获得指定栏目内容页数
        /// </summary>
        /// <param name="_categoryid">栏目ID</param>
        /// <param name="_includechild">是否包含子类内容</param>
        /// <returns></returns>
        public int GetContetPageCount(string _categoryid, bool _includechild)
        {
            JumboECMS.Entity.Normal_Category _category = new JumboECMS.DAL.Normal_CategoryDAL().GetEntity(_categoryid, "");
            if (_category.TypeId == "3" || _category.TypeId == "4")
                return 1;
            using (DbOperHandler _doh = new Common().Doh())
            {
                JumboECMS.DAL.TemplateEngineDAL teDAL = new JumboECMS.DAL.TemplateEngineDAL();
                string _pagestr = teDAL.GetSiteCategoryTemplate(_categoryid);
                string RegexString = "<jcms:categorycontent (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:categorycontent>";
                string[] _tagcontent = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboECMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    int _pagesize = Str2Int(JumboECMS.Utils.Strings.AttributeValue(_tagcontent[0], "pagesize"));
                    if (_pagesize == 0) _pagesize = 20;
                    string _wherestr = string.Empty;
                    if (!_includechild)
                        _wherestr = " [CategoryID]=" + _categoryid + " AND [IsPass]=1";
                    else
                        _wherestr = " [CategoryID] in (Select id FROM [" + base.CategoryTable + "] WHERE [Code] LIKE '" + _category.Code + "%') AND [IsPass]=1";
                    _doh.Reset();
                    _doh.ConditionExpress = _wherestr;
                    int _totalcount = _doh.Count("jcms_module_" + _category.ModuleType);
                    return JumboECMS.Utils.Int.PageCount(_totalcount, _pagesize);
                }
                else
                    return 1;
            }
        }
        /// <summary>
        /// 链接到栏目页
        /// </summary>
        /// <param name="_categoryid"></param>
        /// <param name="_page"></param>
        /// <returns></returns>
        public string GetCategoryLink(string _categoryid, int _page)
        {
            JumboECMS.Entity.Normal_Category _Category = new JumboECMS.DAL.Normal_CategoryDAL().GetEntity(_categoryid, "");
            if (_page == 1 && _Category.AliasPage != "")
            {
                return _Category.AliasPage;
            }
            string TempUrl = JumboECMS.Common.PageFormat.Category(site.Dir, _page);
            TempUrl = TempUrl.Replace("<#SiteDir#>", site.Dir);
            TempUrl = TempUrl.Replace("<#CategoryFilePath#>", _Category.FilePath.ToLower());
            TempUrl = TempUrl.Replace("<#id#>", _categoryid);
            if (_page > 0) TempUrl = TempUrl.Replace("*", _page.ToString());
            return TempUrl;
        }
        /// <summary>
        /// 判断是否有下属栏目
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool HasChild(string _categoryid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "parentid=" + _categoryid;
                bool _haschild = (_doh.Exist(base.CategoryTable));
                return _haschild;
            }
        }
        /// <summary>
        /// 获得某个栏目树
        /// </summary>
        /// <param name="_parentid"></param>
        /// <param name="_includechild"></param>
        /// <returns></returns>
        public JumboECMS.Entity.Normal_CategoryTree GetCategoryTree(string _categoryid, bool _includechild)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                return getTree(_doh, _categoryid, _includechild);
            }
        }
        private JumboECMS.Entity.Normal_CategoryTree getTree(DbOperHandler _doh, string _categoryid, bool _includechild)
        {
            JumboECMS.Entity.Normal_CategoryTree _tree = new JumboECMS.Entity.Normal_CategoryTree();
            JumboECMS.Entity.Normal_Category _category = new JumboECMS.DAL.Normal_CategoryDAL().GetEntity(_categoryid, "");
            _tree.Id = Str2Int(_categoryid);
            _tree.Name = _category.Title;
            _tree.Link = Go2Category(_categoryid, 1);
            _tree.HasChild = HasChild(_categoryid);
            List<JumboECMS.Entity.Normal_CategoryTree> subtree = new List<JumboECMS.Entity.Normal_CategoryTree>();
            if (_includechild)
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT Id FROM [" + base.CategoryTable + "] WHERE [ParentId]=" + _categoryid + " order by code";
                DataTable dtCategory = _doh.GetDataTable();
                for (int i = 0; i < dtCategory.Rows.Count; i++)
                {
                    string _subcategoryid = dtCategory.Rows[i]["Id"].ToString();
                    subtree.Add(getTree(_doh, _subcategoryid, _includechild));
                }
                dtCategory.Clear();
                dtCategory.Dispose();
            }
            _tree.SubChild = subtree;
            return _tree;
        }
    }
}
