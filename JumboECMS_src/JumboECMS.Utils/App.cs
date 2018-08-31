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
using System.Web.Caching;
using System.Text;

namespace JumboECMS.Utils
{
    /// <summary>
    /// App操作类
    /// </summary>
    public static class App
    {
        public static string Url
        {
            get
            {
                if (HttpContext.Current.Request.Url.Port == 80)
                    return "http://" + HttpContext.Current.Request.Url.Host;
                else
                    return "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
            }
        }
        /// <summary>
        /// 应用程序路径，以/结尾
        /// </summary>
        /// <returns>如:/，/cms/</returns>
        public static string Path
        {
            get
            {
                string _ApplicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                if (_ApplicationPath != "/")
                    _ApplicationPath += "/";
                return _ApplicationPath;
            }
        }
    }
}
