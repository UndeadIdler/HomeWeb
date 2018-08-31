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
using System.Collections.Generic;
using System.Web;
using JumboECMS.Utils;
using JumboECMS.DBUtility;
using JumboECMS.Common;

namespace JumboECMS.DAL
{
    /// <summary>
    /// 网站参数
    /// </summary>
    public class SiteDAL
    {
        public SiteDAL()
        { }
        /// <summary>
        /// 获得网站参数
        /// </summary>
        /// <returns></returns>
        public JumboECMS.Entity.Site GetEntity()
        {
            JumboECMS.Entity.Site eSite = new JumboECMS.Entity.Site();
            eSite.Name1 = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Name1");
            eSite.Name2 = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Name2");
            eSite.Url = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Url");
            if (eSite.Url == "")
                eSite.Url = JumboECMS.Utils.App.Url;
            eSite.Dir = JumboECMS.Utils.App.Path;
            eSite.ICP1 = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ICP1");
            eSite.Keywords1 = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Keywords1");
            eSite.Description1 = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Description1");
            eSite.ICP2 = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ICP2");
            eSite.Keywords2 = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Keywords2");
            eSite.Description2 = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Description2");
            eSite.AllowReg = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "AllowReg") == "1";
            eSite.CheckReg = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "CheckReg") == "1";
            eSite.AdminGroupId = JumboECMS.Utils.Validator.StrToInt(JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "AdminGroupId"), 5);
            eSite.CookieDomain = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "CookieDomain");
            eSite.CookiePath = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "CookiePath");
            eSite.CookiePrev = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "CookiePrev");
            eSite.CookieKeyCode = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "CookieKeyCode");
            eSite.ExecuteSql = (JumboECMS.Utils.Validator.StrToInt(JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ExecuteSql"), 0) == 1);
            eSite.DebugKey = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "DebugKey");
            if (eSite.DebugKey.Length == 0) eSite.DebugKey = "1111-2222-3333-4444";
            eSite.MailOnceCount = JumboECMS.Utils.Validator.StrToInt(JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "MailOnceCount"), 15);
            eSite.MailTimeCycle = JumboECMS.Utils.Validator.StrToInt(JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "MailTimeCycle"), 300);
            eSite.MailPrivateKey = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "MailPrivateKey");
            eSite.MainSite = (JumboECMS.Utils.Validator.StrToInt(JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "MainSite"), 0) == 1);
            eSite.WanSite = (JumboECMS.Utils.Validator.StrToInt(JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "WanSite"), 0) == 1);
            eSite.ProductMaxBuyCount = JumboECMS.Utils.Validator.StrToInt(JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ProductMaxBuyCount"), 20);
            eSite.ProductMaxCartCount = JumboECMS.Utils.Validator.StrToInt(JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ProductMaxCartCount"), 20);
            eSite.ProductMaxOrderCount = JumboECMS.Utils.Validator.StrToInt(JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ProductMaxOrderCount"), 5);
            eSite.PassportTheme = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "PassportTheme");
            return eSite;
        }
        public void CreateSiteFiles()
        {
            JumboECMS.Entity.Site site = GetEntity();
            //生成配置文件
            string TempStr = string.Empty;
            TempStr = "var site = new Object();\r\n" +
                "site.Name1 = '" + site.Name1 + "';\r\n" +
                "site.Name2 = '" + site.Name2 + "';\r\n" +
                "site.Dir = '" + site.Dir + "';\r\n" +
                "site.CookieDomain = '" + site.CookieDomain + "';\r\n" +
                "site.CookiePrev = '" + site.CookiePrev + "';\r\n";
            string _globalJS = JumboECMS.Utils.DirFile.ReadFile("~/_data/global.js");
            string _strBegin = "//<!--网站参数begin";
            string _strEnd = "//-->网站参数end";
            System.Collections.ArrayList TagArray = JumboECMS.Utils.Strings.GetHtmls(_globalJS, _strBegin, _strEnd, true, true);
            if (TagArray.Count > 0)//标签存在
            {
                _globalJS = _globalJS.Replace(TagArray[0].ToString(), _strBegin + "\r\n\r\n" + TempStr + "\r\n\r\n" + _strEnd);
            }
            JumboECMS.Utils.DirFile.SaveFile(_globalJS, "~/_data/global.js");
        }
    }
}
