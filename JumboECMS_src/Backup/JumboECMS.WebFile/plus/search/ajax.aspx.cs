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
using System.Collections.Generic;
using JumboECMS.Utils;

namespace JumboECMS.WebFile.Plus.Search
{
    public partial class _ajax : JumboECMS.UI.AdminCenter
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
                case "ajaxGetContentList":
                    ajaxGetContentList();
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
        private void ajaxGetContentList()
        {
            string type = (q("type") == "" || q("type") == "all") ? JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ModuleList") : q("type");
            string _mode = q("mode");
            int page = Str2Int(q("page"), 1);
            int PSize = Str2Int(q("pagesize"), 10);
            string keyword = q("k").Length < 2 ? q("k") : JumboECMS.Utils.WordSpliter.GetKeyword(q("k"));//分词
            string keyword2 = q("k");
            if (_mode == "1") keyword2 = keyword;//自动分词
            int countNum = 0;
            double eventTime = 0;
            List<JumboECMS.Utils.LuceneHelp.SearchItem> result = JumboECMS.Utils.LuceneHelp.SearchIndex.Search(type, keyword2, PSize, page, out countNum, out eventTime);
            string tempstr = "{recordcount :" + countNum + ", siteurl :'" + site.Url + "', eventtime :'" + eventTime + "', \n";
            tempstr += "table: [";
            if (result != null)
            {
                for (int j = 0; j < result.Count; j++)
                {
                    if (j > 0) tempstr += ",";
                    tempstr += "{id:" + result[j].Id + "," +
                        "title: '" + HighLightKeyWord(result[j].Title, keyword).Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", " ") + "', " +
                        "summary: '" + HighLightKeyWord(result[j].Summary + "...", keyword).Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", " ") + "', " +
                        "tags: '" + HighLightKeyWord(result[j].Tags + "...", keyword).Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", " ") + "', " +
                        "adddate: '" + result[j].AddDate + "', " +
                        "url: '" + result[j].Url + "'" +
                        "}";
                }
            }
            tempstr += "],";
            tempstr += "pagebar:'" + (JumboECMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxPluginSearchList(" + PSize + ",*);")).Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", " ") + "'}";
            this._response = tempstr;
        }

    }
}
