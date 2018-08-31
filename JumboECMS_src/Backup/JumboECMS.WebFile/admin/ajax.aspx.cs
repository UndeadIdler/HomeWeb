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
using JumboECMS.Utils;
using JumboECMS.Common;
using System.Text;
using Lucene.Net;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis.Standard;
namespace JumboECMS.WebFile.Admin
{
    public partial class _other_ajax : JumboECMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxSetVersion":
                    ajaxSetVersion();
                    break;
                case "ajaxChkVersion":
                    ajaxChkVersion();
                    break;
                case "leftmenu":
                    GetLeftMenu();
                    break;
                case "login":
                    ajaxLogin();
                    break;
                case "logout":
                    Logout();
                    break;
                case "chkadminpower":
                    ChkAdminPower();
                    break;
                case "ajaxClearSystemCache":
                    ajaxClearSystemCache();
                    break;
                case "ajaxCreateSystemCount":
                    ajaxCreateSystemCount();
                    break;
                case "ajaxCreateIndexPage":
                    ajaxCreateIndexPage();
                    break;
                case "ajaxCreateSearchIndex":
                    ajaxCreateSearchIndex();
                    break;
                case "ajaxChinese2Pinyin":
                    ajaxChinese2Pinyin();
                    break;
                case "ajaxCreateHTML":
                    ajaxCreateHTML();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }
        private void DefaultResponse()
        {
            Admin_Load("", "json");
            this._response = JsonResult(1, "成功登录");
        }
        private void GetLeftMenu()
        {
            Admin_Load("", "json");
            int menuId = Str2Int(q("m"), 1);
            int minId = 0;
            int maxId = 0;
            string[,] menu = leftMenu();
            StringBuilder sb = new StringBuilder();
            if (menuId < publicMenu)
            {
                minId = menuId;
                maxId = menuId;
            }
            else
            {
                minId = menuId;
                maxId = menu.GetLength(0) - 1;
            }
            int menuNum = (maxId - minId + 1);
            sb.Append("{\"result\":\"1\", \"returnval\":\"获取成功\", \"recordcount\":" + (maxId - minId + 1) + ", \"table\":[");
            int NO = 0;
            for (int i = minId; i < maxId + 1; i++)
            {
                if (menu[i, 0] == null) break;
                if (NO > 0) sb.Append(",");
                NO++;
                sb.Append("{\"no\":" + NO + ", ");
                sb.Append("\"title\":\"" + menu[i, 0].Split('$')[0] + "\", ");
                sb.Append("\"table\":[");
                for (int j = 1; j < menu.GetLength(1); j++)
                {
                    if (menu[i, j] == null) break;
                    if (j > 1) sb.Append(",");
                    sb.Append("{\"no\":" + j + ", ");
                    string[] _thisMenu = menu[i, j].Split('|');
                    string _depth = "1";
                    string _groupid = "0";
                    if (_thisMenu.Length > 2)
                        _depth = _thisMenu[2];
                    if (_thisMenu.Length > 3)
                        _groupid = _thisMenu[3];
                    sb.Append("\"url\":\"" + _thisMenu[0] + "\",");
                    sb.Append("\"title\":\"" + _thisMenu[1] + "\",");
                    sb.Append("\"depth\":\"" + _depth + "\",");
                    sb.Append("\"groupid\":\"" + _groupid + "\"");
                    sb.Append("}");
                }
                sb.Append("]}");
            }
            sb.Append("]}");
            this._response = sb.ToString();
        }
        private void ajaxSetVersion()
        {
            JumboECMS.Utils.Cookie.SetObj("Version", 1, base.Version, site.CookieDomain, "/");
            this._response = JsonResult(1, "设置成功");
        }
        private void ajaxChkVersion()
        {
            string _version = JumboECMS.Utils.Cookie.GetValue("Version");
            if (_version == base.Version)
                this._response = JsonResult(1, "匹配成功");
            else
                this._response = JsonResult(0, "匹配失败");
        }
        private void ajaxLogin()
        {
            string _name = f("name");
            string _pass = JumboECMS.Utils.Strings.Left(f("pass"), 14);
            int _type = JumboECMS.Utils.Validator.StrToInt(f("type"), 0);
            int _screenwidth = JumboECMS.Utils.Validator.StrToInt(f("screenwidth"), 0);
            int iExpires = 0;
            if (_type > 0)
                iExpires = 60 * 60 * 24 * _type;//保存天数
            string _loginInfo = new JumboECMS.DAL.AdminDAL().ChkAdminLogin(_name, _pass, iExpires, _screenwidth);
            this._response = _loginInfo;
        }
        private void Logout()
        {
            new JumboECMS.DAL.AdminDAL().ChkAdminLogout();
            this._response = JsonResult(1, "成功退出");
        }
        private void ChkAdminPower()
        {
            Admin_Load(q("power"), "json");
            this._response = JsonResult(1, "身份合法");
        }
        private void ajaxClearSystemCache()
        {
            Admin_Load("master", "json");
            new JumboECMS.DAL.SiteDAL().CreateSiteFiles();
            SetupSystemDate();
            this._response = JsonResult(1, "基本参数更新完成");
        }
        private void ajaxCreateSystemCount()
        {
            Admin_Load("master", "json");
            CreateCount();
            this._response = JsonResult(1, "统计更新完成");
        }
        private void ajaxCreateIndexPage()
        {
            Admin_Load("", "json");
            JumboECMS.DAL.TemplateEngineDAL teDAL = new JumboECMS.DAL.TemplateEngineDAL();
            teDAL.CreateDefaultFile();
            this._response = JsonResult(1, "网站首页更新完成");
        }
        private void ajaxCreateSearchIndex()
        {
            Admin_Load("master", "json");
            string[] _type = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ModuleList").Split(',');
            for (int i = 0; i < _type.Length; i++)
            {
                CreateSearchIndex(_type[i], Str2Int(q("create")) == 1);
            }
            this._response = JsonResult(1, "索引更新完成");
        }
        private IndexWriter CreateSearchIndex(string _type, bool _create)
        {
            string strXmlFile = Server.MapPath("~/_data/config/jcms(searchindex).config");
            JumboECMS.DBUtility.XmlControl XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
            string _lastid = XmlTool.GetText("Module/" + _type + "/lastid");
            XmlTool.Dispose();
            string INDEX_STORE_PATH = Server.MapPath("~/_data/index/" + _type + "/");  //INDEX_STORE_PATH 为索引存储目录
            IndexWriter writer = null;
            try
            {
                if (!_create)
                {
                    try
                    {
                        writer = new IndexWriter(INDEX_STORE_PATH, new StandardAnalyzer(), false);
                    }
                    catch (Exception)
                    {
                        writer = new IndexWriter(INDEX_STORE_PATH, new StandardAnalyzer(), true);
                    }
                }
                else
                {
                    writer = new IndexWriter(INDEX_STORE_PATH, new StandardAnalyzer(), true);
                    _lastid = "0";
                }
                doh.Reset();
                doh.ConditionExpress = "[id]>" + _lastid;
                if (!doh.Exist("jcms_module_" + _type))
                    return null;
                doh.Reset();
                doh.SqlCmd = "select Id,CategoryId,AddDate,Title,Summary,Tags,FirstPage from [jcms_module_" + _type + "] WHERE [Ispass]=1 AND [id]>" + _lastid;
                DataTable dtContent = doh.GetDataTable();
                //建立索引字段
                for (int j = 0; j < dtContent.Rows.Count; j++)
                {
                    string _url = dtContent.Rows[j]["FirstPage"].ToString();

                    Document doc = new Document();
                    Field field = new Field("id", dtContent.Rows[j]["Id"].ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED);//存储，不索引
                    doc.Add(field);
                    field = new Field("url", _url, Field.Store.YES, Field.Index.NO);
                    doc.Add(field);
                    field = new Field("tablename", _type, Field.Store.YES, Field.Index.TOKENIZED);//存储，索引
                    doc.Add(field);
                    field = new Field("title", dtContent.Rows[j]["title"].ToString(), Field.Store.YES, Field.Index.TOKENIZED);//存储，索引
                    doc.Add(field);
                    field = new Field("adddate", dtContent.Rows[j]["adddate"].ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED);//存储，不索引
                    doc.Add(field);
                    field = new Field("summary", dtContent.Rows[j]["Summary"].ToString(), Field.Store.YES, Field.Index.TOKENIZED);//存储，索引
                    doc.Add(field);
                    field = new Field("tags", dtContent.Rows[j]["Tags"].ToString(), Field.Store.YES, Field.Index.TOKENIZED);//存储，索引
                    doc.Add(field);
                    writer.AddDocument(doc);
                }
                dtContent.Clear();
                dtContent.Dispose();
                //writer.Optimize();不要写这句，否则为覆盖
                writer.Close();
            }
            catch (Exception)
            {
            }
            doh.Reset();
            doh.ConditionExpress = "[Ispass]=1 ORDER BY Id desc";
            int _maxid = JumboECMS.Utils.Validator.StrToInt(doh.GetField("jcms_module_" + _type, "Id").ToString(), 0);
            strXmlFile = Server.MapPath("~/_data/config/jcms(searchindex).config");
            XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
            XmlTool.Update("Module/" + _type + "/lastid", _maxid.ToString());
            XmlTool.Update("Module/" + _type + "/lasttime", System.DateTime.Now.ToString(), true);
            XmlTool.Save();
            XmlTool.Dispose();
            return writer;
        }
        private void ajaxChinese2Pinyin()
        {
            Admin_Load("", "json");
            int t = Str2Int(f("t"), 0);
            if (t == 1)
                this._response = JsonResult(1, JumboECMS.Utils.ChineseSpell.MakeSpellCode(f("chinese"), "", SpellOptions.TranslateUnknowWordToInterrogation));
            else
                this._response = JsonResult(1, JumboECMS.Utils.ChineseSpell.MakeSpellCode(f("chinese"), "", SpellOptions.FirstLetterOnly));
        }
        private void ajaxCreateHTML()
        {
            TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
            string categoryid = Str2Str(q("categoryid"));
            doh.Reset();
            doh.SqlCmd = "SELECT Id,[Title],[ModuleType],[TypeId],LanguageCode FROM [" + base.CategoryTable + "] WHERE [TypeId]<4";
            if (categoryid != "0")
                doh.SqlCmd += " AND ([Code] LIKE (Select code FROM [" + base.CategoryTable + "] WHERE id=" + categoryid + ")+'%') ORDER BY code";
            DataTable dtCategory = doh.GetDataTable();
            int _no = 1;
            for (int i = 0; i < dtCategory.Rows.Count; i++)
            {
                string _CategoryId = dtCategory.Rows[i]["Id"].ToString();
                string _CategoryModuleType = dtCategory.Rows[i]["ModuleType"].ToString();
                string _CategoryTypeId = dtCategory.Rows[i]["TypeId"].ToString();
                string _CategoryLanguageCode = dtCategory.Rows[i]["LanguageCode"].ToString();
                if (_CategoryTypeId == "2")//终极栏目
                {
                    JumboECMS.Entity.Normal_Module ThisModule = new JumboECMS.DAL.Normal_ModuleDAL().GetEntity(_CategoryModuleType);
                    doh.Reset();
                    doh.SqlCmd = "SELECT id FROM [jcms_module_" + _CategoryModuleType + "] WHERE [IsPass]=1 and [CategoryID]=" + _CategoryId;
                    DataTable dtContent = doh.GetDataTable();
                    for (int j = 0; j < dtContent.Rows.Count; j++)
                    {
                        string _ContentId = dtContent.Rows[j]["Id"].ToString();
                        CreateContentFile(ThisModule, _ContentId, -1);
                        _no++;
                    }
                    dtContent.Clear();
                    dtContent.Dispose();
                }
                CreateCategoryFile(_CategoryId, false);
                _no++;
            }
            dtCategory.Clear();
            dtCategory.Dispose();
            JumboECMS.DAL.TemplateEngineDAL teDAL = new JumboECMS.DAL.TemplateEngineDAL();
            teDAL.CreateDefaultFile();
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts = ts2.Subtract(ts1).Duration();
            this._response = JsonResult(1, "耗时" + ts.Seconds.ToString() + " 秒，生成" + _no + "个文件");
        }
    }
}
