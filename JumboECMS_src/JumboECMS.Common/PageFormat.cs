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
using System.Web;
namespace JumboECMS.Common
{
    /// <summary>
    /// 页面地址格式
    /// </summary>
    public static class PageFormat
    {
        /// <summary>
        /// 站点首页
        /// </summary>
        public static string Site(string _siteDir)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath(_siteDir + "_data/config/pageformat.config");
            JumboECMS.DBUtility.XmlControl XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
            string TempUrl = XmlTool.GetText("Pages/Site/P"); ;
            XmlTool.Dispose();
            return TempUrl;
        }
        /// <summary>
        /// 栏目页
        /// </summary>
        public static string Category(string _siteDir, int page)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath(_siteDir + "_data/config/pageformat.config");
            JumboECMS.DBUtility.XmlControl XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
            string TempUrl = "";
            if (page == 1)
                TempUrl = XmlTool.GetText("Pages/Category/P_1");
            else
                TempUrl = XmlTool.GetText("Pages/Category/P_N");
            XmlTool.Dispose();
            return TempUrl;
        }
        /// <summary>
        /// 内容页
        /// </summary>
        public static string View(string _siteDir, string _moduletype, int page)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath(_siteDir + "_data/config/pageformat.config");
            JumboECMS.DBUtility.XmlControl XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
            string TempUrl = "";
            if (page == 1)
                TempUrl = XmlTool.GetText("Pages/View/P_1");
            else
                TempUrl = XmlTool.GetText("Pages/View/P_N");
            XmlTool.Dispose();
            return TempUrl;
        }
    }
}
