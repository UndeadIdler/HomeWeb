using System;
using System.Collections.Generic;
using System.Text;
using JumboECMS.API.Discuz.Toolkit;

namespace JumboECMS.API.Discuz
{
    public class DiscuzSessionHelper
    {
        private static string apikey, secret, url;
        private static DiscuzSession ds;
        static DiscuzSessionHelper()
        {
            string strXmlFile = System.Web.HttpContext.Current.Server.MapPath("~/_data/config/site.config");
            JumboECMS.DBUtility.XmlControl XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
            apikey = XmlTool.GetText("Root/ForumAPIKey");//API Key
            secret = XmlTool.GetText("Root/ForumSecret");//密钥
            url = XmlTool.GetText("Root/ForumUrl");//论坛地址
            XmlTool.Dispose();
            ds = new DiscuzSession(apikey, secret, url);
        }

        public static DiscuzSession GetSession()
        {
            return ds;
        }
    }
}
