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
using System.Web.SessionState;
namespace JumboECMS.Common
{
    /// <summary>
    /// 只读常量
    /// </summary>
    public class Const
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (DatabaseType == "0")
                {
                    if (HttpContext.Current.Application["jecmsV161_dbPath"] == null)
                    {
                        string dbPath = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbPath");
                        HttpContext.Current.Application.Lock();
                        HttpContext.Current.Application["jecmsV161_dbPath"] = dbPath;
                        HttpContext.Current.Application.UnLock();
                    }
                    return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath(HttpContext.Current.Application["jecmsV161_dbPath"].ToString());
                }
                else
                {
                    if (HttpContext.Current.Application["jecmsV161_dbConnStr"] == null)
                    {
                        string dbServerIP = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbServerIP");
                        string dbLoginName = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginName");
                        string dbLoginPass = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginPass");
                        string dbName = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbName");
                        string dbConnStr = "Data Source=" + dbServerIP + ";Initial Catalog=" + dbName + ";User ID=" + dbLoginName + ";Password=" + dbLoginPass + ";Pooling=true";
                        HttpContext.Current.Application.Lock();
                        HttpContext.Current.Application["jecmsV161_dbConnStr"] = dbConnStr;
                        HttpContext.Current.Application.UnLock();
                    }
                    return HttpContext.Current.Application["jecmsV161_dbConnStr"].ToString();
                }
            }
        }
        /// <summary>
        ///  数据库类型:0代表Access，1代表SqlServer
        /// </summary>
        public static string DatabaseType
        {
            get
            {
                if (System.Web.HttpContext.Current.Application["jecmsV161_dbType"] == null)
                {
                    System.Web.HttpContext.Current.Application.Lock();
                    System.Web.HttpContext.Current.Application["jecmsV161_dbType"] = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbType");
                    System.Web.HttpContext.Current.Application.UnLock();
                }
                return System.Web.HttpContext.Current.Application["jecmsV161_dbType"].ToString();
            }
        }
        /// <summary>
        /// 获得用户IP
        /// </summary>
        public static string GetUserIp
        {
            get
            {
                string ip;
                string[] temp;
                bool isErr = false;
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                else
                    ip = HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
                if (ip.Length > 15)
                    isErr = true;
                else
                {
                    temp = ip.Split('.');
                    if (temp.Length == 4)
                    {
                        for (int i = 0; i < temp.Length; i++)
                        {
                            if (temp[i].Length > 3) isErr = true;
                        }
                    }
                    else
                        isErr = true;
                }

                if (isErr)
                    return "1.1.1.1";
                else
                    return ip;
            }
        }
        /// <summary>
        /// 格式化IP
        /// </summary>
        public static string FormatIp(string ipStr)
        {
            string[] temp = ipStr.Split('.');
            string format = "";
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].Length < 3) temp[i] = Convert.ToString("000" + temp[i]).Substring(Convert.ToString("000" + temp[i]).Length - 3, 3);
                format += temp[i].ToString();
            }
            return format;
        }
        /// <summary>
        /// 来源地址
        /// </summary>
        public static string GetRefererUrl
        {
            get
            {
                if (HttpContext.Current.Request.ServerVariables["Http_Referer"] == null)
                    return "";
                else
                    return HttpContext.Current.Request.ServerVariables["Http_Referer"].ToString();
            }
        }
        /// <summary>
        /// 当前地址
        /// </summary>
        public static string GetCurrentUrl
        {
            get
            {
                string strUrl;
                strUrl = HttpContext.Current.Request.ServerVariables["Url"];
                if (HttpContext.Current.Request.QueryString.Count == 0) //如果无参数
                    return strUrl;
                else
                    return strUrl + "?" + HttpContext.Current.Request.ServerVariables["Query_String"];
            }

        }
        /// <summary>
        /// 判断校验码是否符合要求
        /// </summary>
        /// <param name="code">用户输入的校验码</param>
        /// <returns>返回校验码是否正确</returns>
        public bool CheckValidateCode(string code)
        {
            try
            {
                if (code.ToUpper() != HttpContext.Current.Session["jcms_validate_code"].ToString().ToUpper())
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
