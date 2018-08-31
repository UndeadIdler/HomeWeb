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
using JumboECMS.Common;
using JumboECMS.DBUtility;
using JumboECMS.Entity;
using JumboECMS.Utils;
using Newtonsoft.Json;
namespace JumboECMS.DAL
{
    /// <summary>
    /// 语言包
    /// </summary>
    public class LanguageDAL
    {
        public LanguageDAL()
        { }
        /// <summary>
        /// 绑定语言包(V6之后深入开发)
        /// </summary>
        /// <param name="_lng">如cn表示中文，en表示英文</param>
        /// <returns></returns>
        public Dictionary<string, object> GetEntity(string _lng)
        {
            string json = JumboECMS.Utils.DirFile.ReadFile("~/_data/languages/" + _lng + ".js");
            json = JumboECMS.Utils.Strings.GetHtml(json, "//<!--语言包begin", "//-->语言包end");
            return (Dictionary<string, object>)JumboECMS.Utils.fastJSON.JSON.Instance.ToObject(json);
        }
    }
}
